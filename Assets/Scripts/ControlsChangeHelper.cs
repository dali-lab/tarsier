using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsChangeHelper : MonoBehaviour {

    public GameObject LController;
    public GameObject RController;

    // Allows one to turn the scene switcher control on or off
    public void SceneSwitchControl(bool enabled)
    {
        LController.GetComponent<Select_Scene_Option>().enabled = enabled;
        LController.GetComponent<VRTK.Controller_Menu_Popup>().enabled = enabled;
        foreach (Transform child in LController.transform)
        {
            child.gameObject.SetActive(enabled);
        }
    }

    // Allows one to turn the teleportation control on or off
    public void TeleportControl(bool enabled)
    {
        RController.GetComponent<VRTK.VRTK_Pointer>().enabled = enabled;
        RController.GetComponent<VRTK.VRTK_BezierPointerRenderer>().enabled = enabled;
    }

    // Allows one to turn the human / tarsier vision control on or off
    public void VisionControl(bool enabled)
    {
        foreach (Transform child in RController.transform)
        {
            child.gameObject.SetActive(enabled);
        }
    }

    public void StartupControl(bool enabled)
    {
        RController.GetComponent<UIPointer>().enabled = enabled;
    }
}
