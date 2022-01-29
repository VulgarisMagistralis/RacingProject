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
using System.Collections;
using System.Collections.Generic;

namespace Esri.Unity
{
    public class Dictionary<TKey, TValue>
    {
        private Standard.IntermediateDictionary<TKey, TValue> intermediateDictionary;

        internal IntPtr Handle
        {
            get
            {
                if (intermediateDictionary != null)
                {
                    return intermediateDictionary.Handle;
                }

                return IntPtr.Zero;
            }
            set
            {
                if (intermediateDictionary != null)
                {
                    intermediateDictionary.Handle = value;
                }
            }
        }

        public int Count
        {
            get
            {
                return (int)intermediateDictionary.Size;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return intermediateDictionary.At(key);
            }
            set
            {
                if (intermediateDictionary.Contains(key))
                {
                    intermediateDictionary.Replace(key, value);
                }
                else
                {
                    intermediateDictionary.Insert(key, value);
                }
            }
        }

        internal Dictionary(IntPtr handle)
        {
            intermediateDictionary = new Standard.IntermediateDictionary<TKey, TValue>(handle);
        }

        public void Add(TKey key, TValue value)
        {
            this[key] = value;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this[item.Key] = item.Value;
        }

        public void Clear()
        {
            intermediateDictionary.RemoveAll();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var result = intermediateDictionary.At(item.Key);

            return result != null ? result.Equals(item.Value) : false;
        }

        public bool ContainsKey(TKey key)
        {
            return intermediateDictionary.Contains(key);
        }

        public bool Remove(TKey key)
        {
            if (intermediateDictionary.Contains(key))
            {
                intermediateDictionary.Remove(key);

                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var result = intermediateDictionary.At(item.Key);

            if (result != null && result.Equals(item.Value))
            {
                intermediateDictionary.Remove(item.Key);
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (intermediateDictionary.Contains(key))
            {
                value = intermediateDictionary.At(key);

                return true;
            }

            value = default;

            return false;
        }
    }
}
