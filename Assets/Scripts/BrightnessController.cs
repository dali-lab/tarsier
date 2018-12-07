using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessController : MonoBehaviour {

    public float neededDistance = 20f;
    public VRTK.VRTK_BasicTeleport teleporter;
 
    public GameObject nextScenePopup;               // Reference to gameObject that has the canvas and script 
    public GameObject doMorePopup;                  // Reference to gameObject that has the canvas telling player to do more
    public GameObject teachControlPopup;            // Reference to gameObject that has the canvas teaching the controls

    public GameObject RControllerModel;             // Reference to gameObject that has the Right controller model

    Coroutine timeOutCoroutine;

    public GameObject player;                       // Reference to the player's position
    Vector3 origPosition;                           // To store the position before a teleport

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
        while (RControllerModel.transform.childCount == 0)
        {
            yield return null;
        }

        teachControlPopup.GetComponent<Canvas>().enabled = true;
        GameObject trigger = RControllerModel.transform.GetChild(15).gameObject;
        trigger.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Regular");

        timeOutCoroutine = StartCoroutine(TimeOut());

        teleporter.Teleporting += Teleporter_Teleporting;
        teleporter.Teleported += Teleporter_Teleported;
    }

    private void Teleporter_Teleported(object sender, VRTK.DestinationMarkerEventArgs e)
    {
        float travelled = Vector3.Distance(origPosition, player.transform.position);
        neededDistance -= travelled;

        if (neededDistance < 0)
        {
            nextScenePopup.GetComponent<Canvas>().enabled = true;
            nextScenePopup.GetComponent<GoToNextScene>().enabled = true;

            StopCoroutine(timeOutCoroutine);
            doMorePopup.GetComponent<Canvas>().enabled = false;
        }
    }

    private void Teleporter_Teleporting(object sender, VRTK.DestinationMarkerEventArgs e)
    {
        if (neededDistance == 20f)
        {
            teachControlPopup.GetComponent<Canvas>().enabled = false;
            GameObject trigger = RControllerModel.transform.GetChild(15).gameObject;
            trigger.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }

        Vector3 pos = player.transform.position;
        origPosition = new Vector3(pos.x, pos.y, pos.z);
    }

    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(60f);
        while (true)
        {
            doMorePopup.GetComponent<Canvas>().enabled = true;
            yield return new WaitForSeconds(5f);
            doMorePopup.GetComponent<Canvas>().enabled = false;
            yield return new WaitForSeconds(10f);
        }
    }
}
