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
using Esri.GameEngine.RenderCommandQueue.Parameters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Esri.ArcGISMapsSDK.Renderer.GPUResources
{
	internal class GPUResourcesProvider
	{
		private readonly Dictionary<uint, GPUResourceMaterial> materials = new Dictionary<uint, GPUResourceMaterial>();
		private readonly Dictionary<uint, GPUResourceMesh> meshes = new Dictionary<uint, GPUResourceMesh>();
		private readonly Dictionary<uint, GPUResourceTexture2D> textures = new Dictionary<uint, GPUResourceTexture2D>();
		private readonly Dictionary<uint, GPUResourceRenderTexture> renderTextures = new Dictionary<uint, GPUResourceRenderTexture>();

		public IReadOnlyDictionary<uint, GPUResourceMaterial> Materials => materials;
		public IReadOnlyDictionary<uint, GPUResourceMesh> Meshes => meshes;
		public IReadOnlyDictionary<uint, GPUResourceTexture2D> Textures => textures;
		public IReadOnlyDictionary<uint, GPUResourceRenderTexture> RenderTextures => renderTextures;

		public GPUResourceMaterial CreateMaterial(uint id, MaterialType materialType, Material customMaterial)
		{
			string basePath = "Shaders/Materials";

			Material material = null;

			if (!customMaterial)
			{
				var shaderPath = materialType == MaterialType.Tile ? "TileSurface" : "SceneNodeSurface";

#if (!UNITY_ANDROID && !UNITY_IOS) || UNITY_EDITOR
				if (GraphicsSettings.renderPipelineAsset != null)
				{
					var renderType = GraphicsSettings.renderPipelineAsset.GetType().ToString();

					if (renderType == "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset")
					{
						material = new Material(Resources.Load<Material>(basePath + "/URP/" + shaderPath));
					}
					else if (renderType == "UnityEngine.Rendering.HighDefinition.HDRenderPipelineAsset")
					{
						material = new Material(Resources.Load<Material>(basePath + "/HDRP/" + shaderPath));
					}
				}
				else
				{
					material = new Material(Resources.Load<Shader>(basePath + "/Legacy/" + shaderPath));
				}
#else
				if (GraphicsSettings.renderPipelineAsset != null)
				{
					var renderType = GraphicsSettings.renderPipelineAsset.GetType().ToString();

					if (renderType == "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset")
					{
						material = new Material(Resources.Load<Material>(basePath + "/URP/" + shaderPath));
					}
				}
				else
				{
					material = new Material(Resources.Load<Shader>(basePath + "/Legacy/" + shaderPath));
				}
#endif
			}
			else
			{
				material = new Material(customMaterial);
			}

			var resourceMaterial = new GPUResourceMaterial(material);
			materials.Add(id, resourceMaterial);

			return resourceMaterial;
		}

		public GPUResourceMesh CreateMesh(uint id)
		{
			var resourceMesh = new GPUResourceMesh(new Mesh());
			meshes.Add(id, resourceMesh);

			return resourceMesh;
		}

		public GPUResourceRenderTexture CreateRenderTexture(uint id, uint width, uint height, GameEngine.RenderCommandQueue.Parameters.TextureFormat format, bool useMipMaps)
		{
			var rendertextureFormat = GetUnityRendertextureFormatFromInternal(format);

			var nativeRenderTexture = new RenderTexture((int)width, (int)height, 0, rendertextureFormat, RenderTextureReadWrite.Linear);
			nativeRenderTexture.enableRandomWrite = true;
			nativeRenderTexture.autoGenerateMips = false;
			nativeRenderTexture.useMipMap = useMipMaps;
			nativeRenderTexture.anisoLevel = 9;
			nativeRenderTexture.Create();

			var resourceRenderTexture = new GPUResourceRenderTexture(nativeRenderTexture);
			renderTextures.Add(id, resourceRenderTexture);

			return resourceRenderTexture;
		}

		public GPUResourceTexture2D CreateTexture(uint id, uint width, uint height, GameEngine.RenderCommandQueue.Parameters.TextureFormat format, bool isSRGB)
		{
			var textureFormat = GetUnityTextureFormatFromInternal(format);
			Texture2D nativeTexture = new Texture2D((int)width, (int)height, textureFormat, false, !isSRGB);

			nativeTexture.wrapMode = TextureWrapMode.Clamp;
			nativeTexture.filterMode = FilterMode.Bilinear;

			var resourceTexture = new GPUResourceTexture2D(nativeTexture);
			textures.Add(id, resourceTexture);

			return resourceTexture;
		}

		public void DestroyMaterial(uint id)
		{
			Debug.Assert(materials.ContainsKey(id));

			var material = materials[id];
			material.Destroy();
			materials.Remove(id);
		}

		public void DestroyMesh(uint id)
		{
			Debug.Assert(meshes.ContainsKey(id));

			var mesh = meshes[id];
			mesh.Destroy();
			meshes.Remove(id);
		}

		public void DestroyTexture(uint id)
		{
			Debug.Assert(textures.ContainsKey(id));

			var texture = textures[id];
			texture.Destroy();
			textures.Remove(id);
		}

		public void DestroyRenderTexture(uint id)
		{
			Debug.Assert(renderTextures.ContainsKey(id));

			var renderTexture = renderTextures[id];
			renderTexture.Release();
			renderTexture.Destroy();
			renderTextures.Remove(id);
		}

		public void Release()
		{
			foreach (var texture in textures)
			{
				texture.Value.Destroy();
			}

			foreach (var renderTexture in renderTextures)
			{
				renderTexture.Value.Release();
				renderTexture.Value.Destroy();
			}

			textures.Clear();
			renderTextures.Clear();
		}

		private static UnityEngine.TextureFormat GetUnityTextureFormatFromInternal(GameEngine.RenderCommandQueue.Parameters.TextureFormat textureFormat)
		{
			switch (textureFormat)
			{
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.RGB8UNorm:
					return UnityEngine.TextureFormat.RGB24;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.R32Float:
					return UnityEngine.TextureFormat.RFloat;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.RGBA16UNorm:
					return UnityEngine.TextureFormat.RGBAFloat;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.RGBA8UNorm:
					return UnityEngine.TextureFormat.RGBA32;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.DXT1:
					return UnityEngine.TextureFormat.DXT1;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.DXT5:
					return UnityEngine.TextureFormat.DXT5;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.RGBA32Float:
					return UnityEngine.TextureFormat.RGBAFloat;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.ETC2RGB8:
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.ETC2SRGB8:
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.ETC2EacSRGB8:
					return UnityEngine.TextureFormat.ETC2_RGB;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.ETC2RGB8PunchthroughAlpha1:
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.ETC2SRGB8PunchthroughAlpha1:
					return UnityEngine.TextureFormat.ETC2_RGBA1;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.ETC2EacRGBA8:
					return UnityEngine.TextureFormat.ETC2_RGBA8;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.R32Norm:
					return UnityEngine.TextureFormat.RGBA32;
				default:
					Debug.LogError("Texture format not supported!");
					return UnityEngine.TextureFormat.RGB24;
			}
		}

		private static RenderTextureFormat GetUnityRendertextureFormatFromInternal(GameEngine.RenderCommandQueue.Parameters.TextureFormat textureFormat)
		{
			switch (textureFormat)
			{
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.RGBA8UNorm:
					return RenderTextureFormat.ARGB32;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.R32Float:
					return RenderTextureFormat.RFloat;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.RGBA16UNorm:
					return RenderTextureFormat.ARGBHalf;
				case GameEngine.RenderCommandQueue.Parameters.TextureFormat.RGBA32Float:
					return RenderTextureFormat.ARGBFloat;
				default:
					Debug.LogError("RenderTexture format not supported!");
					return RenderTextureFormat.ARGB32;
			}
		}
	}
}
