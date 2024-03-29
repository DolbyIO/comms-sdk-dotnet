add_dotnet_library(
    NAME
        DolbyIO.Comms.Sdk
    SOURCES
        AssemblyInfo.cs
        Native/Enums/ConferenceAccessPermission.cs
        Native/Enums/ConferenceStatus.cs
        Native/Enums/DeviceDirection.cs
        Native/Enums/LogLevel.cs
        Native/Enums/ParticipantStatus.cs
        Native/Enums/ParticipantType.cs
        Native/Enums/Result.cs
        Native/Enums/SpatialAudioStyle.cs
        Native/Enums/ListenMode.cs
        Native/Enums/ScreenShareType.cs
        Native/Structs/Handles/VideoFrame.cs
        Native/Structs/Handles/VideoSinkHandle.cs
        Native/Structs/Handles/VideoFrameHandlerHandle.cs
        Native/Structs/DeviceIdentity.cs
        Native/Structs/AudioDevice.cs
        Native/Structs/Conference.cs
        Native/Structs/ConferenceOptions.cs
        Native/Structs/ConferenceParams.cs
        Native/Structs/ConnectionOptions.cs
        Native/Structs/Constants.cs
        Native/Structs/JoinOptions.cs
        Native/Structs/ListenOptions.cs
        Native/Structs/MediaConstraints.cs
        Native/Structs/Participant.cs
        Native/Structs/ParticipantInfo.cs
        Native/Structs/UserInfo.cs
        Native/Structs/VideoDevice.cs
        Native/Structs/VideoSink.cs
        Native/Structs/VideoFrameHandler.cs
        Native/Structs/VideoTrack.cs
        Native/Structs/ScreenShareSource.cs
        Native/Callback.cs
        Native/Handlers.cs
        Native/Native.cs
        Services/SessionService.cs
        Services/ConferenceService.cs
        Services/MediaDeviceService.cs
        Services/Audio/AudioService.cs
        Services/Audio/LocalAudioService.cs
        Services/Audio/RemoteAudioService.cs
        Services/Video/VideoService.cs
        Services/Video/LocalVideoService.cs
        Services/Video/RemoteVideoService.cs
        ComponentName.cs
        DolbyIOException.cs
        DolbyIOSDK.cs
    VERSION
        ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH}
    FRAMEWORK
        netstandard2.1
)
