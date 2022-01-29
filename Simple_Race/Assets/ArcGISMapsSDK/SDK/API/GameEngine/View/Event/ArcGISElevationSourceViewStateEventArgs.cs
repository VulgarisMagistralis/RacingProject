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
using System;

namespace Esri.GameEngine.View.Event
{
	public class ArcGISElevationSourceViewStateEventArgs : EventArgs
	{
		public Esri.GameEngine.Elevation.Base.ArcGISElevationSource ArcGISElevationSource { get; }

		public State.ArcGISElevationSourceViewStatus Status { get; }

		public Exception Error { get; }

		internal ArcGISElevationSourceViewStateEventArgs(Esri.GameEngine.Elevation.Base.ArcGISElevationSource elevationSource, State.ArcGISElevationSourceViewStatus status, Exception error)
		{
			ArcGISElevationSource = elevationSource;
			Status = status;
			Error = error;
		}
	}
}
