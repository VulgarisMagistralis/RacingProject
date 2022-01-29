// COPYRIGHT 1995-2021 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Attn: Contracts and Legal Department
// Environmental Systems Research Institute, Inc.
// 380 New York Street
// Redlands, California 92373
// USA
//
// email: legal@esri.com
using Esri.ArcGISMapsSDK.Renderer.GPUComputing;
using Esri.ArcGISMapsSDK.Renderer.GPUResources;
using Esri.ArcGISMapsSDK.Renderer.SceneComponents;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.RenderCommandQueue;
using Esri.GameEngine.RenderCommandQueue.Parameters;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Esri.ArcGISMapsSDK.Renderer
{
	internal class RenderCommandClient
	{
		private static readonly int baseTextureUVIndex = 0;
		private static readonly int featureIndicesUVIndex = 1;
		private static readonly int uvRegionIdsUVIndex = 2;

		private readonly GPUResourcesProvider gpuResourceProvider;
		private readonly SceneComponentProvider sceneComponentProvider;
		private readonly INormalMapGenerator normalMapGenerator;
		private readonly IImageComposer imageComposer;

		public RenderCommandClient(GPUResourcesProvider gpuResourceProvider, SceneComponentProvider sceneComponentProvider, IImageComposer imageComposer, INormalMapGenerator normalMapGenerator)
		{
			this.gpuResourceProvider = gpuResourceProvider;
			this.sceneComponentProvider = sceneComponentProvider;
			this.normalMapGenerator = normalMapGenerator;
			this.imageComposer = imageComposer;
		}

		public void ExecuteRenderCommand(RenderCommand renderCommand)
		{
			switch (renderCommand.Type)
			{
				// Creation Commands
				case RenderCommandType.CreateMaterial:
					{
						CreateMaterialCommandParameters parameters = (CreateMaterialCommandParameters)renderCommand.CommandParameters;

						UnityEngine.Material material = Interop.FromUnityMaterial(parameters.Material);
						gpuResourceProvider.CreateMaterial(parameters.MaterialId, parameters.MaterialType, material);
					}
					break;

				case RenderCommandType.CreateRenderTarget:
					{
						CreateRenderTargetCommandParameters parameters = (CreateRenderTargetCommandParameters)renderCommand.CommandParameters;
						gpuResourceProvider.CreateRenderTexture(parameters.RenderTargetId, parameters.Width, parameters.Height, parameters.TextureFormat, parameters.HasMipMaps);
					}
					break;

				case RenderCommandType.CreateSceneComponent:
					{
						CreateSceneComponentCommandParameters parameters = (CreateSceneComponentCommandParameters)renderCommand.CommandParameters;
						sceneComponentProvider.CreateSceneComponent(parameters.SceneComponentId);
					}
					break;

				case RenderCommandType.CreateTexture:
					{
						CreateTextureCommandParameters parameters = (CreateTextureCommandParameters)renderCommand.CommandParameters;
						gpuResourceProvider.CreateTexture(parameters.TextureId, parameters.Width, parameters.Height, parameters.TextureFormat, parameters.IsSRGB);
					}
					break;

				// Destruction Commands
				case RenderCommandType.DestroyMaterial:
					{
						DestroyMaterialCommandParameters parameters = (DestroyMaterialCommandParameters)renderCommand.CommandParameters;

						gpuResourceProvider.DestroyMaterial(parameters.MaterialId);
					}
					break;

				case RenderCommandType.DestroyRenderTarget:
					{
						DestroyRenderTargetCommandParameters parameters = (DestroyRenderTargetCommandParameters)renderCommand.CommandParameters;

						gpuResourceProvider.DestroyRenderTexture(parameters.RenderTargetId);
					}
					break;

				case RenderCommandType.DestroySceneComponent:
					{
						DestroySceneComponentCommandParameters parameters = (DestroySceneComponentCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						sceneComponentProvider.DestroySceneComponent(parameters.SceneComponentId);
					}
					break;

				case RenderCommandType.DestroyTexture:
					{
						DestroyTextureCommandParameters parameters = (DestroyTextureCommandParameters)renderCommand.CommandParameters;

						gpuResourceProvider.DestroyTexture(parameters.TextureId);
					}
					break;

				// SceneComponent operations
				case RenderCommandType.SetVisible:
					{
						SetVisibleCommandParameters parameters = (SetVisibleCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						sceneComponentProvider.SceneComponents[parameters.SceneComponentId].IsVisible = parameters.IsVisible;
					}
					break;

				case RenderCommandType.SetMaterial:
					{
						SetMaterialCommandParameters parameters = (SetMaterialCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId) && gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId));

						sceneComponentProvider.SceneComponents[parameters.SceneComponentId].Material = gpuResourceProvider.Materials[parameters.MaterialId];
					}
					break;

				case RenderCommandType.SetMesh:
					{
						SetMeshCommandParameters parameters = (SetMeshCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						var sceneComponent = sceneComponentProvider.SceneComponents[parameters.SceneComponentId];

						var mesh = sceneComponent.Mesh;

						if (mesh == null)
						{
							mesh = sceneComponent.Mesh = gpuResourceProvider.CreateMesh(parameters.SceneComponentId);
							mesh.MarkDynamic();
						}

						mesh.Clear();

						mesh.SetVertices(parameters.Positions.ToNativeArray<UnityEngine.Vector3>());
						mesh.SetNormals(parameters.Normals.ToNativeArray<UnityEngine.Vector3>());
						mesh.SetColors(parameters.Colors.ToNativeArray<UnityEngine.Color32>());
						mesh.SetUVs(baseTextureUVIndex, parameters.Uvs.ToNativeArray<UnityEngine.Vector2>());
						mesh.SetUVs(featureIndicesUVIndex, parameters.FeatureIndices.ToNativeArray<float>());
						mesh.SetUVs(uvRegionIdsUVIndex, parameters.UvRegionIds.ToNativeArray<float>());
						mesh.SetTriangles(parameters.Triangles.ToNativeArray<int>());

						mesh.RecalculateBounds();
					}
					break;

				case RenderCommandType.SetMeshBuffer:
					{
						SetMeshBufferCommandParameters parameters = (SetMeshBufferCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						var sceneComponent = sceneComponentProvider.SceneComponents[parameters.SceneComponentId];
						var mesh = sceneComponent.Mesh;

						switch (parameters.MeshBufferChangeType)
						{
							case MeshBufferChangeType.Positions:
								mesh.SetVertices(parameters.Buffer.ToNativeArray<UnityEngine.Vector3>());
								break;

							case MeshBufferChangeType.Normals:
								mesh.SetNormals(parameters.Buffer.ToNativeArray<UnityEngine.Vector3>());
								break;

							case MeshBufferChangeType.Uv0:
								mesh.SetUVs(baseTextureUVIndex, parameters.Buffer.ToNativeArray<UnityEngine.Vector2>());
								break;

							case MeshBufferChangeType.Colors:
								mesh.SetColors(parameters.Buffer.ToNativeArray<UnityEngine.Color32>());
								break;
						}
					}
					break;

				case RenderCommandType.SetSceneComponentPivot:
					{
						SetSceneComponentPivotCommandParameters parameters = (SetSceneComponentPivotCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						Vector3d pivot;
						pivot.x = parameters.X;
						pivot.y = parameters.Y;
						pivot.z = parameters.Z;

						sceneComponentProvider.SceneComponents[parameters.SceneComponentId].Location = pivot;
					}
					break;

				// Texture and RenderTexture Operations
				case RenderCommandType.SetTexturePixelData:
					{
						SetTexturePixelDataCommandParameters parameters = (SetTexturePixelDataCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Textures.ContainsKey(parameters.TextureId));

						gpuResourceProvider.Textures[parameters.TextureId].SetPixelData(parameters.Pixels.Data, parameters.Pixels.Size);
					}
					break;

				case RenderCommandType.GenerateMipMaps:
					{
						GenerateMipMapsCommandParameters parameters = (GenerateMipMapsCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.RenderTextures.ContainsKey(parameters.RenderTargetId));

						gpuResourceProvider.RenderTextures[parameters.RenderTargetId].GenerateMipMaps();
					}
					break;

				// Material Operations
				case RenderCommandType.SetMaterialRenderTargetProperty:
					{
						SetMaterialRenderTargetPropertyCommandParameters parameters = (SetMaterialRenderTargetPropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId) && gpuResourceProvider.RenderTextures.ContainsKey(parameters.Value));

						var shaderParam = getRenderTextureMaterialShaderParameterName(parameters.MaterialRenderTargetProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetTexture(shaderParam, gpuResourceProvider.RenderTextures[parameters.Value]);
					}
					break;

				case RenderCommandType.SetMaterialScalarProperty:
					{
						SetMaterialScalarPropertyCommandParameters parameters = (SetMaterialScalarPropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId));

						var shaderParam = getScalarMaterialShaderParameterName(parameters.MaterialScalarProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetFloat(shaderParam, parameters.Value);
					}
					break;

				case RenderCommandType.SetMaterialTextureProperty:
					{
						SetMaterialTexturePropertyCommandParameters parameters = (SetMaterialTexturePropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId) && gpuResourceProvider.Textures.ContainsKey(parameters.Value));

						var shaderParam = getTextureMaterialShaderParameterName(parameters.MaterialTextureProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetTexture(shaderParam, gpuResourceProvider.Textures[parameters.Value]);
					}
					break;

				case RenderCommandType.SetMaterialNamedTextureProperty:
					{
						SetMaterialNamedTexturePropertyCommandParameters parameters = (SetMaterialNamedTexturePropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId) && gpuResourceProvider.Textures.ContainsKey(parameters.Value));

						var textureName = Marshal.PtrToStringAnsi(parameters.TextureName.Data);

						gpuResourceProvider.Materials[parameters.MaterialId].SetTexture(textureName, gpuResourceProvider.Textures[parameters.Value]);
					}
					break;

				case RenderCommandType.SetMaterialVectorProperty:
					{
						SetMaterialVectorPropertyCommandParameters parameters = (SetMaterialVectorPropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId));

						UnityEngine.Vector4 value;
						value.x = parameters.Value.X;
						value.y = parameters.Value.Y;
						value.z = parameters.Value.Z;
						value.w = parameters.Value.W;

						var shaderParam = getVectorMaterialShaderParameterName(parameters.MaterialVectorProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetVector(shaderParam, value);
					}
					break;

				// Compose and normal operations
				case RenderCommandType.Compose:
					{
						// TODO: Implement when blending is added
					}
					break;

				case RenderCommandType.MultipleCompose:
					{
						MultipleComposeCommandParameters parameters = (MultipleComposeCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.RenderTextures.ContainsKey(parameters.TargetId));

						var composables = parameters.ComposedTextures.ToArray<ComposedTextureElement>();
						ComposableImage[] blenderInputArray = new ComposableImage[composables.Length];
						int pos = 0;

						foreach (var composable in composables)
						{
							Debug.Assert(gpuResourceProvider.Textures.ContainsKey(composable.TextureId));

							ComposableImage blenderInput;

							blenderInput.opacity = composable.Opacity;
							blenderInput.image = gpuResourceProvider.Textures[composable.TextureId];
							blenderInput.extent = new UnityEngine.Vector4(composable.Region.X, composable.Region.Y, composable.Region.Z, composable.Region.W);

							blenderInputArray[pos++] = blenderInput;
						}

						imageComposer.Compose(blenderInputArray, gpuResourceProvider.RenderTextures[parameters.TargetId]);
					}
					break;

				case RenderCommandType.Copy:
					{
						// TODO: Implement when blending is added
					}
					break;

				case RenderCommandType.GenerateNormalTexture:
					{
						GenerateNormalTextureCommandParameters parameters = (GenerateNormalTextureCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Textures.ContainsKey(parameters.ElevationId) && gpuResourceProvider.RenderTextures.ContainsKey(parameters.TargetId));

						normalMapGenerator.Compute(gpuResourceProvider.Textures[parameters.ElevationId], new UnityEngine.Vector4(parameters.TileExtension.X, parameters.TileExtension.Y, parameters.TileExtension.Z, parameters.TileExtension.W),
												new UnityEngine.Vector4(parameters.TextureExtension.X, parameters.TextureExtension.Y, parameters.TextureExtension.Z, parameters.TextureExtension.W), gpuResourceProvider.RenderTextures[parameters.TargetId]);
					}
					break;

				case RenderCommandType.CommandGroupBegin:
				case RenderCommandType.CommandGroupEnd:
					break;

				default:

					Debug.Fail("Unknown RenderCommand Type!");

					break;
			}
		}

		private static string getRenderTextureMaterialShaderParameterName(MaterialRenderTargetProperty parameter)
		{
			switch (parameter)
			{
				case MaterialRenderTargetProperty.ImageryMap0:
					return "_MainTex";
				case MaterialRenderTargetProperty.NormalMap0:
					return "_BumpMap";
				default:
					Debug.Fail("Not implemented MaterialRenderTargetProperty!");
					return "";
			}
		}

		private static string getTextureMaterialShaderParameterName(MaterialTextureProperty parameter)
		{
			switch (parameter)
			{
				case MaterialTextureProperty.BaseMap:
					return "_MainTex";
				case MaterialTextureProperty.UvRegionLut:
					return "_UVRegionLUT";
				case MaterialTextureProperty.FeatureIds:
					return "_FeatureIds";
				case MaterialTextureProperty.PositionsMap0:
					return "_VertexOffset";
				default:
					Debug.Fail("Not implemented MaterialTextureProperty!");
					return "";
			}
		}

		private static string getScalarMaterialShaderParameterName(MaterialScalarProperty parameter)
		{
			switch (parameter)
			{
				case MaterialScalarProperty.ClippingMode:
					return "_ClippingMode";
				case MaterialScalarProperty.UseUvRegionLut:
					return "_UseUvRegionLut";
				case MaterialScalarProperty.Opacity:
					return ""; // TODO: https://devtopia.esri.com/runtime/millennium-falcon/issues/1270#issuecomment-3022218
				default:
					Debug.Fail($"Not implemented MaterialScalarProperty: {parameter}!");
					return "";
			}
		}

		private static string getVectorMaterialShaderParameterName(MaterialVectorProperty parameter)
		{
			switch (parameter)
			{
				case MaterialVectorProperty.MapAreaMax:
					return "_MapAreaMax";
				case MaterialVectorProperty.MapAreaMin:
					return "_MapAreaMin";
				default:
					Debug.Fail("Not implemented MaterialVectorProperty!");
					return "";
			}
		}
	}
}
