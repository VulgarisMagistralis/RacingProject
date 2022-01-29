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

namespace Esri.GameEngine.RenderCommandQueue
{
	internal class RenderCommand
	{
		public ValueType CommandParameters
		{
			get;
		}

		public RenderCommandType Type
		{
			get;
		}

		public RenderCommand(RenderCommandType type, ValueType commandParameters)
		{
			CommandParameters = commandParameters;
			Type = type;
		}
	}
}
