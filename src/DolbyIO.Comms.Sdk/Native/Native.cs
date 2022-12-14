using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /**
     * Native Interop for the Dolby.io Communications C++ SDK
     * @nodocument
     */
    #nullable enable
    internal class Native
    {
        internal const string LibName = "DolbyIO.Comms.Native";

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Init(string accessToken, RefreshTokenCallBack cb);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetLogLevel([MarshalAs(UnmanagedType.I4)] LogLevel level);
    
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Open(UserInfo user, [Out] UserInfo res);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int Close();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Create(ConferenceOptions options, [Out] Conference infos);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Join(Conference conference, JoinOptions options, [Out] Conference res);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Listen(Conference conference, ListenOptions options, [Out] Conference res);
       
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Demo(SpatialAudioStyle audioStyle, [Out] Conference conference);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentConference([Out] Conference conference);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetParticipants(ref int size, out IntPtr participants);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Mute(bool muted);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int RemoteMute(bool muted, string participantId);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StartAudio();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StopAudio();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StartRemoteAudio(string participantId);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StopRemoteAudio(string participantId);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetSpatialEnvironment(float scaleX, float scaleY, float scaleZ,
                                                        float forwardX, float forwardY, float forwardZ,
                                                        float upX, float upY, float upZ,
                                                        float rightX, float rightY, float rightZ);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetSpatialDirection(float x, float y, float z);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetSpatialPosition(string userId, float x, float y, float z);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SendMessage(string message);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int DeclineInvitation(string conferenceId);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetAudioDevices(ref int size, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out AudioDevice[] devices);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetPreferredAudioInputDevice(AudioDevice device);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetPreferredAudioOutputDevice(AudioDevice device);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentAudioInputDevice(out AudioDevice device);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentAudioOutputDevice(out AudioDevice device);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Leave();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Release();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern string GetLastErrorMsg();

        // Video
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetVideoDevices(ref int size, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out VideoDevice[] devices);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentVideoDevice(out VideoDevice device);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern VideoSinkHandle CreateVideoSink(VideoSink.VideoSinkOnFrame f);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern bool DeleteVideoSink(IntPtr handle);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern bool DeleteVideoFrameBuffer(IntPtr handle);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetVideoSink(VideoSinkHandle handle);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int StartVideo(VideoDevice device, VideoFrameHandlerHandle handler);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int StopVideo();

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern VideoFrameHandlerHandle CreateVideoFrameHandler();

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern bool DeleteVideoFrameHandler(IntPtr handle);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetVideoFrameHandlerSink(VideoFrameHandlerHandle handle, VideoSinkHandle sink);

        // Events Handling
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnConferenceStatusUpdatedHandler(ConferenceStatusUpdatedEventHandler handler);                                      

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnParticipantAddedHandler(ParticipantAddedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnParticipantUpdatedHandler(ParticipantUpdatedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnConferenceMessageReceivedHandler(ConferenceMessageReceivedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnConferenceInvitationReceivedHandler(ConferenceInvitationReceivedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnAudioDeviceAddedHandler(AudioDeviceAddedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnAudioDeviceRemovedHandler(AudioDeviceRemovedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnAudioDeviceChangedHandler(AudioDeviceChangedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnVideoDeviceAddedHandler(VideoDeviceAddedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnVideoDeviceRemovedHandler(VideoDeviceRemovedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnVideoDeviceChangedHandler(VideoDeviceChangedEventHandler handler);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnActiveSpeakerChangeHandler(ActiveSpeakerChangeEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnSignalingChannelExceptionHandler(SignalingChannelErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnInvalidTokenExceptionHandler(InvalidTokenErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnDvcErrorExceptionHandler(DvcErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void SetOnPeerConnectionFailedExceptionHandler(PeerConnectionErrorEventHandler handler);

        internal static void CheckException(int err)
        {
            if (Result.Success != (Result)err)
            {
                throw new DolbyIOException(Native.GetLastErrorMsg());
            }
        }
    }
}
