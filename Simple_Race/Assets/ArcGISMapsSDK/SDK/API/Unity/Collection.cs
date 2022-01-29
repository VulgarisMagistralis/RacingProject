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

using System.Runtime.InteropServices;
using System;

namespace Esri.Unity
{
    [StructLayout(LayoutKind.Sequential)]
    public class Collection<T>
    {
        #region Internal Members
        internal Collection(IntPtr handle) => Handle = handle;
    
        ~Collection()
        {
            if (typeof (T) == typeof(GameEngine.Elevation.Base.ArcGISElevationSource))
            {
                new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(Handle);
            }
            else if (typeof (T) == typeof(GameEngine.Layers.Base.ArcGISLayer))
            {
                new GameEngine.Layers.Base.ArcGISLayerCollection(Handle);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    
        internal IntPtr Handle { get; private set; }
        #endregion // Internal Members
    }
    
    public static class CollectionSpecialization
    {
        public static ulong Add(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, GameEngine.Elevation.Base.ArcGISElevationSource value)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.Add(value);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static ulong AddArray(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, Collection<GameEngine.Elevation.Base.ArcGISElevationSource> vector2)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.AddArray(vector2);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static GameEngine.Elevation.Base.ArcGISElevationSource At(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, ulong position)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.At(position);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static bool Contains(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, GameEngine.Elevation.Base.ArcGISElevationSource value)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.Contains(value);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static bool Equals(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, Collection<GameEngine.Elevation.Base.ArcGISElevationSource> vector2)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.Equals(vector2);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static GameEngine.Elevation.Base.ArcGISElevationSource First(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.First();
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static ulong GetSize(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.Size;
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static ulong IndexOf(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, GameEngine.Elevation.Base.ArcGISElevationSource value)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.IndexOf(value);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static void Insert(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, ulong position, GameEngine.Elevation.Base.ArcGISElevationSource value)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	collection.Insert(position, value);
        
        	collection.Handle = IntPtr.Zero;
        }
        
        public static bool IsEmpty(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.IsEmpty();
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static GameEngine.Elevation.Base.ArcGISElevationSource Last(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	var result = collection.Last();
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static void Move(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, ulong oldPosition, ulong newPosition)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	collection.Move(oldPosition, newPosition);
        
        	collection.Handle = IntPtr.Zero;
        }
        
        public static ulong Npos(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self)
        {
        	return GameEngine.Elevation.Base.ArcGISElevationSourceCollection.Npos();
        }
        
        public static void Remove(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self, ulong position)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	collection.Remove(position);
        
        	collection.Handle = IntPtr.Zero;
        }
        
        public static void RemoveAll(this Collection<GameEngine.Elevation.Base.ArcGISElevationSource> self)
        {
        	var collection = new GameEngine.Elevation.Base.ArcGISElevationSourceCollection(self.Handle);
        
        	collection.RemoveAll();
        
        	collection.Handle = IntPtr.Zero;
        }
        
        public static ulong Add(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, GameEngine.Layers.Base.ArcGISLayer value)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.Add(value);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static ulong AddArray(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, Collection<GameEngine.Layers.Base.ArcGISLayer> vector2)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.AddArray(vector2);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static GameEngine.Layers.Base.ArcGISLayer At(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, ulong position)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.At(position);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static bool Contains(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, GameEngine.Layers.Base.ArcGISLayer value)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.Contains(value);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static bool Equals(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, Collection<GameEngine.Layers.Base.ArcGISLayer> vector2)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.Equals(vector2);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static GameEngine.Layers.Base.ArcGISLayer First(this Collection<GameEngine.Layers.Base.ArcGISLayer> self)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.First();
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static ulong GetSize(this Collection<GameEngine.Layers.Base.ArcGISLayer> self)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.Size;
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static ulong IndexOf(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, GameEngine.Layers.Base.ArcGISLayer value)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.IndexOf(value);
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static void Insert(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, ulong position, GameEngine.Layers.Base.ArcGISLayer value)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	collection.Insert(position, value);
        
        	collection.Handle = IntPtr.Zero;
        }
        
        public static bool IsEmpty(this Collection<GameEngine.Layers.Base.ArcGISLayer> self)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.IsEmpty();
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static GameEngine.Layers.Base.ArcGISLayer Last(this Collection<GameEngine.Layers.Base.ArcGISLayer> self)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	var result = collection.Last();
        
        	collection.Handle = IntPtr.Zero;
        
        	return result;
        }
        
        public static void Move(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, ulong oldPosition, ulong newPosition)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	collection.Move(oldPosition, newPosition);
        
        	collection.Handle = IntPtr.Zero;
        }
        
        public static ulong Npos(this Collection<GameEngine.Layers.Base.ArcGISLayer> self)
        {
        	return GameEngine.Layers.Base.ArcGISLayerCollection.Npos();
        }
        
        public static void Remove(this Collection<GameEngine.Layers.Base.ArcGISLayer> self, ulong position)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	collection.Remove(position);
        
        	collection.Handle = IntPtr.Zero;
        }
        
        public static void RemoveAll(this Collection<GameEngine.Layers.Base.ArcGISLayer> self)
        {
        	var collection = new GameEngine.Layers.Base.ArcGISLayerCollection(self.Handle);
        
        	collection.RemoveAll();
        
        	collection.Handle = IntPtr.Zero;
        }
    }
}