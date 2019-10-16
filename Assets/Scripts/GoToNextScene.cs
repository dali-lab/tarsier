using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextScene : MonoBehaviour {
    public VRTK.VRTK_ControllerEvents controllerEvents;                 // Pointer to Left Controller, which will be used to change the scene
    public GameObject LControllerModel;                                 // Pointer to L Controller Model, from which we'll get the Menu Button

    private void OnEnable()
    {
        controllerEvents.TriggerPressed += ControllerEvents_Trigger_Pressed;
        HighlightMaterial();
    }

    private void OnDisable()
    {
        controllerEvents.TriggerPressed -= ControllerEvents_Trigger_Pressed;
        UnHighlightMaterial();
    }

    private void HighlightMaterial()
    {

        GameObject menuButton = LControllerModel.transform.GetChild(2).gameObject;
        menuButton.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Regular");
    }

    private void UnHighlightMaterial()
    {
        GameObject menuButton = LControllerModel.transform.GetChild(2).gameObject;
        menuButton.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
    }

    private void ControllerEvents_Trigger_Pressed(object sender, VRTK.ControllerInteractionEventArgs e)
    {
        ChangeScene();
    }

    public void ChangeScene()
    {
        int currScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        switch(currScene)
        {
            case 1:
                SteamVR_LoadLevel.Begin("Scenes/brightness");
                break;
            case 2:
                SteamVR_LoadLevel.Begin("Scenes/forest2");
                break;
            default:
                SteamVR_LoadLevel.Begin("Scenes/Tutorial");
                break;
        }
    }
}
