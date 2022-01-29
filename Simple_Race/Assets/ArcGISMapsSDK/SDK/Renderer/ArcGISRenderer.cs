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
using Esri.GameEngine.View;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer
{
	public class ArcGISRenderer
	{
		private readonly RenderCommandClient renderCommandClient;
		private readonly ArcGISRendererView rendererView;
		private readonly INormalMapGenerator normalMapGenerator;
		private readonly IImageComposer imageComposer;
		private readonly GPUResourcesProvider gpuResourceProvider = new GPUResourcesProvider();
		private readonly SceneComponentProvider sceneComponentProvider;

		private readonly RenderCommandThrottle renderCommandThrottle = new RenderCommandThrottle();
		private static readonly bool throttlingManagerEnabled = true;

		private DecodedRenderCommandQueue currentRenderCommandQueue;

		public ArcGISRenderer(ArcGISRendererView rendererView, GameObject gameObject)
		{
			if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 ||
					SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal)
			{
				normalMapGenerator = new NormalMapGeneratorPS();
				imageComposer = new ImageComposerPS();
			}
			else
			{
				normalMapGenerator = new NormalMapGeneratorCS();
				imageComposer = new ImageComposerCS();
			}

			this.rendererView = rendererView;
			sceneComponentProvider = new SceneComponentProvider(500, gameObject);
			renderCommandClient = new RenderCommandClient(gpuResourceProvider, sceneComponentProvider, imageComposer, normalMapGenerator);

			currentRenderCommandQueue = rendererView.GetCurrentDecodedRenderCommandQueue();
		}

		public void Update()
		{
			if (throttlingManagerEnabled)
			{
				renderCommandThrottle.Clear();
				RenderCommand renderCommand = currentRenderCommandQueue.GetNextCommand();

				do
				{
					if (renderCommand != null)
					{
						renderCommandClient.ExecuteRenderCommand(renderCommand);
						if (renderCommandThrottle.DoThrottle(renderCommand))
						{
							// Break and defer processing of the remaining commands to next Update
							break;
						}

						renderCommand = currentRenderCommandQueue.GetNextCommand();
					}
					else
					{
						currentRenderCommandQueue = rendererView.GetCurrentDecodedRenderCommandQueue();
						renderCommand = currentRenderCommandQueue.GetNextCommand();
					}
				}
				while (renderCommand != null);
			}
			else
			{
				currentRenderCommandQueue = rendererView.GetCurrentDecodedRenderCommandQueue();

				for (var renderCommand = currentRenderCommandQueue.GetNextCommand(); renderCommand != null; renderCommand = currentRenderCommandQueue.GetNextCommand())
				{
					renderCommandClient.ExecuteRenderCommand(renderCommand);
				}
			}
		}

		public void Release()
		{
			gpuResourceProvider.Release();
		}
	}
}
