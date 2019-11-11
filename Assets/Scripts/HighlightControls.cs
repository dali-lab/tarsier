// Highlights buttons when they are touched (using capacative sensing)
// Instantiates a HighlightObject for every touch-sensitive button, and shows them when they're touched

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HighlightControls : MonoBehaviour
{
    // VRTK's event system for when buttons are touched or pressed (or anything)
    protected VRTK_ControllerEvents controllerEvents;

    // Whether or not this script is on the right controller (true if on the right controller, false if on the left)
    public bool RightHand;
    // How fast to fade the highlights in and out
    public float FadeSpeed = 2;
    // What object to instantiate as the highlight, what object is overlayed on the buttons
    public GameObject HighlightObject;
    // The colors for every button's highlight
    public Color ButtonOneColor = Color.green;
    public Color ButtonTwoColor = Color.green;
    public Color TouchpadColor = Color.green;
    public Color TriggerColor = Color.green;
    public Color GripColor = Color.green;

    // GameObjects for each button's HighlightObject
    private GameObject ButtonOne;
    private GameObject ButtonTwo;
    private GameObject Touchpad;
    private GameObject Trigger;
    private GameObject Grip;

    private void Start()
    {
        // Get the VRTK ControllerEvents script attached to the same GameObject as this script
        controllerEvents = GetComponent<VRTK_ControllerEvents>();

        // Mirror the positions of the buttons if the script is on the right controller
        // (I found the initial values using the left hand)
        float sign = 1f;
        if (RightHand) {
            sign = -1f;
        }

        // Create the highlight objects (I pretty much just used trial and error for their position and size)
        ButtonOne = CreateHighlight(new Vector3(-0.00146f * sign, -0.00313f, -0.00488f), new Vector3(0.012f, 0.012f, 0.012f));
        ButtonTwo = CreateHighlight(new Vector3(0.0023f * sign, -0.0007f, 0.0088f), new Vector3(0.012f, 0.012f, 0.012f));
        Touchpad = CreateHighlight(new Vector3(-0.01791f * sign, 0.0067f, 0.0079f), new Vector3(0.016f, 0.016f, 0.016f));
        Trigger = CreateHighlight(new Vector3(-0.0095f * sign, -0.0207f, 0.0218f), new Vector3(0.028f, 0.028f, 0.028f));
        Grip = CreateHighlight(new Vector3(0.0028f * sign, -0.0302f, -0.0226f), new Vector3(0.026f, 0.026f, 0.026f));
    }

    private void Update()
    {
        // Update the highlights for every button, based on whether or not the button is pressed
        UpdateHighlight(ButtonOne, controllerEvents.buttonOneTouched, ButtonOneColor);
        UpdateHighlight(ButtonTwo, controllerEvents.buttonTwoTouched, ButtonTwoColor);
        UpdateHighlight(Touchpad, controllerEvents.touchpadTouched, TouchpadColor);
        UpdateHighlight(Trigger, controllerEvents.triggerTouched, TriggerColor);
        UpdateHighlight(Grip, controllerEvents.gripHairlinePressed, GripColor);
    }
    
    // Create the HighlightObjects for a button
    // Takes a position for the object (where the button is in relation to the controller), and a scale
    // Returns an instance of the HighlightObject for the button
    private GameObject CreateHighlight(Vector3 Position, Vector3 Scale)
    {
        // Instantiate the object at the correct position
        GameObject Instance = Instantiate(HighlightObject, Position, Quaternion.identity, gameObject.transform);
        Instance.transform.localScale = Scale; // Scale the highlight
        Instance.transform.parent = gameObject.transform;
        // Hide the highlight to start
        Instance.GetComponent<Renderer>().enabled = false;

        return Instance;
    }

    // Updates the HighlightObject for a button
    // Takes the GameObject of the highlight, whether or not the button is touched, and the color of the highlight
    private void UpdateHighlight(GameObject Control, bool Active, Color HighlightColor)
    {
        Renderer Render = Control.GetComponent<Renderer>(); // Get the renderer of the HighlightObject
        if (Active) {      
            Render.enabled = true; // Show the highlight
            // If the highlight is transparent, increase its opacity until it's opaque
            if (Render.material.color.a < 1f) {
                float a = Render.material.color.a + Time.deltaTime * FadeSpeed; // Calculate the new opacity
                // Change the highlight's color
                Render.material.color = new Color(HighlightColor.r, HighlightColor.g, HighlightColor.b, a);
            }
        }
        else {
            // If the highlight isn't fully transparent, decrease its opacity
            if (Render.material.color.a > 0f) {
                float a = Render.material.color.a - Time.deltaTime * FadeSpeed;
                Render.material.color = new Color(HighlightColor.r, HighlightColor.g, HighlightColor.b, a);
            }
            else { // Once the highlight is fully transparent, hide it
                Render.enabled = false;
            }
        }
    }
}
