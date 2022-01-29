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
using System.Collections.Generic;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.GPUComputing
{
	internal class ImageComposerPS : IImageComposer
	{
		private readonly Material material = null;

		public ImageComposerPS()
		{
			material = new Material(Resources.Load<Shader>("Shaders/Utils/PS/BlendImage"));
		}

		public void Compose(ComposableImage[] inputImages, GPUResourceRenderTexture output)
		{
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

					material.SetTexture("Input" + (8 * i + tex) , texture);
				}

				material.SetFloatArray("Opacities", opacities);
				material.SetVectorArray("OffsetsAndScales", offsets);
				material.SetInt("NumTextures", numTexturesPerIteration);

				UnityEngine.Graphics.Blit(null, ((GPUResourceRenderTexture)output).NativeRenderTexture, material);
			}
		}
	}
}
