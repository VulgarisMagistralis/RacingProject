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

namespace Esri.Standard
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial class IntermediateFuture<T>
    {
        #region Methods
        /// Cancels the Future.
        /// 
        /// - Remark: Sets the Future to a failed and canceled state and causes the following:
        /// * The property Future.isCanceled() returns true
        /// * Future.getError() returns an error indicating cancellation
        /// * Future.get() returns null
        /// * Future.wait() returns FutureStatus.canceled
        /// 
        /// The underlying asynchronous code cooperatively checks for cancellation status
        /// and may continue to execute for a short while after the Future is set to canceled.
        /// - Returns: true if the Future was canceled, false if the Future is already canceled.
        /// Returns false if an error occurs.
        /// - Since: 100.0.0
        internal bool Cancel()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_cancel(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Returns the result of the Future.
        /// 
        /// - Remark: If the Future is successful then Future.get() will return the result. For a
        /// Future which is successful but has no result then an empty Element is returned.
        /// 
        /// If the Future has failed during execution, the call to Future.get() will result
        /// in an error.
        /// 
        /// Canceled Future are an exception and return a null with no error.
        /// 
        /// If the Future is not complete, a call to Future.get() will block the calling
        /// thread until the Future completes execution.
        /// - Returns: The result of the Future or null if the Future was canceled.
        /// - Since: 100.0.0
        internal T Get()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_get(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Standard.Element localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.Element(localResult);
            }
            
            return Convert.FromElement<T>(localLocalResult);
        }
        
        /// If the Future is executing, or has completed successfully, a null is returned. If the Future has failed returns the  error.
        /// 
        /// - Remark: If the Future is executing, or completed successfully null is returned. For a
        /// completed but failed Future the failure is returned in an Error.
        /// 
        /// If the Future was canceled this is also a failure and returns an error.
        /// - Returns: Returns the Error instance or null.
        /// - Since: 100.0.0
        internal Exception GetError()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_getError(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return Convert.FromError(new Standard.Error(localResult));
        }
        
        /// Indicates if the Future was canceled.
        /// 
        /// - Returns: true if the Future was canceled or false otherwise.
        /// - Since: 100.0.0
        internal bool IsCanceled()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_isCanceled(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Indicates if the Future has completed execution.
        /// 
        /// - Returns: true if the Future has completed, false otherwise.
        /// - Since: 100.0.0
        internal bool IsDone()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_isDone(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Waits for the Future to complete.
        /// 
        /// - Remark: If the Future is successful or canceled then Future.wait() will return the
        /// FutureStatus.
        /// 
        /// If the Future has failed during execution, the call to Future.wait() will
        /// result in an error.
        /// 
        /// If the Future is not complete, a call to Future.wait() will block the calling
        /// thread until the Future completes execution.
        /// - Returns: The FutureStatus. Returns FutureStatus.unknown if an error occurs.
        /// - Since: 100.0.0
        internal FutureStatus Wait()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_wait(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        #endregion // Methods
        
        #region Events
        /// Sets the function that will be called when the Future is completed.
        /// 
        /// - Remark: When the Future completes then Future.isDone() is true and this callback
        /// will be called.
        /// 
        /// Setting this callback after Future has completed will immediately call the
        /// callback.
        /// 
        /// Setting the callback to null after it has already been set will stop the function
        /// from being called.
        /// - Since: 100.0.0
        internal FutureCompletedEvent TaskCompleted
        {
            get
            {
                return _taskCompletedHandler.Delegate;
            }
            set
            {
                _taskCompletedHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_taskCompletedHandler.Delegate != null)
                {
                    PInvoke.RT_Task_setTaskCompletedCallback(Handle, FutureCompletedEventHandler.HandlerFunction, _taskCompletedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_Task_setTaskCompletedCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Events
        
        #region Internal Members
        internal IntermediateFuture(IntPtr handle) => Handle = handle;
        
        ~IntermediateFuture()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_taskCompletedHandler.Delegate != null)
                {
                    PInvoke.RT_Task_setTaskCompletedCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_Task_destroy(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        
        internal FutureCompletedEventHandler _taskCompletedHandler = new FutureCompletedEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Task_cancel(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Task_get(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Task_getError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Task_isCanceled(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Task_isDone(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern FutureStatus RT_Task_wait(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Task_setTaskCompletedCallback(IntPtr handle, FutureCompletedEventInternal taskCompleted, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Task_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}