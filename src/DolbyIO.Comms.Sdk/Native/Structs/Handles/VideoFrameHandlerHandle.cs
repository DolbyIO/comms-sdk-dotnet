using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    internal sealed class VideoFrameHandlerHandle : SafeHandle
    {
        public VideoFrameHandlerHandle()
            : base(IntPtr.Zero, true)
        {}

        public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);

        protected override bool ReleaseHandle()
        {
            return Native.DeleteVideoFrameHandler(handle);
        }
    }
}