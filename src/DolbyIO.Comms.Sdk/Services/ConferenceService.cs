using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DolbyIO.Comms.Services 
{   
    /// <summary>
    /// The conference service allows joining and leaving conferences as well as
    /// subscribing to conference events.
    ///
    /// To use the conference service, follow these steps:
    /// 1. Open a session using <see cref="DolbyIO.Comms.Services.SessionService.OpenAsync(UserInfo)"/> from the <see cref="DolbyIO.Comms.Services.SessionService"/>..
    /// 2. Subscribe to events exposed through the service, for example <see cref="StatusUpdated"/> and <see cref="ParticipantUpdated"/>.
    /// 3. Create a conference using the <see cref="CreateAsync(ConferenceOptions)"/> method.
    /// 4. Join the created conference using the <see cref="JoinAsync(Conference, JoinOptions)"/> method or use the <see cref="ListenAsync(Conference, ListenOptions)"/> method to join the conference as a listener.
    /// 5. Leave the conference using the <see cref="LeaveAsync"/> method.
    /// </summary>
    public sealed class ConferenceService
    {
        private ConferenceStatusUpdatedEventHandler _statusUpdated;

        /// <summary>
        /// Sets the <see cref="ConferenceStatusUpdatedEventHandler"/> that is raised when a conference status has changed.
        /// See <see cref="DolbyIO.Comms.ConferenceStatus">ConferenceStatus</see>
        /// <example>
        /// <code>
        /// _sdk.Conference.StatusUpdated += (ConferenceStatus status, string conferenceId) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ConferenceStatusUpdatedEventHandler"/> event handler.</value>
        public event ConferenceStatusUpdatedEventHandler StatusUpdated
        {
            add 
            { 
                Native.AddOnConferenceStatusUpdatedHandler(value.GetHashCode(), value); 
                _statusUpdated += value;
            }

            remove
            {
                Native.RemoveOnConferenceStatusUpdatedHandler(value.GetHashCode(), value);
                _statusUpdated -= value;
            }
        }

        private ParticipantAddedEventHandler _participantAdded;

        /// <summary>
        /// Sets the <see cref="ParticipantAddedEventHandler"/> that is raised when a new participant has been added to a conference.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantAdded += (Participant participant) => 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ParticipantAddedEventHandler"/> event handler.</value>
        public event ParticipantAddedEventHandler ParticipantAdded
        {
            add 
            {
                Native.AddOnParticipantAddedHandler(value.GetHashCode(), value); 
                _participantAdded += value;
            }

            remove
            {
                Native.RemoveOnParticipantAddedHandler(value.GetHashCode(), value);
                _participantAdded -= value;
            }
        }

        private ParticipantUpdatedEventHandler _participantUpdated;

        /// <summary>
        /// Sets the <see cref="ParticipantUpdatedEventHandler"/> that is raised when a conference participant has changed a status.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantUpdated += (Participant participant) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ParticipantUpdatedEventHandler"/> event handler.</value>
        public event ParticipantUpdatedEventHandler ParticipantUpdated
        {
            add 
            {
                Native.AddOnParticipantUpdatedHandler(value.GetHashCode(), value); 
                _participantUpdated += value;
            }

            remove
            {
                Native.RemoveOnParticipantUpdatedHandler(value.GetHashCode(), value);
                _participantUpdated -= value;
            }
        }

        private ActiveSpeakerChangeEventHandler _activeSpeakerChange;

        /// <summary>
        /// Sets the <see cref="ActiveSpeakerChangeEventHandler"/> that is raised when an active speaker has changed.
        /// <example>
        /// <code>
        /// _sdk.Conference.ActiveSpeakerChange += (string conferenceId, int count, string[] activeSpeakers) => 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ActiveSpeakerChangeEventHandler"/> event handler.</value>
        public event ActiveSpeakerChangeEventHandler ActiveSpeakerChange
        {
            add 
            {
                Native.AddOnActiveSpeakerChangeHandler(value.GetHashCode(), value); 
                _activeSpeakerChange += value;
            }

            remove
            {
                Native.RemoveOnActiveSpeakerChangeHandler(value.GetHashCode(), value);
                _activeSpeakerChange -= value;
            }
        }

        private ConferenceMessageReceivedEventHandler _messageReceived;

        /// <summary>
        /// Sets the <see cref="ConferenceMessageReceivedEventHandler"/> that is raised when a participant receives a message.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.MessageReceived += (string conferenceId, string userId, ParticipantInfo info, string message) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="ConferenceMessageReceivedEventHandler"/> event handler.</value>
        public event ConferenceMessageReceivedEventHandler MessageReceived
        {
            add 
            { 
                Native.AddOnConferenceMessageReceivedHandler(value.GetHashCode(), value);
                _messageReceived += value;
            }

            remove
            {
                Native.RemoveOnConferenceMessageReceivedHandler(value.GetHashCode(), value);
                _messageReceived -= value;
            }
        }

        private ConferenceInvitationReceivedEventHandler _invitationReceived;

        /// <summary>
        /// Sets the <see cref="ConferenceInvitationReceivedEventHandler"/> that is raised when a participant receives a conference invitation.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.InvitationReceived += (string conferenceId, string conferenceAlias, ParticipantInfo info) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="ConferenceInvitationReceivedEventHandler"/> event handler.</value>
        public event ConferenceInvitationReceivedEventHandler InvitationReceived
        {
            add
            { 
                Native.AddOnConferenceInvitationReceivedHandler(value.GetHashCode(), value);
                _invitationReceived += value;
            }

            remove
            {
                Native.RemoveOnConferenceInvitationReceivedHandler(value.GetHashCode(), value);
                _invitationReceived -= value;
            }
        }

        private DvcErrorEventHandler _dvcError;

        /// <summary>
        /// Sets the <see cref="DvcErrorEventHandler"/> that is raised when an error related to the Dolby Voice Codec (DVC) occurs.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.DvcError += (string reason) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="DvcErrorEventHandler"/> event handler.</value>
        public event DvcErrorEventHandler DvcError
        {
            add 
            { 
                Native.AddOnDvcErrorExceptionHandler(value.GetHashCode(), value);
                _dvcError += value;
            }

            remove
            {
                Native.RemoveOnDvcErrorExceptionHandler(value.GetHashCode(), value);
                _dvcError -= value;
            }
        }

        private PeerConnectionErrorEventHandler _peerConnectionError;

        /// <summary>
        /// Sets the <see cref="PeerConnectionErrorEventHandler"/> that is raised when a peer connection problem occurs.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.PeerConnectionError += (string reason, string description) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="PeerConnectionErrorEventHandler"/> event handler.</value>
        public event PeerConnectionErrorEventHandler PeerConnectionError
        {
            add 
            { 
                Native.AddOnPeerConnectionFailedExceptionHandler(value.GetHashCode(), value);
                _peerConnectionError += value;
            }

            remove
            {
                Native.RemoveOnPeerConnectionFailedExceptionHandler(value.GetHashCode(), value);
                _peerConnectionError -= value;
            }
        }

        private VideoTrackAddedEventHandler _videoTrackAdded;

        /// <summary>
        /// Sets the <see cref="VideoTrackAddedEventHandler"/> that is raised when a <see cref="VideoTrack"/> is added.
        /// </summary>
        public event VideoTrackAddedEventHandler VideoTrackAdded
        {
            add
            {
                Native.AddOnConferenceVideoTrackAddedHandler(value.GetHashCode(), value);
                _videoTrackAdded += value;
            }

            remove
            {
                Native.RemoveOnConferenceVideoTrackAddedHandler(value.GetHashCode(), value);
                _videoTrackAdded -= value;
            }
        }

        private VideoTrackRemovedEventHandler _videoTrackRemoved;

        /// <summary>
        /// Sets the <see cref="VideoTrackRemovedEventHandler"/> that is raised when a <see cref="VideoTrack"/> is removed.
        /// </summary>
        public event VideoTrackRemovedEventHandler VideoTrackRemoved
        {
            add
            {
                Native.AddOnConferenceVideoTrackRemovedHandler(value.GetHashCode(), value);
                _videoTrackRemoved += value;
            }

            remove
            {
                Native.RemoveOnConferenceVideoTrackRemovedHandler(value.GetHashCode(), value);
                _videoTrackRemoved -= value;
            }
        }

        private volatile bool _isInConference = false;

        /// <summary>
        /// Gets whether the SDK is connected to a conference.
        /// </summary>
        /// <value><c>true</c> if the SDK is connected to a conference; otherwise, <c>false</c>.</value>
        public bool IsInConference { get => _isInConference; }

        /// <summary>
        /// Gets information about the current conference.
        /// </summary>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the currently active <see cref="Conference" />.</returns>
        public async Task<Conference> GetCurrentAsync()
        {
            return await Task.Run(() =>
            {
                Conference conference = new Conference();
                Native.CheckException(Native.GetCurrentConference(conference));
                return conference;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the list of participants that are present in the current conference.
        /// </summary>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns a <xref href="System.Collections.Generic.List`1" /> of <see cref="Participant" /> objects.</returns>
        public async Task<List<Participant>> GetParticipantsAsync()
        {
            return await Task.Run(() =>
            {
                List<Participant> participants = new List<Participant>();
                IntPtr src;
                int size = 0;

                Native.CheckException(Native.GetParticipants(ref size, out src));

                IntPtr[] tmp = new IntPtr[size];
                Marshal.Copy(src, tmp, 0, size);

                for (int i = 0; i < size; i++)
                {
                    var participant = Marshal.PtrToStructure<Participant>(tmp[i]);
                    participants.Add(participant);
                }

                return participants;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a conference and returns information about the conference upon completion.
        /// </summary>
        /// <param name="options">The conference options.</param>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the newly created <see cref="Conference" />.</returns>
        public async Task<Conference> CreateAsync(ConferenceOptions options)
        {
            return await Task.Run(() => 
            {
                Conference conference = new Conference();
                Native.CheckException(Native.Create(options, conference));
                return conference;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Joins an existing conference as a user.
        /// </summary>
        /// <param name="conference">The conference object that represents the conference to join.</param>
        /// <param name="options">The join options for the current participant.</param>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the joined <see cref="Conference" /> object.</returns>
        public async Task<Conference> JoinAsync(Conference conference, JoinOptions options)
        {
            return await Task.Run(() => 
            {
                Conference res = new Conference();
                Native.CheckException(Native.Join(conference, options, res));
                _isInConference = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Joins an existing conference as a listener.
        /// </summary>
        /// <param name="conference">The conference object that represents the conference to listen to.</param>
        /// <param name="options">The join options for the current participant.</param>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the joined <see cref="Conference" /> object.</returns>
        public async Task<Conference> ListenAsync(Conference conference, ListenOptions options)
        {
            return await Task.Run(() =>
            {
                Conference res = new Conference();
                Native.CheckException(Native.Listen(conference, options, res));
                _isInConference = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a demo conference and joins it upon completion.
        /// </summary>
        /// <param name="audioStyle">The <see cref="DolbyIO.Comms.SpatialAudioStyle">spatial audio style</see> to be used in the demo conference.</param>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the joined <see cref="Conference" /> object.</returns>
        public async Task<Conference> DemoAsync(SpatialAudioStyle audioStyle = SpatialAudioStyle.Individual)
        {
            return await Task.Run(() => 
            {
                Conference conference = new Conference();
                Native.CheckException(Native.Demo(audioStyle, conference));
                _isInConference = true;
                return conference;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Configures a spatial environment of an application, so the audio
        /// renderer understands which directions the application considers forward,
        /// up, and right and which units it uses for distance.
        /// This method is available only for participants who joined a conference using the join method with enabled spatial audio. To set a spatial environment for listeners, use the <see href="https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio">Set Spatial Listeners Audio</see> REST API.
        /// If not called, the SDK uses the default spatial environment, which consists of the following values:
        /// - forward = (0, 0, 1), where +Z axis is in front
        /// - up = (0, 1, 0), where +Y axis is above
        /// - right = (1, 0, 0), where +X axis is to the right
        /// - scale = (1, 1, 1), where one unit on any axis is 1 meter
        ///
        /// For more information about spatial audio, see the <see href="https://docs.dolby.io/communications-apis/docs/guides-spatial-audio">Spatial Audio</see> guide.
        /// </summary>
        /// <param name="scale">A scale that defines how to convert units from the coordinate system of an application (pixels or centimeters) into meters used by the spatial audio coordinate system. For example, if SpatialScale is set to (100,100,100), it indicates that 100 of the applications units (cm) map to 1 meter for the audio coordinates. In such a case, if the listener's location is (0,0,0)cm and a remote participant's location is (200,200,200)cm, the listener has an impression of hearing the remote participant from the (2,2,2)m location. </param>
        /// <param name="forward">A vector describing the direction the application
        /// considers as forward. The value can be either +1, 0, or -1 and must be
        /// orthogonal to up and right.</param>
        /// <param name="up">A vector describing the direction the application considers as
        /// up. The value can be either +1, 0, or -1 and must be orthogonal to
        /// forward and right.</param>
        /// <param name="right">A vector describing the direction the application considers
        /// as right. The value can be either +1, 0, or -1 and must be orthogonal to
        /// forward and up.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SetSpatialEnvironmentAsync(Vector3 scale, Vector3 forward, Vector3 up, Vector3 right)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialEnvironment(
                scale.X, scale.Y, scale.Z,
                forward.X, forward.Y, forward.Z,
                up.X, up.Y, up.Z,
                right.X, right.Y, right.Z
            ))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the direction the local participant is facing in space. This method is available only for participants who joined the conference using the join method with enabled spatial audio. To set a spatial direction for listeners, use the <see href="https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio">Set Spatial Listeners Audio</see> REST API.
        ///
        /// If the local participant hears audio from the position (0,0,0) facing down the Z-axis and locates a remote participant in the position (1,0,1), the local participant hears the remote participant from their front-right. If the local participant chooses to change the direction they are facing and rotate +90 degrees about the Y-axis, then instead of hearing the speaker from the front-right position, they hear the speaker from the front-left position.
        ///
        /// For more information about spatial audio, see the <see href="https://docs.dolby.io/communications-apis/docs/guides-spatial-audio">Spatial Audio</see> guide.
        /// </summary>
        /// <param name="direction">The direction the local participant is facing in space.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SetSpatialDirectionAsync(Vector3 direction)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialDirection(direction.X, direction.Y, direction.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets a participant's position in space to enable the spatial audio experience during a Dolby Voice conference. This method is available only for participants who joined the conference using the join method with enabled spatial audio. To set a spatial position for listeners, use the <see href="https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio">Set Spatial Listeners Audio</see> REST API.
        ///
        /// Depending on the specified participant in the participant parameter, the setSpatialPosition method impacts the location from which audio is heard or from which audio is rendered:
        /// - When the specified participant is the local participant, setSpatialPosition sets a location from which the local participant listens to a conference. If the local participant does not have an established location, the participant hears audio from the default location (0, 0, 0).
        /// - When the specified participant is a remote participant, setSpatialPosition ensures the remote participant's audio is rendered from the specified location in space. Setting the remote participants’ positions is required in conferences that use the individual spatial audio style. In these conferences, if a remote participant does not have an established location, the participant does not have a default position and will remain muted until a position is specified. The shared spatial audio style does not support setting the remote participants' positions. In conferences that use the shared style, the spatial scene is shared by all participants, so that each client can set a position and participate in the shared scene.
        ///
        /// For example, if a local participant Eric, who uses the individual spatial audio style and does not have a set direction, calls setSpatialPosition(VoxeetSDK.session.participant, {x:3,y:0,z:0}), Eric hears audio from the position (3,0,0). If Eric also calls setSpatialPosition(Sophia, {x:7,y:1,z:2}), he hears Sophia from the position (7,1,2). In this case, Eric hears Sophia 4 meters to the right, 1 meter above, and 2 meters in front.
        ///
        /// For more information about spatial audio, see the <see href="https://docs.dolby.io/communications-apis/docs/guides-spatial-audio">Spatial Audio</see> guide.
        /// </summary>
        /// <param name="participantId">The selected participant. Using the local participant sets the location from which the participant will hear a conference. Using a remote participant sets the position from which the participant's audio will be rendered.</param>
        /// <param name="position">The participant's audio location.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SetSpatialPositionAsync(string participantId, Vector3 position)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialPosition(participantId, position.X, position.Y, position.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a message to the current conference.
        /// </summary>
        /// <remarks>
        /// Attention: The message size is limited to 16KB.
        /// </remarks>
        /// <param name="message">The message to send to the conference.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SendMessageAsync(string message)
        {
            await Task.Run(() => Native.CheckException(Native.SendMessage(message))).ConfigureAwait(false);
        }

        /// <summary>
        /// Declines a conference invitation.
        /// </summary>
        /// <param name="conferenceId">The conference identifier.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task DeclineInvitationAsync(string conferenceId) {
            await Task.Run(() => Native.CheckException(Native.DeclineInvitation(conferenceId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Leaves a conference.
        /// </summary>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task LeaveAsync()
        {
            await Task.Run(() => {
                Native.CheckException(Native.Leave());
                _isInConference = false;
            }).ConfigureAwait(false);
        }
    }
}
