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
    public enum RuntimeClient
    {
        /// Unity.
        /// 
        /// - Since: 100.9.0
        Unity = 1,
        
        /// Unreal Engine.
        /// 
        /// - Since: 100.9.0
        Unreal = 2,
        
        /// Unknown runtime client.
        /// 
        /// - Since: 100.9.0
        Unknown = -1
    };
}