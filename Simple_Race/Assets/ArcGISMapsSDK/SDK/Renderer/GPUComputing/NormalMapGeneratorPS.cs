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
	internal class NormalMapGeneratorPS : INormalMapGenerator
	{
		private readonly Material material = null;

		public NormalMapGeneratorPS()
		{
			material = new Material(Resources.Load<Shader>("Shaders/Utils/PS/ComputeNormals"));
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

			material.SetTexture("Input", inputElevation.NativeTexture);
			material.SetFloat("MinLatitude", (float)minLatitude);
			material.SetFloat("LatitudeAngleDelta", (float)latitudeAngleDelta);
			material.SetFloat("LongitudeArc", (float)longitudeArc);
			material.SetFloat("LatitudeLength", (float)latitudeLength);
			material.SetFloat("CircleLongitude", (float)circleLongitude);
			material.SetFloat("EarthRadius", (float)GeoUtils.EarthRadius);
			material.SetVector("InputOffsetAndScale", new Vector4(textureExtension.x, textureExtension.y, textureExtension.z, textureExtension.w));

			UnityEngine.Graphics.Blit(null, ((GPUResourceRenderTexture)output).NativeRenderTexture, material);
		}
	}
}
