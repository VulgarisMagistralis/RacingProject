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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Esri
{
	public static class Extensions
	{
		private static int[] ToIntArray(IntPtr unmanagedArray, ulong sizeBytes)
		{
			Debug.Assert(unmanagedArray != IntPtr.Zero);
			Debug.Assert(sizeBytes >= sizeof(int));

			var result = new int[sizeBytes / sizeof(int)];
			Marshal.Copy(unmanagedArray, result, 0, (int)sizeBytes / sizeof(int));
			return result;
		}

		private static long[] ToLongArray(IntPtr unmanagedArray, ulong sizeBytes)
		{
			Debug.Assert(unmanagedArray != IntPtr.Zero);
			Debug.Assert(sizeBytes >= sizeof(long));

			var result = new long[sizeBytes / sizeof(long)];
			Marshal.Copy(unmanagedArray, result, 0, (int)sizeBytes / sizeof(long));
			return result;
		}

		internal static bool IsNullOrEmpty<T>(this Unity.DataBuffer<T> dataBuffer) where T : struct
		{
			return dataBuffer == null ||
				dataBuffer.Handle == IntPtr.Zero ||
				dataBuffer.SizeBytes == 0 ||
				dataBuffer.SizeBytes < dataBuffer.ItemSize;
		}

		public static byte[] ToArray(this Unity.DataBuffer<byte> dataBuffer)
		{
			if (dataBuffer.IsNullOrEmpty())
			{
				return Array.Empty<byte>();
			}

			var result = new byte[dataBuffer.SizeBytes / sizeof(byte)];
			Marshal.Copy(dataBuffer.Data, result, 0, (int)dataBuffer.SizeBytes / sizeof(byte));
			return result;
		}

		public static float[] ToArray(this Unity.DataBuffer<float> dataBuffer)
		{
			if (dataBuffer.IsNullOrEmpty())
			{
				return Array.Empty<float>();
			}

			var result = new float[dataBuffer.SizeBytes / sizeof(float)];
			Marshal.Copy(dataBuffer.Data, result, 0, (int)dataBuffer.SizeBytes / sizeof(float));
			return result;
		}

		public static int[] ToArray(this Unity.DataBuffer<int> dataBuffer)
		{
			if (dataBuffer.IsNullOrEmpty())
			{
				return Array.Empty<int>();
			}

			return ToIntArray(dataBuffer.Data, dataBuffer.SizeBytes);
		}

		public static long[] ToArray(this Unity.DataBuffer<long> dataBuffer)
		{
			if (dataBuffer.IsNullOrEmpty())
			{
				return Array.Empty<long>();
			}

			return ToLongArray(dataBuffer.Data, dataBuffer.SizeBytes);
		}

		public static uint[] ToArray(this Unity.DataBuffer<uint> dataBuffer)
		{
			if (dataBuffer.IsNullOrEmpty())
			{
				return Array.Empty<uint>();
			}

			return Array.ConvertAll(ToIntArray(dataBuffer.Data, dataBuffer.SizeBytes), l => (uint)l);
		}

		public static ulong[] ToArray(this Unity.DataBuffer<ulong> dataBuffer)
		{
			if (dataBuffer.IsNullOrEmpty())
			{
				return Array.Empty<ulong>();
			}

			return Array.ConvertAll(ToLongArray(dataBuffer.Data, dataBuffer.SizeBytes), l => (ulong)l);
		}

		public static T[] ToArray<T>(this Unity.DataBuffer<T> dataBuffer) where T : struct
		{
			if (dataBuffer.IsNullOrEmpty())
			{
				return Array.Empty<T>();
			}

			var typeSize = (ulong)Marshal.SizeOf(typeof(T));
			var length = dataBuffer.SizeBytes / typeSize;
			T[] result = new T[length];

			Debug.Assert(dataBuffer.ItemSize == typeSize);
			Debug.Assert(dataBuffer.SizeBytes % typeSize == 0);

			var unmanagedArray = dataBuffer.Data;

			for (ulong i = 0; i < length; i++)
			{
				IntPtr unmanagedElement = new IntPtr(unmanagedArray.ToInt64() + (long)(i * typeSize));
				result[i] = Marshal.PtrToStructure<T>(unmanagedElement);
			}
			return result;
		}
	}
}
