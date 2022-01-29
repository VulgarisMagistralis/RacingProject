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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Esri.GameEngine.RenderCommandQueue
{
	internal class DecodedRenderCommandQueue
	{
		private readonly Unity.DataBuffer<byte> rawRenderCommands;
		private ulong currentOffset = 0;

		public DecodedRenderCommandQueue(Unity.DataBuffer<byte> rawRenderCommands)
		{
			this.rawRenderCommands = rawRenderCommands;
		}

		public RenderCommand GetNextCommand()
		{
			if (currentOffset < rawRenderCommands.SizeBytes)
			{
				var commandType = GetCommandType(rawRenderCommands);

				switch (commandType)
				{
					case RenderCommandType.CreateMaterial:
						{
							var parameters = GetCommandParams<CreateMaterialCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.CreateMaterial, parameters);
						}
					case RenderCommandType.CreateRenderTarget:
						{
							var parameters = GetCommandParams<CreateRenderTargetCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.CreateRenderTarget, parameters);
						}
					case RenderCommandType.CreateTexture:
						{
							var parameters = GetCommandParams<CreateTextureCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.CreateTexture, parameters);
						}
					case RenderCommandType.CreateSceneComponent:
						{
							var parameters = GetCommandParams<CreateSceneComponentCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.CreateSceneComponent, parameters);
						}
					case RenderCommandType.DestroyMaterial:
						{
							var parameters = GetCommandParams<DestroyMaterialCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.DestroyMaterial, parameters);
						}
					case RenderCommandType.DestroyRenderTarget:
						{
							var parameters = GetCommandParams<DestroyRenderTargetCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.DestroyRenderTarget, parameters);
						}
					case RenderCommandType.DestroyTexture:
						{
							var parameters = GetCommandParams<DestroyTextureCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.DestroyTexture, parameters);
						}
					case RenderCommandType.DestroySceneComponent:
						{
							var parameters = GetCommandParams<DestroySceneComponentCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.DestroySceneComponent, parameters);
						}
					case RenderCommandType.MultipleCompose:
						{
							var parameters = GetCommandParams<MultipleComposeCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.MultipleCompose, parameters);
						}
					case RenderCommandType.Compose:
						{
							var parameters = GetCommandParams<ComposeCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.Compose, parameters);
						}
					case RenderCommandType.Copy:
						{
							var parameters = GetCommandParams<CopyCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.Copy, parameters);
						}
					case RenderCommandType.GenerateNormalTexture:
						{
							var parameters = GetCommandParams<GenerateNormalTextureCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.GenerateNormalTexture, parameters);
						}
					case RenderCommandType.SetTexturePixelData:
						{
							var parameters = GetCommandParams<SetTexturePixelDataCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetTexturePixelData, parameters);
						}
					case RenderCommandType.SetMaterialScalarProperty:
						{
							var parameters = GetCommandParams<SetMaterialScalarPropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMaterialScalarProperty, parameters);
						}
					case RenderCommandType.SetMaterialVectorProperty:
						{
							var parameters = GetCommandParams<SetMaterialVectorPropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMaterialVectorProperty, parameters);
						}
					case RenderCommandType.SetMaterialRenderTargetProperty:
						{
							var parameters = GetCommandParams<SetMaterialRenderTargetPropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMaterialRenderTargetProperty, parameters);
						}
					case RenderCommandType.SetMaterialTextureProperty:
						{
							var parameters = GetCommandParams<SetMaterialTexturePropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMaterialTextureProperty, parameters);
						}
					case RenderCommandType.SetMaterialNamedTextureProperty:
						{
							var parameters = GetCommandParams<SetMaterialNamedTexturePropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMaterialNamedTextureProperty, parameters);
						}
					case RenderCommandType.GenerateMipMaps:
						{
							var parameters = GetCommandParams<GenerateMipMapsCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.GenerateMipMaps, parameters);
						}
					case RenderCommandType.SetVisible:
						{
							var parameters = GetCommandParams<SetVisibleCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetVisible, parameters);
						}
					case RenderCommandType.SetMaterial:
						{
							var parameters = GetCommandParams<SetMaterialCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMaterial, parameters);
						}
					case RenderCommandType.SetMesh:
						{
							var parameters = GetCommandParams<SetMeshCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMesh, parameters);
						}
					case RenderCommandType.SetMeshBuffer:
						{
							var parameters = GetCommandParams<SetMeshBufferCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetMeshBuffer, parameters);
						}
					case RenderCommandType.SetSceneComponentPivot:
						{
							var parameters = GetCommandParams<SetSceneComponentPivotCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.SetSceneComponentPivot, parameters);
						}
					case RenderCommandType.CommandGroupBegin:
						{
							var parameters = GetCommandParams<NullCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.CommandGroupBegin, parameters);
						}
					case RenderCommandType.CommandGroupEnd:
						{
							var parameters = GetCommandParams<NullCommandParameters>(rawRenderCommands);
							return new RenderCommand(RenderCommandType.CommandGroupEnd, parameters);
						}
					default:
						Debug.Fail("Cannot decode unknown renderCommand type {commandType}");
						break;
				}
			}

			return null;
		}

		private RenderCommandType GetCommandType(Unity.DataBuffer<byte> dataBuffer)
		{
			RenderCommandType commandType;
			System.IntPtr unmanagedElement = new System.IntPtr(dataBuffer.Data.ToInt64() + (long)currentOffset);

			unsafe
			{
				commandType = *((RenderCommandType*)unmanagedElement.ToPointer());
			}

			var typeSize = (ulong)sizeof(RenderCommandType);
			currentOffset += typeSize;

			return commandType;
		}

		private T GetCommandParams<T>(Unity.DataBuffer<byte> dataBuffer)
		{
			var typeSize = (ulong)Marshal.SizeOf(typeof(T));
			
			System.IntPtr unmanagedElement = new System.IntPtr(dataBuffer.Data.ToInt64() + (long)currentOffset);
			var commandParameters = Marshal.PtrToStructure<T>(unmanagedElement);

			currentOffset += typeSize;

			return commandParameters;
		}		
	}
}
