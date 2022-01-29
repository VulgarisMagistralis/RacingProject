// COPYRIGHT 1995-2021 ESRI
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

namespace Esri.Unity
{
	public class ImmutableArray<T>
	{
		#region Properties
		/// The type of the array.
		///
		/// - Remark: The type of the array object.
		/// - SeeAlso: ArrayType
		/// - Since: 100.0.0
		public Standard.ArrayType ObjectType
		{
			get
			{
				return intermediateImmutableArray.ObjectType;
			}
		}

		/// Determines the number of values in the array.
		///
		/// - Remark: The number of values in the array. If an error occurs a 0 will be returned.
		/// - Since: 100.0.0
		public ulong Size
		{
			get
			{
				return intermediateImmutableArray.Size;
			}
		}

		/// The type of the values this array holds.
		///
		/// - Since: 100.0.0
		public Standard.ElementType ValueType
		{
			get
			{
				return intermediateImmutableArray.ValueType;
			}
		}
		#endregion // Properties

		#region Methods
		/// Get a value at a specific position.
		///
		/// - Remark: Retrieves the value at the specified position.
		/// - Parameter position: The position which you want to get the value.
		/// - Returns: The value, Element, at the position requested.
		/// - Since: 100.0.0
		public T At(ulong position)
		{
			return intermediateImmutableArray.At(position);
		}

		/// Does the array contain the given value.
		///
		/// - Remark: Does the array contain a specific value.
		/// - Parameter value: The value you want to find.
		/// - Returns: True if the value is in the array otherwise false.
		/// - Since: 100.0.0
		public bool Contains(T value)
		{
			return intermediateImmutableArray.Contains(value);
		}

		/// Creates a ArrayBuilder.
		///
		/// - Parameter valueType: The type of the values the returned ArrayBuilder holds.
		/// - Returns: A ArrayBuilder
		/// - SeeAlso: ArrayBuilder
		/// - Since: 100.9.0
		public static ImmutableArrayBuilder<T> CreateBuilder()
		{
			var intermediateImmutableArray = Standard.IntermediateImmutableArray<T>.CreateBuilder(Convert.ToElementType<T>());

			var handle = intermediateImmutableArray.Handle;

			intermediateImmutableArray.Handle = IntPtr.Zero;

			return new ImmutableArrayBuilder<T>(handle);
		}

		/// Returns true if the two arrays are equal, false otherwise.
		///
		/// - Parameter array2: The second array.
		/// - Returns: Returns true if the two arrays are equal, false otherwise.
		/// - Since: 100.0.0
		public bool Equals(Unity.ImmutableArray<T> array2)
		{
			return intermediateImmutableArray.Equals(array2);
		}

		/// Get the first value in the array.
		///
		/// - Returns: The value, Element, at the position requested.
		/// - Since: 100.0.0
		public T First()
		{
			return intermediateImmutableArray.First();
		}

		/// Retrieves the position of the given value in the array.
		///
		/// - Parameter value: The value you want to find.
		/// - Returns: The position of the value in the array, or the max value of size_t otherwise.
		/// - Since: 100.0.0
		public ulong IndexOf(T value)
		{
			return intermediateImmutableArray.IndexOf(value);
		}

		/// Determines if there are any values in the array.
		///
		/// - Remark: Check if the array object has any values in it.
		/// - Returns: true if the  array object contains no values otherwise false. Will return true if an error occurs.
		/// - Since: 100.0.0
		public bool IsEmpty()
		{
			return intermediateImmutableArray.IsEmpty();
		}

		/// Get the last value in the array.
		///
		/// - Returns: The value, Element, at the position requested.
		/// - Since: 100.0.0
		public T Last()
		{
			return intermediateImmutableArray.Last();
		}
		#endregion // Methods

		#region Internal Members
		public ImmutableArray(IntPtr handle)
		{
			intermediateImmutableArray = new Standard.IntermediateImmutableArray<T>(handle);
		}

		internal IntPtr Handle
		{
			get
			{
				if (intermediateImmutableArray != null)
				{
					return intermediateImmutableArray.Handle;
				}

				return IntPtr.Zero;
			}
			set
			{
				if (intermediateImmutableArray != null)
				{
					intermediateImmutableArray.Handle = value;
				}
			}
		}

		private Standard.IntermediateImmutableArray<T> intermediateImmutableArray;
		#endregion // Internal Members
	}
}
