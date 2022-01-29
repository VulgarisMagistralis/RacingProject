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

using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Esri
{
	internal static partial class Interop
	{
		internal static global::Unity.Collections.NativeArray<byte> FromByteArrayStruct(Standard.IntermediateByteArrayStruct value)
		{
			unsafe
			{

				var nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(value.Data.ToPointer(), (int)value.Size, Allocator.None);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
				NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeArray, AtomicSafetyHandle.Create());
#endif
				return nativeArray;
			}
		}
	}
}
