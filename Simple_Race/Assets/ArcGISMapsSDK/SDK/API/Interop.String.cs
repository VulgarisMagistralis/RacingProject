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
using System.Runtime.InteropServices;

namespace Esri
{
	internal static partial class Interop
	{
		internal static string FromString(IntPtr interopString)
		{
			var errorHandler = ErrorManager.CreateHandler();

			var stringPtr = RT_String_cStr(interopString, errorHandler);

			ErrorManager.CheckError(errorHandler);

			var result = Marshal.PtrToStringAnsi(stringPtr);

			errorHandler = ErrorManager.CreateHandler();

			RT_String_destroy(interopString, errorHandler);

			ErrorManager.CheckError(errorHandler);

			return result;
		}

		[DllImport(Interop.Dll)]
		private static extern IntPtr RT_String_cStr(IntPtr handle, IntPtr errorHandler);

		[DllImport(Interop.Dll)]
		private static extern void RT_String_destroy(IntPtr handle, IntPtr errorHandler);
	}
}
