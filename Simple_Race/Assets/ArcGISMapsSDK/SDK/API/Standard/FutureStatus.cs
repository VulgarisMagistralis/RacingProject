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

namespace Esri.Standard
{
    public enum FutureStatus
    {
        /// The Future has completed.
        /// 
        /// - Since: 100.0.0
        Completed = 0,
        
        /// The Future was canceled.
        /// 
        /// - Since: 100.0.0
        Canceled = 1,
        
        /// The Future has not completed and is not canceled.
        /// 
        /// - Since: 100.0.0
        NotComplete = 2,
        
        /// The Future status is unknown. Used for error conditions.
        /// 
        /// - Since: 100.0.0
        Unknown = -1
    };
}