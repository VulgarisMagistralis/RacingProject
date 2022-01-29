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
	public partial class ImmutableArrayBuilder<T>
	{
		#region Properties
		/// The type of the values this ArrayBuilder holds.
		///
		/// - Since: 100.9.0
		public Standard.ElementType ValueType
		{
			get
			{
				return intermediateImmutableArrayBuilder.ValueType;
			}
		}
		#endregion // Properties

		#region Methods
		/// Add a value to the ArrayBuilder.
		///
		/// - Parameter value: The value that is to be added.
		/// - Since: 100.9.0
		public void Add(T value)
		{
			intermediateImmutableArrayBuilder.Add(value);
		}

		/// Creates a Array containing the values added to this ArrayBuilder.
		///
		/// - Remark: The order of the values in the returned Array matches the order in which the
		/// values were added to this ArrayBuilder.
		///
		/// This call empties this ArrayBuilder of values, but leaves it in a valid
		/// (re-usable) state.
		/// - Returns: A Array containing the values added to this ArrayBuilder.
		/// - Since: 100.9.0
		public Unity.ImmutableArray<T> MoveToArray()
		{
			return intermediateImmutableArrayBuilder.MoveToArray();
		}
		#endregion // Methods

		#region Internal Members

		public ImmutableArrayBuilder(IntPtr handle)
		{
			intermediateImmutableArrayBuilder = new Standard.IntermediateImmutableArrayBuilder<T>(handle);
		}

		internal IntPtr Handle
		{
			get
			{
				if (intermediateImmutableArrayBuilder != null)
				{
					return intermediateImmutableArrayBuilder.Handle;
				}

				return IntPtr.Zero;
			}
			set
			{
				if (intermediateImmutableArrayBuilder != null)
				{
					intermediateImmutableArrayBuilder.Handle = value;
				}
			}
		}

		private Standard.IntermediateImmutableArrayBuilder<T> intermediateImmutableArrayBuilder;
		#endregion // Internal Members
	}
}
