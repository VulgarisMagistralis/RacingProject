// COPYRIGHT 1995-2020 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Environmental Systems Research Institute, Inc.
// Attn: Contracts and Legal Services Department
// 380 New York Street
// Redlands, California, 92373
// USA
//
// email: contracts@esri.com

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Esri
{
	internal static partial class Interop
	{
		// Keep material reference from being GC'd. Workaround for https://devtopia.esri.com/runtime/millennium-falcon/issues/1249
		private static Dictionary<IntPtr, UnityEngine.Material> keepAlive = new Dictionary<IntPtr, UnityEngine.Material>();

		internal static UnityEngine.Material FromUnityMaterial(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				return null;
			}
			GCHandle gcHandle = GCHandle.FromIntPtr(value);
			return (UnityEngine.Material)gcHandle.Target;
		}

		internal static IntPtr ToUnityMaterial(UnityEngine.Material value)
		{
			if(value == null)
			{
				return IntPtr.Zero;
			}
			IntPtr ptr = GCHandle.ToIntPtr(GCHandle.Alloc(value));
			keepAlive[ptr] = value; 
			return ptr;
		}
	}
}
