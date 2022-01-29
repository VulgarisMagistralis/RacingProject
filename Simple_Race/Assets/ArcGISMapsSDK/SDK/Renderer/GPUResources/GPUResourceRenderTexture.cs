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
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.GPUResources
{
	internal class GPUResourceRenderTexture
	{
		public int Width
		{
			get
			{
				return NativeRenderTexture.width;
			}
		}

		public int Height
		{
			get
			{
				return NativeRenderTexture.height;
			}
		}

		public RenderTexture NativeRenderTexture { get; }

		public GPUResourceRenderTexture(RenderTexture texture)
		{
			NativeRenderTexture = texture;
		}

		public void GenerateMipMaps()
		{
			NativeRenderTexture.GenerateMips();
		}

		public void Release()
		{
			NativeRenderTexture.Release();
		}

		public void Destroy()
		{
			Object.Destroy(NativeRenderTexture);
		}
	}
}
