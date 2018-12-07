using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DofControllerScript : MonoBehaviour {

    public Refocus refocusScript;                   // Reference to refocus script on camera so we can know if a transition has occured   
    public GameObject nextScenePopup;               // Reference to gameObject that has the canvas and script 
    public GameObject doMorePopup;                  // Reference to gameObject that has the canvas telling player to do more
    public GameObject teachControlPopup;            // Reference to gameObject that has the canvas teaching the control
    public GameObject RControllerModel;             // Reference to gameObject that has the right controller's model

    Coroutine timeOutCoroutine;

    private void Start()
    {
        nextScenePopup.GetComponent<Canvas>().enabled = false;
        nextScenePopup.GetComponent<GoToNextScene>().enabled = false;
        doMorePopup.GetComponent<Canvas>().enabled = false;
        teachControlPopup.GetComponent<Canvas>().enabled = false;

        if (Global.guided)
        {
            StartCoroutine(TeachControl());
        }
    }

    private IEnumerator TeachControl()
    {
        // Wait till L Controller is expanded
        while (RControllerModel.transform.childCount == 0)
        {
            yield return null;
        }

        teachControlPopup.GetComponent<Canvas>().enabled = true;
        // Change shader
        GameObject trackpad = RControllerModel.transform.GetChild(12).gameObject;
        trackpad.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Regular");

        StartCoroutine(CheckTransitions());
        timeOutCoroutine = StartCoroutine(TimeOut());
    }

    private IEnumerator CheckTransitions()
    {
        int transitions = 0;                            // Number of times the user has transitioned between eyes
        float timeOfLastTransition = -2f;               // To store when the last transition occured
        float minTimeBetweenTransitions = 1f;           // Minimum amount of time between transitions for it to be valid
        bool wasHuman = true;                           // Last known state

        while(transitions < 5)
        {
            yield return new WaitForSeconds(.1f);
            
            // If the two bools are different, then player is still in same state
            if (wasHuman != refocusScript.enabled)
            {
                wasHuman = !refocusScript.enabled;
                continue;
            }

            if (transitions == 0)
            {
                GameObject trackpad = RControllerModel.transform.GetChild(12).gameObject;
                trackpad.GetComponent<Renderer>().material.shader = Shader.Find("Standard");

                teachControlPopup.GetComponent<Canvas>().enabled = false;
            }

            // If not enough time has passed
            if (Time.time - timeOfLastTransition < minTimeBetweenTransitions)
            {
                timeOfLastTransition = Time.time;
                wasHuman = !refocusScript.enabled;
                continue;
            }
            // Only if a transition occurred and it took the required amount of time
            transitions++;
            timeOfLastTransition = Time.time;
            wasHuman = !refocusScript.enabled;
        }

        // Popup the canvas
        nextScenePopup.GetComponent<Canvas>().enabled = true;
        nextScenePopup.GetComponent<GoToNextScene>().enabled = true;

        StopCoroutine(timeOutCoroutine);
        doMorePopup.GetComponent<Canvas>().enabled = false;
    }

    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(60f);
        while(true)
        {
            doMorePopup.GetComponent<Canvas>().enabled = true;
            yield return new WaitForSeconds(5f);
            doMorePopup.GetComponent<Canvas>().enabled = false;
            yield return new WaitForSeconds(10f);
        }
    }
}
