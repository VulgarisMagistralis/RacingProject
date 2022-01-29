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
namespace Esri.ArcGISMapsSDK.Utils.Math
{
	public class Plane
	{
		public Vector3d normal;
		public Vector3d point;
		public double d;

		public Plane(Vector3d normal, Vector3d point)
		{
			this.normal = normal;
			this.point = point;
			d = -Vector3d.Dot(normal, point);
		}
	}
}
