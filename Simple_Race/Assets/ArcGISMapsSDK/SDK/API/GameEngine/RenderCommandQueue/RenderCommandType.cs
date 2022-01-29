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

namespace Esri.GameEngine.RenderCommandQueue
{
    public enum RenderCommandType
    {
        /// Create material
        /// 
        /// - Since: 100.12.0
        CreateMaterial = 0,
        
        /// Create render target
        /// 
        /// - Since: 100.12.0
        CreateRenderTarget = 1,
        
        /// Create texture
        /// 
        /// - Since: 100.12.0
        CreateTexture = 2,
        
        /// Create scene component
        /// 
        /// - Since: 100.12.0
        CreateSceneComponent = 3,
        
        /// Destroy material
        /// 
        /// - Since: 100.12.0
        DestroyMaterial = 4,
        
        /// Destroy render target
        /// 
        /// - Since: 100.12.0
        DestroyRenderTarget = 5,
        
        /// Destroy texture
        /// 
        /// - Since: 100.12.0
        DestroyTexture = 6,
        
        /// Destroy scene component
        /// 
        /// - Since: 100.12.0
        DestroySceneComponent = 7,
        
        /// Multiple compose
        /// 
        /// - Since: 100.12.0
        MultipleCompose = 8,
        
        /// Compose
        /// 
        /// - Since: 100.12.0
        Compose = 9,
        
        /// Copy
        /// 
        /// - Since: 100.12.0
        Copy = 10,
        
        /// Generate normal texture
        /// 
        /// - Since: 100.12.0
        GenerateNormalTexture = 11,
        
        /// Set the pixel data of a texture
        /// 
        /// - Since: 100.12.0
        SetTexturePixelData = 12,
        
        /// Set the value of a material's scalar property
        /// 
        /// - Since: 100.12.0
        SetMaterialScalarProperty = 13,
        
        /// Set the value of a material's vector property
        /// 
        /// - Since: 100.12.0
        SetMaterialVectorProperty = 14,
        
        /// Set the value of a material's render target property
        /// 
        /// - Since: 100.12.0
        SetMaterialRenderTargetProperty = 15,
        
        /// Set the value of a material's texture property
        /// 
        /// - Since: 100.12.0
        SetMaterialTextureProperty = 16,
        
        /// Generate MipMaps
        /// 
        /// - Since: 100.12.0
        GenerateMipMaps = 17,
        
        /// Set visible
        /// 
        /// - Since: 100.12.0
        SetVisible = 18,
        
        /// Set material
        /// 
        /// - Since: 100.12.0
        SetMaterial = 19,
        
        /// Set mesh
        /// 
        /// - Since: 100.12.0
        SetMesh = 20,
        
        /// Set mesh buffer
        /// 
        /// - Since: 100.12.0
        SetMeshBuffer = 21,
        
        /// Set the pivot of a scene component
        /// 
        /// - Since: 100.12.0
        SetSceneComponentPivot = 22,
        
        /// Set a named texture on a material
        /// 
        /// - Since: 100.12.0
        SetMaterialNamedTextureProperty = 23,
        
        /// Mark the start of a group of commands that should be executed atomically
        /// 
        /// - Since: 100.12.0
        CommandGroupBegin = 24,
        
        /// Mark the end of a group of commands that should be executed atomically
        /// 
        /// - Since: 100.12.0
        CommandGroupEnd = 25
    };
}