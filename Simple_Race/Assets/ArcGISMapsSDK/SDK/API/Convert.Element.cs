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
	internal static partial class Convert
	{
		internal static object FromElement(Standard.Element element)
		{
			var type = element.ObjectType;

			object result;

			switch (type)
			{
				case Standard.ElementType.Attribute:
					result = element.GetValueAsAttribute();
					break;
				case Standard.ElementType.Bool:
					result = element.GetValueAsBool();
					break;
				case Standard.ElementType.Float32:
					result = element.GetValueAsFloat32();
					break;
				case Standard.ElementType.Float64:
					result = element.GetValueAsFloat64();
					break;
				case Standard.ElementType.Int16:
					result = element.GetValueAsInt16();
					break;
				case Standard.ElementType.Int32:
					result = element.GetValueAsInt32();
					break;
				case Standard.ElementType.Int64:
					result = element.GetValueAsInt64();
					break;
				case Standard.ElementType.Int8:
					result = element.GetValueAsInt8();
					break;
				case Standard.ElementType.String:
					result = element.GetValueAsString();
					break;
				case Standard.ElementType.UInt16:
					result = element.GetValueAsUInt16();
					break;
				case Standard.ElementType.UInt32:
					result = element.GetValueAsUInt32();
					break;
				case Standard.ElementType.UInt64:
					result = element.GetValueAsUInt64();
					break;
				case Standard.ElementType.UInt8:
					result = element.GetValueAsUInt8();
					break;
				case Standard.ElementType.VisualizationAttribute:
					result = element.GetValueAsVisualizationAttribute();
					break;
				case Standard.ElementType.VisualizationAttributeDescription:
					result = element.GetValueAsVisualizationAttributeDescription();
					break;
				default:
					throw new InvalidCastException();
			}

			return result;
		}

		internal static T FromElement<T>(Standard.Element element)
		{
			return (T)System.Convert.ChangeType(FromElement(element), typeof(T));
		}

		internal static Standard.Element ToElement<T>(T value)
		{
			Standard.Element result;

			switch (value)
			{
				case GameEngine.Security.ArcGISAuthenticationConfiguration converted:
					result = Standard.Element.FromArcGISAuthenticationConfiguration(converted);
					break;
				case bool converted:
					result = Standard.Element.FromBool(converted);
					break;
				case float converted:
					result = Standard.Element.FromFloat32(converted);
					break;
				case double converted:
					result = Standard.Element.FromFloat64(converted);
					break;
				case short converted:
					result = Standard.Element.FromInt16(converted);
					break;
				case int converted:
					result = Standard.Element.FromInt32(converted);
					break;
				case long converted:
					result = Standard.Element.FromInt64(converted);
					break;
				case sbyte converted:
					result = Standard.Element.FromInt8(converted);
					break;
				case string converted:
					result = Standard.Element.FromString(converted);
					break;
				case ushort converted:
					result = Standard.Element.FromUInt16(converted);
					break;
				case uint converted:
					result = Standard.Element.FromUInt32(converted);
					break;
				case ulong converted:
					result = Standard.Element.FromUInt64(converted);
					break;
				case byte converted:
					result = Standard.Element.FromUInt8(converted);
					break;
				case GameEngine.Attributes.VisualizationAttributeDescription converted:
					result = Standard.Element.FromVisualizationAttributeDescription(converted);
					break;
				default:
					throw new InvalidCastException();
			}

			return result;
		}

		internal static Standard.ElementType ToElementType<T>()
		{
			if (typeof(T) == typeof(GameEngine.Security.ArcGISAuthenticationConfiguration))
			{
				return Standard.ElementType.ArcGISAuthenticationConfiguration;
			}
			else if (typeof(T) == typeof(bool))
			{
				return Standard.ElementType.Bool;
			}
			else if (typeof(T) == typeof(float))
			{
				return Standard.ElementType.Float32;
			}
			else if (typeof(T) == typeof(double))
			{
				return Standard.ElementType.Float64;
			}
			else if (typeof(T) == typeof(short))
			{
				return Standard.ElementType.Int16;
			}
			else if (typeof(T) == typeof(int))
			{
				return Standard.ElementType.Int32;
			}
			else if (typeof(T) == typeof(long))
			{
				return Standard.ElementType.Int64;
			}
			else if (typeof(T) == typeof(sbyte))
			{
				return Standard.ElementType.Int8;
			}
			else if (typeof(T) == typeof(string))
			{
				return Standard.ElementType.String;
			}
			else if (typeof(T) == typeof(ushort))
			{
				return Standard.ElementType.UInt16;
			}
			else if (typeof(T) == typeof(uint))
			{
				return Standard.ElementType.UInt32;
			}
			else if (typeof(T) == typeof(ulong))
			{
				return Standard.ElementType.UInt64;
			}
			else if (typeof(T) == typeof(byte))
			{
				return Standard.ElementType.UInt8;
			}
			else if (typeof(T) == typeof(GameEngine.Attributes.VisualizationAttributeDescription))
			{
				return Standard.ElementType.VisualizationAttributeDescription;
			}
			else
			{
				throw new InvalidCastException();
			}
		}
	}
}
