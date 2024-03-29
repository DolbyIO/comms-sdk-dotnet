#ifndef _CONFERENCE_H_
#define _CONFERENCE_H_

#include "sdk.h"
#include "handlers.h"

namespace dolbyio::comms::native {

  /**
   * @brief C# ConferenceParams C struct.
   */
  struct conference_params {
    bool dolby_voice;
    bool stats;
    // Video Codecs
    int32_t spatial_audio_style;
  };

  /**
   * @brief C# ConferenceOptions C struct.
   */
  struct conference_options {
    conference_params params;
    char*             alias;
  };

  /**
   * @brief C# Conference C struct.
   */
  struct conference {
    char* id;
    char* alias;
    bool  is_new;

    int32_t status;
    int32_t permissions[constants::MAX_PERMISSIONS];
    int     permissions_count;
    // participants
    int32_t spatial_audio_style;
  };

  /**
   * @brief C# MediaConstraints C struct.
   */
  struct media_constraints {
    bool audio;
    bool video;
    bool send_only;
  };

  /**
   * @brief C# ConnectionOptions C struct.
   */
  struct connection_options {
    int   max_video_forwarding;
    char* conference_access_token;
    bool  spatial_audio;
    bool  simulcast;
  };

  /**
   * @brief C# JoinOptions C struct.
   */
  struct join_options {
    connection_options connection;
    media_constraints  constraints;
  };

  /**
   * @brief C# ListenOptions C struct. 
   */
  struct listen_options {
    connection_options connection;
    int                listen_mode;
  };

  /**
   * @brief C# ParticipantInfo C struct.
   */
  struct participant_info {
    char* external_id;
    char* name;
    char* avatar_url;
  };

  /**
   * @brief C# Participant C struct.
   */
  struct participant {
    participant_info info;

    char*   id;
    int32_t type;
    int32_t status;
    bool    is_sending_audio;
    bool    audible_locally;
  };

  /**
   * @brief C# Video Track C Struct
   */
  struct video_track {
    char* peer_id;
    char* stream_id;
    char* track_id;
    char* sdp_track_id;

    bool is_screenshare;
    bool remote;
  };

  struct on_conference_status_updated {
    using event = dolbyio::comms::conference_status_updated;
    using type = void (*)(int status, const char* conferenceId);
    static constexpr const char* name = "on_conference_status_updated";
  };

  struct on_participant_added {
    using event = dolbyio::comms::participant_added;
    using type = void (*)(participant* p);
    static constexpr const char* name = "on_participant_added";
  };

  struct on_participant_updated {
    using event = dolbyio::comms::participant_updated;
    using type = void (*)(participant* p);
    static constexpr const char* name = "on_participant_updated";
  };

  struct on_dvc_error_exception {
    using event = dolbyio::comms::dvc_error_exception;
    using type = void (*)(const char* reason);
    static constexpr const char* name = "on_dvc_error_exception";
  };

  struct on_peer_connection_failed_exception {
    using event = dolbyio::comms::peer_connection_failed_exception;
    using type = void (*)(const char* reason);
    static constexpr const char* name = "on_peer_connection_failed_exception";
  };

  struct on_active_speaker_change {
    using event = dolbyio::comms::active_speaker_changed;
    using type = void (*)(char* conference_id, int count, char* active_speakers[]);
    static constexpr const char* name = "on_active_speaker_change";
  };

  struct on_conference_message_received {
    using event = dolbyio::comms::conference_message_received;
    using type = void (*)(char* conference_id, char* user_id, participant_info* info, char* message);
    static constexpr const char* name = "on_conference_message_received";
  };

  struct on_conference_invitation_received {
    using event = dolbyio::comms::conference_invitation_received;
    using type = void (*)(char* conference_id, char* conference_alias, participant_info* info);
    static constexpr const char* name = "on_conference_invitation_received";
  };

  struct on_conference_video_track_added {
    using event = dolbyio::comms::video_track_added;
    using type = void (*)(video_track track);
    static constexpr const char* name = "on_conference_video_track_added";
  };

  struct on_conference_video_track_removed {
    using event = dolbyio::comms::video_track_removed;
    using type = void (*)(video_track track);
    static constexpr const char* name = "on_conference_video_track_removed";
  };

  template<typename Traits> 
  struct translator<dolbyio::comms::native::conference_options, dolbyio::comms::services::conference::conference_options, Traits> {
     static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
        dest->alias = strdup(src.alias.value_or(""));
        dest->params.dolby_voice = src.params.dolby_voice;
        dest->params.stats = src.params.stats;
        dest->params.spatial_audio_style = dolbyio::comms::native::to_underlying(src.params.spatial_audio_style);
     }

