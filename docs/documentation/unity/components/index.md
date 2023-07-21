## Components
### Dolby.io Manager

Lets you access functionalities of the Unity plugin. It also provides useful parameters for the plugin configuration.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/dolbyiomanager.png" width="300px">
</div>

| Name | Type | Description  |
|---|:---|:---|
| **Player Name** | String | The user name for the Dolby.io session. |
| **Auto Leave Conference** | Boolean | A boolean that allows the plugin to automatically leave the conference when the application is quitting. |
| **Auto Close Session** | Boolean | A boolean that allows the plugin to automatically close the session when the application is quitting. |
| **Position Duration** | Float (seconds) | The throttling time for setting the local player position. |
| **Direction Duration** | Float (seconds) | The throttling time for setting the local player direction. |
| **Audio Listener** | GameObject | A GameObject containing the AudioListener component to get the local player position from. If empty, the default AudioListener is chosen. | 

### Credentials

The Credentials components allows you to provide the required credentials to the Dolby.io Manager. You can use only one Credential component at a time.

#### Access Token

Provides a client access token.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/token.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **Access Token** | String | A client access token from the [Dolby.io dashboard](https://dashboard.dolby.io/).|

#### App Key

Provides an app key and secret.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/appkey.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **App Key** | String | An app key from the [Dolby.io dashboard](https://dashboard.dolby.io/).|
| **App Secret** | String | An app secret from the [Dolby.io dashboard](https://dashboard.dolby.io/).|

### Conference Controller

Lets you control a conference.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/conferencecontroller.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **Conference Alias** | String | The alias of the conference that you would like to join. |
| **Audio Style** | Enum | A <xref href="DolbyIO.Comms.SpatialAudioStyle"/> for the conference. |
| **Scale** | Vector3 | The scale of the 3D environment. |
| **Auto Join** | Bool | A boolean that informs whether you wish to automatically join the conference when the application starts. |
| **Video Device** | GameObject | The GameObject Dropdown of the video device source. |
| **Screen Share Source** | GameObject | The GameObject Dropdown of the screen share source. |

### Devices
#### Audio Device Dropdown

When attached to a [TMP_Dropdown](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/api/TMPro.TMP_Dropdown.html), this component allows you to display and select the input and output audio device.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/audiodevices.png" width="300px">
</div>

| Name | Type | Description |
|--|:--|:--|
| **Direction** | Enum | A <xref href="DolbyIO.Comms.DeviceDirection"/> to display the corresponding devices. |

#### Video Device Dropdown

When attached to a [TMP_Dropdown](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/api/TMPro.TMP_Dropdown.html), this component allows you to display and select the video capture device.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/videodevices.png" width="300px">
</div>

#### Screen Share Source Dropdown

When attached to a [TMP_Dropdown](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/api/TMPro.TMP_Dropdown.html), this components allows you to display and select the screen share source to capture.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/screensharesources.png" width="300px">
</div>

### Video
#### Video controller

When attached to a GameObject with a mesh render and a mesh filter, allows video to be rendering onto its surface

| Name | Type | Description |
|--|:--|:--|
| **Filter By** | Enum | The type of the filter used for the display. |
| **Filter** | String | The value of the filter. |
| **Is Local** | Bool | A boolean to determine whether to display local or remote video. |
| **Is Screen Share** | Bool | A boolean that determines whether to display screenshare. |

