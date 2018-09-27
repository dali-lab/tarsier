using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;


public class toggleTooltips : MonoBehaviour {

    public GameObject RightControllerTooltips;
    public GameObject RightControllerTouchpad;
    public GameObject RightControllerButtonTwo;
    public GameObject RightControllerTrigger;
    public GameObject LeftControllerTooltips;
    public GameObject LeftControllerTouchpad;
    public GameObject LeftControllerButtonTwo;
    public GameObject LeftControllerTrigger;

    public GameObject RightController;
    public GameObject LeftController;

    public bool tooltipEnabled;

    Color32 triggerColor;
    private Color32 rightTouchpadColor;
    private Color32 rightButtonColor;
    private Color32 leftTouchpadColor;
    private Color32 leftButtonColor;

    void Start()
    {
        RightControllerTooltips = GameObject.Find("/[VRTK_Scripts]/Right Controller/RightControllerTooltips");
        RightControllerTouchpad = GameObject.Find("/[VRTK_Scripts]/Right Controller/RightControllerTooltips/TouchpadTooltip");
        RightControllerButtonTwo = GameObject.Find("/[VRTK_Scripts]/Right Controller/RightControllerTooltips/ButtonTwoTooltip");
        RightControllerTrigger = GameObject.Find("/[VRTK_Scripts]/Right Controller/RightControllerTooltips/TriggerTooltip");
        LeftControllerTooltips = GameObject.Find("/[VRTK_Scripts]/Left Controller/LeftControllerTooltips");
        LeftControllerTouchpad = GameObject.Find("/[VRTK_Scripts]/Left Controller/LeftControllerTooltips/TouchpadTooltip");
        LeftControllerButtonTwo = GameObject.Find("/[VRTK_Scripts]/Left Controller/LeftControllerTooltips/ButtonTwoTooltip");
        LeftControllerTrigger = GameObject.Find("/[VRTK_Scripts]/Left Controller/LeftControllerTooltips/TriggerTooltip");

        RightController = GameObject.Find("/[VRTK_Scripts]/Right Controller");
        LeftController = GameObject.Find("/[VRTK_Scripts]/Left Controller");

        tooltipEnabled = false;

        triggerColor = new Color32(19, 255, 36, 255);
        rightTouchpadColor = new Color32(255, 88, 111, 255);
        rightButtonColor = new Color32(164, 56, 255, 255);
        leftTouchpadColor = new Color32(229, 255, 79, 255);
        leftButtonColor = new Color32(11, 255, 223, 255);

    }

    public void toggle() {
        RightControllerTooltips.SetActive(!RightControllerTooltips.activeSelf);
        LeftControllerTooltips.SetActive(!LeftControllerTooltips.activeSelf);

        tooltipEnabled = RightControllerTooltips.activeSelf;

        if (tooltipEnabled) { // turn on highlights
            turnOnHighlights();

        }
        else // turn off highlights
        {
            turnOffHighlights();
        }
    }

    private void turnOnHighlights()
    {
        RightController.GetComponent<VRTK_ControllerHighlighter>().HighlightElement(SDK_BaseController.ControllerElements.Trigger, triggerColor);
        RightController.GetComponent<VRTK_ControllerHighlighter>().HighlightElement(SDK_BaseController.ControllerElements.Touchpad, rightTouchpadColor);
        RightController.GetComponent<VRTK_ControllerHighlighter>().HighlightElement(SDK_BaseController.ControllerElements.ButtonTwo, rightButtonColor);

        LeftController.GetComponent<VRTK_ControllerHighlighter>().HighlightElement(SDK_BaseController.ControllerElements.Trigger, triggerColor);
        LeftController.GetComponent<VRTK_ControllerHighlighter>().HighlightElement(SDK_BaseController.ControllerElements.Touchpad, leftTouchpadColor);
        LeftController.GetComponent<VRTK_ControllerHighlighter>().HighlightElement(SDK_BaseController.ControllerElements.ButtonTwo, leftButtonColor);
    }

    private void turnOffHighlights()
    {
        RightController.GetComponent<VRTK_ControllerHighlighter>().UnhighlightController();
        LeftController.GetComponent<VRTK_ControllerHighlighter>().UnhighlightController();
    }
}