     static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
        dest.alias = std::string(src->alias);
        dest.params.dolby_voice = src->params.dolby_voice;
        dest.params.stats = src->params.stats;
        dest.params.spatial_audio_style = (dolbyio::comms::spatial_audio_style)src->params.spatial_audio_style;
     }
  };

  template<typename Traits> 
  struct translator<dolbyio::comms::native::conference, dolbyio::comms::conference_info, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->id = strdup(src.id);
      dest->alias = strdup(src.alias.value_or(""));
      dest->is_new = src.is_new;
      dest->status = to_underlying(src.status);
      dest->spatial_audio_style = to_underlying(src.spatial_audio_style.value_or(dolbyio::comms::spatial_audio_style::disabled));

      for (int i = 0; i < src.permissions.size(); i++) {
        dest->permissions[i] = to_underlying(src.permissions.at(i));
      }
      dest->permissions_count = src.permissions.size();
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      dest.id = std::string(src->id);
      dest.alias = std::string(src->alias);
      dest.is_new = src->is_new;
      dest.status = (dolbyio::comms::conference_status)src->status;
      dest.spatial_audio_style = (dolbyio::comms::spatial_audio_style)src->spatial_audio_style;

      for (int i = 0; i < src->permissions_count; i++) {
        dest.permissions.emplace_back((dolbyio::comms::conference_access_permissions)src->permissions[i]);
      }
    }
  };

  template<typename Traits>
  struct translator<dolbyio::comms::native::connection_options, dolbyio::comms::services::conference::connection_options, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->max_video_forwarding = src.max_video_forwarding.value_or(25);
      dest->conference_access_token = strdup(src.conference_access_token.value_or(""));
      dest->spatial_audio = src.spatial_audio;
      dest->simulcast = src.simulcast;
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      dest.max_video_forwarding = src->max_video_forwarding;
      dest.conference_access_token = src->conference_access_token;
      dest.spatial_audio = src->spatial_audio;
      dest.simulcast = src->simulcast;
    }
  };

  template<typename Traits> 
  struct translator<dolbyio::comms::native::join_options, dolbyio::comms::services::conference::join_options, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      no_alloc_to_c(&dest->connection, src.connection);

      dest->constraints.audio = src.constraints.audio;
      dest->constraints.video = src.constraints.video;
      dest->constraints.send_only = src.constraints.send_only;
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      no_alloc_to_cpp(dest.connection, &src->connection);

      dest.constraints.audio = src->constraints.audio;
      dest.constraints.video = src->constraints.video;
      dest.constraints.send_only = src->constraints.send_only;
    }
  };

  template<typename Traits>
  struct translator<dolbyio::comms::native::listen_options, dolbyio::comms::services::conference::listen_options, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      no_alloc_to_c(&dest->connection, src.connection);
      dest->listen_mode = to_underlying(src.type);
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      no_alloc_to_cpp(dest.connection, &src->connection);
      dest.type = (dolbyio::comms::listen_mode)(src->listen_mode);
    }
  };
  
  template<typename Traits> 
  struct translator<dolbyio::comms::native::participant_info, struct dolbyio::comms::participant_info::info, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->external_id = strdup(src.external_id.value_or(""));
      dest->name = strdup(src.name.value_or(""));
      dest->avatar_url = strdup(src.avatar_url.value_or(""));
    }
    
    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      dest.external_id = std::string(src->external_id);
      dest.name = std::string(src->name);
      dest.avatar_url = std::string(src->avatar_url);
    }
  };

  template<typename Traits> 
  struct translator<dolbyio::comms::native::participant, dolbyio::comms::participant_info, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->id = strdup(src.user_id);
      dest->type = dolbyio::comms::native::to_underlying(src.type.value_or(dolbyio::comms::participant_type::none));
      dest->status = dolbyio::comms::native::to_underlying(src.status.value_or(dolbyio::comms::participant_status::inactive));
      dest->is_sending_audio = src.is_sending_audio.value_or(false);
      dest->audible_locally = src.audible_locally.value_or(false);

      no_alloc_to_c(&dest->info, src.info);
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      dest.user_id = std::string(src->id);
      dest.type = (dolbyio::comms::participant_type)src->type;
      dest.status = (dolbyio::comms::participant_status)src->status;
      dest.is_sending_audio = src->is_sending_audio;
      dest.audible_locally = src->audible_locally;
      
      no_alloc_to_cpp(dest.info, &src->info);
    }
  };

  template<typename Traits> 
  struct translator<dolbyio::comms::native::video_track, dolbyio::comms::video_track, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->peer_id = strdup(src.peer_id);
      dest->stream_id = strdup(src.stream_id);
      dest->track_id = strdup(src.track_id);
      dest->sdp_track_id = strdup(src.sdp_track_id);

      dest->is_screenshare = src.is_screenshare;
      dest->remote = src.remote;
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      dest.peer_id = std::string(src->peer_id);
      dest.stream_id = std::string(src->stream_id);
      dest.track_id = std::string(src->track_id);
      dest.sdp_track_id = std::string(src->sdp_track_id);

      dest.is_screenshare = src->is_screenshare;
      dest.remote = src->remote;
    }
  };

} // namespace dolbyio::comms::native

#endif // _CONFERENCE_H_