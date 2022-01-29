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

namespace Esri.Unity
{
	[StructLayout(LayoutKind.Sequential)]
	public class DataBuffer<T> where T : struct
	{
		private GameEngine.IntermediateDataBuffer<T> intermediateDataBuffer;

		internal IntPtr Handle
		{
			get
			{
				return intermediateDataBuffer.Handle;
			}
			set
			{
				intermediateDataBuffer.Handle = value;
			}
		}

		internal ulong ItemSize
		{
			get
			{
				return intermediateDataBuffer.ItemSize;
			}
		}

		internal DataBuffer(IntPtr handle)
		{
			intermediateDataBuffer = new GameEngine.IntermediateDataBuffer<T>(handle);
		}

		internal IntPtr Data
		{
			get
			{
				return intermediateDataBuffer.Data;
			}
		}

		public ulong SizeBytes
		{
			get
			{
				return intermediateDataBuffer.SizeBytes;
			}
		}

		public static bool operator ==(DataBuffer<T> lhs, DataBuffer<T> rhs)
		{
			IntPtr lhsHandle = (object)lhs == null ? IntPtr.Zero : lhs.Handle;
			IntPtr rhsHandle = (object)rhs == null ? IntPtr.Zero : rhs.Handle;

			return lhsHandle == rhsHandle;
		}

		public static bool operator !=(DataBuffer<T> lhs, DataBuffer<T> rhs)
		{
			IntPtr lhsHandle = (object)lhs == null ? IntPtr.Zero : lhs.Handle;
			IntPtr rhsHandle = (object)rhs == null ? IntPtr.Zero : rhs.Handle;

			return lhsHandle != rhsHandle;
		}

		public override bool Equals(object obj)
		{
			return obj is DataBuffer<T> buffer && (DataBuffer<T>)obj == this;
		}

		public override int GetHashCode()
		{
			int hashCode = 732208100;
			hashCode = hashCode * -1521134295 + Handle.GetHashCode();
			return hashCode;
		}
	}
}
