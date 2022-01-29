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

namespace Esri.ArcGISRuntime.MapView
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class Camera
    {
        #region Constructors
        /// Create a camera object.
        /// 
        /// - Parameters:
        ///   - latitude: The latitude of the camera position in degrees.
        ///   - longitude: The longitude of the camera position in degrees.
        ///   - altitude: The altitude of the camera position in meters.
        ///   - heading: The heading of the camera.
        ///   - pitch: The pitch of the camera. The value must be from 0 to 180 and represents the angle applied to the camera when rotating around its Y axis in the East, North, Up (ENU) ground reference frame. 0 is looking straight down towards the center of the earth, 180 looking straight up towards the sky. Negative pitches are not allowed and the values do not wrap around. If the behavior of a negative pitch is required, then the corresponding transformation with positive pitch can be set instead. For example if heading:0 pitch:-20 roll:0 is required then heading:180 pitch:20 roll:180 can be used instead.
        ///   - roll: The roll of the camera.
        /// - Since: 100.0.0
        public Camera(double latitude, double longitude, double altitude, double heading, double pitch, double roll)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Camera_createWithLatLongAltitudeHeadingPitchRoll(latitude, longitude, altitude, heading, pitch, roll, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Create a camera object.
        /// 
        /// - Parameters:
        ///   - locationPoint: A point geometry containing the location and altitude at which to place the camera.
        ///   - heading: The heading of the camera.
        ///   - pitch: The pitch of the camera. The value must be from 0 to 180 and represents the angle applied to the camera when rotating around its Y axis in the East, North, Up (ENU) ground reference frame. 0 is looking straight down towards the center of the earth, 180 looking straight up towards the sky. Negative pitches are not allowed and the values do not wrap around. If the behavior of a negative pitch is required, then the corresponding transformation with positive pitch can be set instead. For example if heading:0 pitch:-20 roll:0 is required then heading:180 pitch:20 roll:180 can be used instead.
        ///   - roll: The roll of the camera.
        /// - Since: 100.0.0
        public Camera(ArcGISRuntime.Geometry.Point locationPoint, double heading, double pitch, double roll)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localLocationPoint = locationPoint.Handle;
            
            Handle = PInvoke.RT_Camera_createWithLocationHeadingPitchRoll(localLocationPoint, heading, pitch, roll, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The heading of the camera.
        /// 
        /// - SeeAlso: Camera
        /// - Since: 100.0.0
        public double Heading
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getHeading(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The location of the camera.
        /// 
        /// - SeeAlso: Camera, Point
        /// - Since: 100.0.0
        public ArcGISRuntime.Geometry.Point Location
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getLocation(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                ArcGISRuntime.Geometry.Point localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISRuntime.Geometry.Point(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// The pitch of the camera.
        /// 
        /// - Remark: The pitch value must be from 0 to 180 and represents the angle applied to the
        /// camera when rotating around its Y axis in the East, North, Up (ENU) ground reference frame. 0 is looking straight
        /// down towards the center of the earth, 180 looking straight up towards the sky. Negative
        /// pitches are not allowed and the values do not wrap around. If the behavior of a negative pitch is required, then the
        /// corresponding transformation with positive pitch can be set instead. For example if heading:0 pitch:-20 roll:0 is
        /// required then heading:180 pitch:20 roll:180 can be used instead.
        /// - SeeAlso: Camera
        /// - Since: 100.0.0
        public double Pitch
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getPitch(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The roll of the camera.
        /// 
        /// - SeeAlso: Camera
        /// - Since: 100.0.0
        public double Roll
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getRoll(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Creates a copy of the camera with the altitude adjusted.
        /// 
        /// - Parameter deltaAltitude: The altitude delta to apply to the output camera.
        /// - Returns: A copy of the camera with an elevation delta adjusted by the parameter delta_altitude.
        /// - SeeAlso: Camera
        /// - Since: 100.0.0
        public Camera Elevate(double deltaAltitude)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Camera_elevate(Handle, deltaAltitude, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Camera localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Camera(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Check if the camera are equals.
        /// 
        /// - Remark: Check if the cameras are equals.
        /// - Parameter otherCamera: The other camera object.
        /// - Returns: true if the cameras are equals otherwise false. False if an error occurs.
        /// - SeeAlso: Camera
        /// - Since: 100.0.0
        internal bool Equals(Camera otherCamera)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localOtherCamera = otherCamera.Handle;
            
            var localResult = PInvoke.RT_Camera_equals(Handle, localOtherCamera, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Creates a copy of the camera with the location changed.
        /// 
        /// - Parameter location: The location to move the output camera to.
        /// - Returns: A copy of the camera with the location changed.
        /// - SeeAlso: Camera
        /// - Since: 100.0.0
        public Camera MoveTo(ArcGISRuntime.Geometry.Point location)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localLocation = location.Handle;
            
            var localResult = PInvoke.RT_Camera_moveTo(Handle, localLocation, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Camera localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Camera(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates a copy of the camera with a change in pitch, heading and roll to the given angles in degrees
        /// 
        /// - Parameters:
        ///   - heading: The angle in degrees to which the output camera heading will be rotated
        ///   - pitch: The angle in degrees to which the output camera pitch will be rotated. The value must be from 0 to 180 and represents the angle applied to the camera when rotating around its Y axis in the East, North, Up (ENU) ground reference frame. 0 is looking straight down towards the center of the earth, 180 looking straight up towards the sky. Negative pitches are not allowed and the values do not wrap around. If the behavior of a negative pitch is required, then the corresponding transformation with positive pitch can be set instead. For example if heading:0 pitch:-20 roll:0 is required then heading:180 pitch:20 roll:180 can be used instead.
        ///   - roll: The angle in degrees to which the output camera roll will be rotated
        /// - Returns: A copy of the camera with the position moved
        /// - SeeAlso: Camera
        /// - Since: 100.0.0
        public Camera RotateTo(double heading, double pitch, double roll)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Camera_rotateTo(Handle, heading, pitch, roll, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Camera localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Camera(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal Camera(IntPtr handle) => Handle = handle;
        
        ~Camera()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_Camera_destroy(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Camera_createWithLatLongAltitudeHeadingPitchRoll(double latitude, double longitude, double altitude, double heading, double pitch, double roll, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Camera_createWithLocationHeadingPitchRoll(IntPtr locationPoint, double heading, double pitch, double roll, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Camera_getHeading(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Camera_getLocation(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Camera_getPitch(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Camera_getRoll(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Camera_elevate(IntPtr handle, double deltaAltitude, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Camera_equals(IntPtr handle, IntPtr otherCamera, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Camera_moveTo(IntPtr handle, IntPtr location, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Camera_rotateTo(IntPtr handle, double heading, double pitch, double roll, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Camera_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}