## Tutorial
### Dolby.io Manager

In order to use the Dolby.io Manager, create an empty GameObject in your scene, and attach the Dolby.io Manager script to it. Once you've done that, you will be able to configure several things about your conference, including:

- The name that will be displayed when you join the conference
- The throttling time for setting the local player position and direction 
- Whether or not the conference session automatically opens when the Dolby.io Manager awakes 
- whether the Dolby.io Manager should automatically close the session on application quit 
- Whether the Dolby.io Manager should automatically leave the current conference on application quit

When you run your Unity scene, the Dolby.io Manager will look for a Credentials Component in order to authenticate your access to the plugin's capabilities, so make sure you attach one before you run your scene.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/dolbyiomanager.png" width="300px">
</div>

### Credentials

You must add a Credentials component to the same GameObject that holds the Dolby.io Manager script. You may use either the Access Token Crendentials Component or the App Key Credentials component. In order to obtain credentials, you must make a [Dolby.io Account](https://dashboard.dolby.io/). Once you make it to the Dolby.io Dashboard click the "Create App" button to create an application. After that you can access the API keys that can be copy-and-pasted into the respective Credentials component in Unity. You can use only one Credential component at a time.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/dashboard.png" width="300px">
</div>
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/token.png" width="300px">
</div>
<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/appkey.png" width="300px">
</div>


### Conference Controller

In your scene, add the Conference Controller object to the same GameObject that holds the Dolby.io Manager and Credentials component. Once you've done that, you will be able to configure several things about your conference, including:
- The alias of the conference that you would like to join
- The type of audio style for the conference
- A boolean that determines wheter you wish to automatically join the conference with the application starts
- The scale of the 3D Spatial Audio environment

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/conferencecontroller.png" width="300px">
</div>

#### Mapping Methods

To map some of the methods from the "Conference Controller" script to a UI in Unity, you can create a user interface that provides buttons and dropdowns to trigger the corresponding functionalities. Here's a guide on how to do this:

**Join Conference Button:**
Create a UI button then, in the button's OnClick event, drag in the GameObject that holds the Conference Controller, add the "Join" method from the "Conference Controller" script. This way, when the button is clicked, it will trigger the Join() method, attempting to join the conference with the provided alias.

**Leave Conference Button:**
Similarly, create another UI button for leaving the conference. In the button's OnClick event, drag in the GameObject that holds the Conference Controller and add the "Leave" method from the script to its OnClick event. When clicked, this button will execute the Leave() method, allowing the user to leave the conference.

**Mute/Unmute Local Audio Button:**
For controlling local audio, create a toggle button in the UI. Drag in the GameObject that holds the Conference Controller in the OnValueChanged event of the toggle, and add the "Mute" method from the script, passing the toggle's value (muted or not muted). This way, the user can click the button to toggle between muting and unmuting their microphone.

**Start Video Button:**
For enabling the local video feed, create a UI button and drag in the GameObject that holds the Conference Controller in the button's OnClick event. In the button's OnClick event, add the "StartVideo" method from the script. This method will start the video feed using the selected video device (assuming you have a VideoDeviceDropdown UI element to select the video device).

**Stop Video Button:**
In a similar fashion, create another UI button for stopping the local video feed. Drag in the GameObject that holds the Conference Controller into the button's OnClick event and add the "StopVideo" method from the script to its OnClick event. This button will stop the local video feed when clicked.


**Start Screen Share Button:**
To initiate screen sharing, create a UI button and drag in the GameObject that holds the Conference Controller into the button's OnClick event. In the button's OnClick event, add the "StartScreenShare" method from the script. This method will start sharing the selected screen source (assuming you have a ScreenShareSourceDropdown UI element to select the source).

**Stop Screen Share Button:**
Create another UI button for stopping the screen sharing. Drag in the GameObject that holds the Conference Controller into the button's OnClick event and add the "StopScreenShare" method from the script to its OnClick event. This button will stop the ongoing screen sharing when clicked.



### Devices
#### Audio Device Dropdown
To set up the "Audio Device Dropdown" script in your Unity project, follow these steps:

**Create UI Dropdown:**
In your Unity scene, create a UI dropdown GameObject. You can do this by right-clicking in the Hierarchy panel, choosing UI > Dropdown. This dropdown will be used to display and select the available audio input or output devices.

**Attach TMP_Dropdown Component:**
Select the newly created dropdown GameObject in the Hierarchy, and make sure it has a TMP_Dropdown component attached to it. If it doesn't, add one by clicking "Add Component" in the Inspector and searching for "TMP_Dropdown."

**Attach the Script:**
Now attach the "Audio Device Dropdown" script to UI dropdown GameObject. To do this, drag and drop the script file onto the GameObject in the Hierarchy panel.

**Choose Device Direction:**
In the "Audio Device Dropdown" component, you will see a "Direction" field. Set this field to either "Input" or "Output" or "Both" depending on whether you want to manage audio input or output devices.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/audiodevices.png" width="300px">
</div>


#### Video Device Dropdown

Similarly, to set up the "Video Device Dropdown" script in your Unity project, follow these steps:

**Create UI Dropdown:**
In your Unity scene, create a UI dropdown GameObject. You can do this by right-clicking in the Hierarchy panel, choosing UI > Dropdown. This dropdown will be used to display and select the available audio input or output devices.

**Attach TMP_Dropdown Component:**
Select the newly created dropdown GameObject in the Hierarchy, and make sure it has a TMP_Dropdown component attached to it. If it doesn't, add one by clicking "Add Component" in the Inspector and searching for "TMP_Dropdown."

**Attach the Script:**
Now attach the "Video Device Dropdown" script to UI dropdown GameObject. To do this, drag and drop the script file onto the GameObject in the Hierarchy panel.

**Attach GameObject to Conference Controller:**
As a final step, attach the GameObject that has the TMP_Dropdown and Video Device Dropdown script into the slot in the Conference Controller script (assuming you are using one) named Video Device.


<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/videodevices.png" width="300px">
</div>

#### Screen Share Source Dropdown

Similarly, to set up the "Screen Share Source Dropdown" script in your Unity project, follow these steps:

**Create UI Dropdown:**
In your Unity scene, create a UI dropdown GameObject. You can do this by right-clicking in the Hierarchy panel, choosing UI > Dropdown. This dropdown will be used to display and select the available audio input or output devices.

**Attach TMP_Dropdown Component:**
Select the newly created dropdown GameObject in the Hierarchy, and make sure it has a TMP_Dropdown component attached to it. If it doesn't, add one by clicking "Add Component" in the Inspector and searching for "TMP_Dropdown."

**Attach the Script:**
Now attach the "Screen Share Source Dropdown" script to UI dropdown GameObject. To do this, drag and drop the script file onto the GameObject in the Hierarchy panel.

**Attach GameObject to Conference Controller:**
As a final step, attach the GameObject that has the TMP_Dropdown and Screen Share Sources Dropdown script into the slot in the Conference Controller script (assuming you are using one) named Screen Share Source.


<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/screensharesources.png" width="300px">
</div>



### Video
#### Video controller

To set up the "Video Controller" script in your Unity project, follow these steps:

**Create Video GameObject:**
In your Unity scene, create a GameObject that will serve as the container for displaying video. This GameObject must have a mesh renderer and mesh filter component attached, with the mesh filter ideally being set to a plane.

**Attach the Script:**
Now, attach the "Video Controller" script to the GameObject that will display the video. To do this, drag and drop the script file onto the GameObject in the Hierarchy panel.

**Specify the Conference Controller:**
If you have a separate GameObject with the "Conference Controller" script (assuming you are using one), set the "Conference" field in the "Video Controller" script to that GameObject. This ensures that the "Video Controller" registers itself with the "Conference Controller" during the Awake phase.

**Configure Video Display Settings:**
In the Inspector for the "Video Controller" script, you will see several fields to configure the video display behavior. For example:

- "FilterBy" allows you to specify how to filter the video to display (by participant name, participant ID, or external ID).
- "Filter" allows you to specify the value to use for filtering, depending on the chosen "FilterBy" option.
- "IsLocal" determines whether to display local video (your own video feed).
- "IsScreenShare" determines whether to display a screen share video instead of a regular camera video.

<div style="text-align:left">
    <img style="padding:25px 0" src="~/images/components/videocontroller.png" width="300px">
</div>




