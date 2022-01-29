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
using Esri.ArcGISMapsSDK.Renderer.GPUResources;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.GPUComputing
{
	internal class ImageComposerCS : IImageComposer
	{
		private readonly ComputeShader shader = null;

		public ImageComposerCS()
		{
			shader = Resources.Load<ComputeShader>("Shaders/Utils/CS/BlendImage");
		}

		public void Compose(ComposableImage[] inputImages, GPUResourceRenderTexture output)
		{
			int kernelHandle = shader.FindKernel("CSMain");

			uint x, y, z;
			shader.GetKernelThreadGroupSizes(kernelHandle, out x, out y, out z);

			shader.SetTexture(kernelHandle, "Output", output.NativeRenderTexture);

			int numIterations = inputImages.Length / 8 + (inputImages.Length % 8 == 0 ? 0 : 1);
			float[] opacities = new float[8];
			Vector4[] offsets = new Vector4[8];

			for (int i = 0; i < numIterations; i++)
			{
				int numTexturesPerIteration = i == numIterations - 1 ? inputImages.Length % 8 : 8;

				for (int tex = 0; tex < 8; tex++)
				{
					var texture = Texture2D.blackTexture;

					if (tex < numTexturesPerIteration)
					{
						opacities[tex] = inputImages[(8 * i + tex)].opacity;
						offsets[tex] = inputImages[(8 * i + tex)].extent;

						texture = inputImages[(8 * i + tex)].image.NativeTexture;						
					}

					if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Vulkan ||
							SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal ||
							SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore)
					{
						shader.SetTexture(kernelHandle, "Input_" + (8 * i + tex) + "_", texture);
					}
					else
					{
						shader.SetTexture(kernelHandle, "Input[" + (8 * i + tex) + "]", texture);
					}
				}

				shader.SetFloats("Opacities", opacities);
				shader.SetVectorArray("OffsetsAndScales", offsets);
				shader.SetInt("NumTextures", numTexturesPerIteration);

				shader.Dispatch(kernelHandle, (int)System.Math.Ceiling(output.Width / (float)x), (int)System.Math.Ceiling(output.Height / (float)y), 1);
			}
		}
	}
}
