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
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Utils.Math;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.GPUComputing
{
	internal class NormalMapGeneratorCS : INormalMapGenerator
	{
		private readonly ComputeShader shader = null;

		public NormalMapGeneratorCS()
		{
			shader = Resources.Load<ComputeShader>("Shaders/Utils/CS/ComputeNormals");
		}

		public void Compute(GPUResourceTexture2D inputElevation, Vector4 tileExtension, Vector4 textureExtension, GPUResourceRenderTexture output)
		{
			double tileX = tileExtension.x / (tileExtension.z - tileExtension.x);
			double tileY = tileExtension.y / (tileExtension.w - tileExtension.y);
			uint tileLod = (uint)Mathf.Log(1.0f / (tileExtension.z - tileExtension.x), 2);

			var min = GeoUtils.WebMercatorTileToWGS84LatLon(tileX, tileY, tileLod);
			var max = GeoUtils.WebMercatorTileToWGS84LatLon(tileX + 1, tileY + 1, tileLod);

			var circleLongitude = 2.0 * System.Math.PI * GeoUtils.EarthRadius;
			double minLatitude = min.Latitude * MathUtils.DegreesToRadians;
			double latitudeAngleDelta = ((max.Latitude - min.Latitude) / output.Height) * MathUtils.DegreesToRadians;
			double longitudeArc = (System.Math.Abs(max.Latitude - min.Latitude) / 360.0);

			double latitudeLength = circleLongitude * (System.Math.Abs(max.Longitude - min.Longitude) / 360.0);

			int kernelHandle = shader.FindKernel("CSMain");

			uint x, y, z;
			shader.GetKernelThreadGroupSizes(kernelHandle, out x, out y, out z);

			shader.SetTexture(kernelHandle, "Output", output.NativeRenderTexture);
			shader.SetTexture(kernelHandle, "Input", inputElevation.NativeTexture);
			shader.SetFloat("MinLatitude", (float)minLatitude);
			shader.SetFloat("LatitudeAngleDelta", (float)latitudeAngleDelta);
			shader.SetFloat("LongitudeArc", (float)longitudeArc);
			shader.SetFloat("LatitudeLength", (float)latitudeLength);
			shader.SetFloat("CircleLongitude", (float)circleLongitude);
			shader.SetFloat("EarthRadius", (float)GeoUtils.EarthRadius);
			shader.SetVector("InputOffsetAndScale", new Vector4(textureExtension.x, textureExtension.y, textureExtension.z, textureExtension.w));

			shader.Dispatch(kernelHandle, (int)System.Math.Ceiling(output.Width / (float)x), (int)System.Math.Ceiling(output.Height / (float)y), 1);
		}
	}
}
