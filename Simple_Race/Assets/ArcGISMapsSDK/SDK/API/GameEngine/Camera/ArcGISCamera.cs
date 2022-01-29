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
using Esri.GameEngine.Location;

namespace Esri.GameEngine.Camera
{
	public class ArcGISCamera
	{
		public string Name { get; private set; }

		public ArcGISRotation Orientation { get; private set; }

		public ArcGISPosition Position { get; private set; }

		public ArcGISCamera(string name, ArcGISPosition position, ArcGISRotation orientation)
		{
			Name = name;
			Orientation = orientation;
			Position = position;
		}
	}
}
