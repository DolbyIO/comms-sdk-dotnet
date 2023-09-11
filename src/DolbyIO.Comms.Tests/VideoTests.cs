using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    class MySink : VideoSink
    {
        override public void OnFrame(VideoFrame f) {}
    }

    [Collection("Sdk")]
    public class VideoTests
    {
        private SdkFixture _fixture;

        public VideoTests(SdkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Test_Video_CanCallScreenShareMethods()
        {
            ScreenShareSource source = new ScreenShareSource();

            await _fixture.Sdk.Video.Local.StartScreenShareAsync(source, null);
            await _fixture.Sdk.Video.Local.StopScreenShareAsync();
        }

        [Fact]
        public void Test_Video_canCreateAndDeleteFrameHandler()
        {
            using (var sink = new MySink())
            {
                using(var frameHanbdler = new VideoFrameHandler())
                {
                    frameHanbdler.Sink = sink;
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}