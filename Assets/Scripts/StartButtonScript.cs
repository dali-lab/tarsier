using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour {

    public ControlsChangeHelper controls;
    public GameObject OptionsChooser;

	public void Click()
    {
        if (Global.guided)
        {
            SteamVR_LoadLevel.Begin("Scenes/dof");
        }
        else
        {
            // Change Control system
            controls.SceneSwitchControl(true);
            controls.StartupControl(false);
            controls.TeleportControl(true);
            controls.VisionControl(true);

            // Make the UI Screen disappear
            OptionsChooser.SetActive(false);
        }
    }
}
