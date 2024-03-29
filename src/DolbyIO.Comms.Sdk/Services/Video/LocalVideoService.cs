using System.Threading.Tasks;

#nullable enable

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The LocalVideoService is responsible for capturing the local participant's video
    /// and sending the video into a conference.
    /// </summary>
    public sealed class LocalVideoService
    {
        /// <summary>
        /// Starts capturing the local participant's video.
        ///
        /// You can call this method any time, regardless of the conference state. If this method is invoked when there's
        /// no active conference, the method selects a camera and sets a video frame handler. If the video frame
        /// handler has a video sink, the camera starts delivering frames to the sink.
        ///
        /// This method also allows switching cameras. If you passed a VideoFrameHandler to the
        /// previous start call and would like to continue using that handler, you must pass the same handler into the
        /// subsequent call to switch cameras. This action just switches cameras and keeps the rest of
        /// the pipeline in tact.
        ///
        /// An application is responsible for the frame handler and must not delete the handler or its
        /// sink until the handler invokes the StopAsync() method and the StopAsync() method execution is finished.
        ///
        /// If the application uses a null VideoDevice, then the SDk uses the first video device found in the system.
        ///
        /// If this method returns an error, the application can safely delete the provided frame handler.
        ///
        /// If the application starts the video while not in a conference and joins the conference later, the conference's
        /// local video state is determined by the MediaConstraints passed to the conference::join() method. In this situation, it is possible to
        /// start the local camera preview, but it is not possible not to join the conference with video. In order to enable video later
        /// , you have to call the start() method again. It is not possible to stop sending video into the
        /// conference while keeping the local camera preview..   
        /// </summary>
        /// <param name="device">The device for capturing video or null.</param>
        /// <param name="handler">The VideoFrameHandler for receiving the local video frames.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StartAsync(VideoDevice? device = null, VideoFrameHandler? handler = null)
        {
            VideoDevice input = device ?? new VideoDevice("", "");
            VideoFrameHandler inputHandler = handler ?? new VideoFrameHandler(new VideoFrameHandlerHandle());
            
            await Task.Run(() => Native.CheckException(Native.StartVideo(input, inputHandler.Handle))).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops local video capture.
        /// </summary>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StopAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopVideo())).ConfigureAwait(false);
        }

        /// <summary>
        /// Starts local capture of screen share.
        ///
        /// This method may be called only if a conference is active.
        ///
        /// This method can also be used to switch screen share sources at any point. If you have passed in a
        /// <xref href="VideoFrameHandler"/> to the previous start call and would like to continue using this handler, you must pass the
        /// same handler into the subsequent call used to switch sources. This will have the effect of just switching sources,
        /// keeping the rest of the pipeline in tact.
        ///
        /// The SDK supports switching seamlesly between various screen capture sources, without readding a video track
        /// to the peer connection. However, if switching from low resolution Window capture to high resolution Fullscreen
        /// capture the Window capture screen share must be stopped before the Fullscreen capture can be started.
        /// </summary>
        /// <param name="source">The screen source to capture from.</param>
        /// <param name="handler">The video frames handler.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StartScreenShareAsync(ScreenShareSource source, VideoFrameHandler? handler = null)
        {
            VideoFrameHandler inputHandler = handler ?? new VideoFrameHandler(new VideoFrameHandlerHandle());
            await Task.Run(() => Native.CheckException(Native.StartScreenShare(source, inputHandler.Handle))).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops the screen share capture.
        /// </summary>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StopScreenShareAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopScreenShare())).ConfigureAwait(false);
        }
    }
}