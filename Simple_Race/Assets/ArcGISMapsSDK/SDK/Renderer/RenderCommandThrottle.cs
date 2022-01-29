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
using Esri.GameEngine.RenderCommandQueue;
using Esri.GameEngine.RenderCommandQueue.Parameters;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer
{
	class RenderCommandThrottle
	{
		public static readonly float MaxExecTimeMicroseconds = 0.005f;
		public static readonly ulong MaxResourceMemory = 8 * 1024 * 1024; // bytes
		public static readonly uint MaxComposeCommands = 100;
		public static readonly uint MaxTriangles = 25000;

		private struct Stats
		{
			public uint commandCount;
			public uint composeCount;
			public ulong resourceMemory; // bytes
			public uint triangles;
			public bool isExecutingGroup;
		}

		private Stats stats;
		private float startTime;

		public RenderCommandThrottle()
		{
			Clear();
		}

		public void Clear()
		{
			stats.commandCount = 0;
			stats.composeCount = 0;
			stats.resourceMemory = 0;
			stats.triangles = 0;
			stats.isExecutingGroup = false;
			startTime = Time.realtimeSinceStartup;
		}

		public bool DoThrottle(GameEngine.RenderCommandQueue.RenderCommand renderCommand)
		{
			// Update stats from selected commands
			++stats.commandCount;

			switch (renderCommand.Type)
			{
				case RenderCommandType.MultipleCompose:
				case RenderCommandType.Compose:
					{
						++stats.composeCount;
					}
					break;

				case RenderCommandType.SetTexturePixelData:
					{
						SetTexturePixelDataCommandParameters parameters = (SetTexturePixelDataCommandParameters)renderCommand.CommandParameters;
						stats.resourceMemory += parameters.Pixels.Size;
					}
					break;

				case RenderCommandType.SetMesh:
					{
						SetMeshCommandParameters parameters = (SetMeshCommandParameters)renderCommand.CommandParameters;
						stats.resourceMemory += parameters.Colors.Size + parameters.FeatureIndices.Size + parameters.Normals.Size + parameters.Positions.Size + parameters.Tangents.Size +
																parameters.Triangles.Size + parameters.UvRegionIds.Size + parameters.Uvs.Size;
						stats.triangles = parameters.Triangles.Size / 12;				
					}
					break;

				case RenderCommandType.SetMeshBuffer:
					{
						SetMeshBufferCommandParameters parameters = (SetMeshBufferCommandParameters)renderCommand.CommandParameters;
						stats.resourceMemory += parameters.Buffer.Size;						
					}
					break;

				case RenderCommandType.CommandGroupBegin:
					{
						stats.isExecutingGroup = true;						
					}
					break;

				case RenderCommandType.CommandGroupEnd:
					{
						stats.isExecutingGroup = false;						
					}
					break;

				default:
					break;
			}

			float ElapsedTime = Time.realtimeSinceStartup - startTime;

			if (!stats.isExecutingGroup &&
				(stats.composeCount >= MaxComposeCommands || stats.resourceMemory >= MaxResourceMemory || stats.triangles >= MaxTriangles || ElapsedTime >= MaxExecTimeMicroseconds))
			{
				return true;
			}

			return false;
		}
	}
}
