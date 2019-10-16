using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LeftControls : MonoBehaviour {

    private VRTK_ControllerEvents controllerEvents;

    public GameObject moveToNextScenePopup;
    public GameObject visionPopup;
    public GameObject vision;

    private void OnEnable()
    {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        if (controllerEvents == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
            return;
        }

        controllerEvents.ButtonOnePressed += DoButtonOnePressed;
        //controllerEvents.ButtonTwoPressed += DoButtonTwoPressed;
    }

    private void OnDisable()
    {
        if (controllerEvents != null)
        {
            controllerEvents.ButtonOnePressed -= DoButtonOnePressed;
            //controllerEvents.ButtonTwoPressed -= DoButtonTwoPressed;
        }
    }

    private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
    {
        string debugString = "Controller on index '" + index + "' " + button + " has been " + action
                             + " with a pressure of " + e.buttonPressure + " / Primary Touchpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)" + " / Secondary Touchpad axis at: " + e.touchpadTwoAxis + " (" + e.touchpadTwoAngle + " degrees)";
        VRTK_Logger.Info(debugString);
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        vision.GetComponent<Refocus>().enabled = !vision.GetComponent<Refocus>().enabled;
        vision.GetComponent<Wilberforce.Colorblind>().enabled = !vision.GetComponent<Wilberforce.Colorblind>().enabled;
        vision.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour2>().enabled = !vision.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour2>().enabled;
        

        print("BUTTON ONE PRESSED");
        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "pressed down", e);
    }

    private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
        moveToNextScenePopup.SetActive(!moveToNextScenePopup.activeSelf);


        print("BUTTON TWO PRESSED");
        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "pressed down", e);
        
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
