using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The remote audio service allows to <see cref="MuteAsync(bool, string)">mute</see> selected remote participants (Non-Dolby Voice conferences only) and <see cref="StopAsync(string)"> stop</see> and <see cref="StartAsync(string)">start</see> receiving audio from remote participants.
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Remote.StartAsync(participantId);
    ///     await _sdk.Audio.Remote.StopAsync(participantId);
    ///     await _sdk.Audio.Remote.MuteAsync(true, participantId);
    /// }
    /// catch
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public sealed class RemoteAudioService
    {
        /// <summary>
        /// Start receiving the audio from a remote participant.
        /// </summary>
        /// <param name="participantId">The identifier of the remote participant whose audio should be sent to the local participant.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StartAsync(string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.StartRemoteAudio(participantId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Stop receiving the audio from a remote participant.
        /// </summary>
        /// <param name="participantId">The identifier of the remote participant whose audio should not be sent to the local participant.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StopAsync(string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.StopRemoteAudio(participantId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops playing the specified remote participants' audio to the local participant.
        /// The mute method does not notify the server to stop audio stream transmission.
        /// To stop receiving an audio stream from the server, use the <see cref="StopAsync">StopAsync</see> method.
        /// </summary>
        /// <param name="muted">A boolean value that indicates the required mute state. True
        /// mutes the remote participant, false un-mutes the remote participant.</param>
        /// <param name="participantId">The identifier of the remote participant whose audio should not be played.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        /// <remarks>
        /// <b>Attention:</b> This method is only available in non-Dolby Voice conferences.
        /// </remarks>
        public async Task MuteAsync(bool muted, string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.RemoteMute(muted, participantId))).ConfigureAwait(false);
        }
    }
}