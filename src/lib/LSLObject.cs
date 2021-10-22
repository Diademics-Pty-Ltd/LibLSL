using System;
using System.Runtime.InteropServices;

namespace LSL
{
    /// <summary>
    /// abstract base class for interop objects
    /// This class was lifted directly from liblsl: https://github.com/labstreaminglayer/liblsl-Csharp/blob/master/LSL.cs#L870-L876
    /// </summary>
    public abstract class LSLObject : SafeHandle
    {
        // shared const fields


        public IntPtr Obj { get => handle; }
        
        public LSLObject(IntPtr obj) : base(IntPtr.Zero, true)
        {
#if LSL_PRINT_OBJECT_LIFETIMES
            System.Console.Out.WriteLine($"Created object {obj:X}");
#endif
            if (obj == IntPtr.Zero)
            {
                throw new InternalException("Error creating object");
            }

            SetHandle(obj);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <summary>
        /// To be implemented in inheriting classes: the liblsl function to destroy the internal object
        /// </summary>
        protected abstract void DestroyLSLObject(IntPtr obj);

        protected override bool ReleaseHandle()
        {
#if LSL_PRINT_OBJECT_LIFETIMES
            System.Console.Out.WriteLine($"Destroying object {handle:X}");
#endif
            DestroyLSLObject(handle);
            return true;
        }


    }
}
