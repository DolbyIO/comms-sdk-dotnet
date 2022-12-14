#include <iostream>

#include "sdk.h"
#include "handlers.h"

namespace dolbyio::comms::native {

std::map<std::string, dolbyio::comms::event_handler_id> handlers_map;

dolbyio::comms::sdk* sdk = nullptr;
std::string error = "";

extern "C" {

 EXPORT_API void SetOnSignalingChannelExceptionHandler(on_signaling_channel_exception::type handler) {
    handle<on_signaling_channel_exception>(*sdk, handler,
      [handler](const on_signaling_channel_exception::event& e) {
        handler(strdup(e.what()));
      }
    );
  }

  EXPORT_API void SetOnInvalidTokenExceptionHandler(on_invalid_token_exception::type handler) {
    handle<on_invalid_token_exception>(*sdk, handler,
      [handler](const on_invalid_token_exception::event& e) {
        handler(strdup(e.reason()), strdup(e.description()));
      }
    );
  }

  EXPORT_API int SetLogLevel(uint32_t log_level) {
    return call { [&]() {
      dolbyio::comms::sdk::log_settings settings;
      
      settings.media_log_level = (dolbyio::comms::log_level)log_level;
      settings.sdk_log_level = (dolbyio::comms::log_level)log_level;

      sdk::set_log_settings(settings);
    }}.result();
  }

  EXPORT_API int Init(const char* token, refresh_delegate_type callback) {
    return call { [&]() {
      sdk = dolbyio::comms::sdk::create(
        token,
        [callback](std::unique_ptr<dolbyio::comms::refresh_token>&& refresh_token) {
          char* token = callback();
          (*refresh_token)(std::string(token));
        }
      ).release();
    }}.result();
  }

  EXPORT_API int Release() {
    return call { [&]() {
      for (const auto& [key, value] : handlers_map)
        wait(value->disconnect());

      // Releasing sdk
      if (sdk) {
        delete sdk;
        sdk = nullptr;
      }
    }}.result();
  }

  EXPORT_API char* GetLastErrorMsg() {
    return strdup(error);
  }

} // extern "C"
} // dolbyio::comms::native
