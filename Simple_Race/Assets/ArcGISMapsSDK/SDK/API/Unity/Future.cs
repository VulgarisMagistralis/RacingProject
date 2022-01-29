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

namespace Esri.Unity
{
    public class Future
    {
        private Standard.IntermediateFuture<object> intermediateFuture;

        internal Future(IntPtr handle)
        {
            intermediateFuture = new Standard.IntermediateFuture<object>(handle);
        }

        public object Get()
        {
            return intermediateFuture.Get();
        }

        public Standard.FutureCompletedEvent TaskCompleted
        {
            get
            {
                return intermediateFuture.TaskCompleted;
            }
            set
            {
                intermediateFuture.TaskCompleted = value;
            }
        }
    }

    public class Future<T>
    {
        private Standard.IntermediateFuture<T> intermediateFuture;

        internal Future(IntPtr handle)
        {
            intermediateFuture = new Standard.IntermediateFuture<T>(handle);
        }

        public T Get()
        {
            return intermediateFuture.Get();
        }

        public Standard.FutureCompletedEvent TaskCompleted
        {
            get
            {
                return intermediateFuture.TaskCompleted;
            }
            set
            {
                intermediateFuture.TaskCompleted = value;
            }
        }
    }
}
