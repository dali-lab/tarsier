using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestControllerScript : MonoBehaviour {

    public GameObject teachControlCanvas;
    public GameObject LControllerModel;
   

    // Use this for initialization
    void Start () {
        teachControlCanvas.GetComponent<Canvas>().enabled = false;

        if (Global.guided)
        {
            StartCoroutine(Control());
        }

    }

 
    private IEnumerator Control()
    {
        yield return new WaitForSeconds(60f);

        // Show the control canvas
        teachControlCanvas.GetComponent<Canvas>().enabled = true;
        Global.guided = false;
        Highlight();
        while(true)
        {
            yield return new WaitForSeconds(15f);
            teachControlCanvas.GetComponent<Canvas>().enabled = false;
            UnHighlight();

            yield return new WaitForSeconds(20f);
            teachControlCanvas.GetComponent<Canvas>().enabled = true;
            Highlight();
        }
    }

    private void Highlight()
    {
        GameObject trigger  = LControllerModel.transform.GetChild(15).gameObject;
        GameObject touchpad = LControllerModel.transform.GetChild(12).gameObject;

        trigger.GetComponent<Renderer>().material.shader  = Shader.Find("Outlined/Regular");
        touchpad.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Regular");
    }
    
    private void UnHighlight()
    {
        GameObject trigger = LControllerModel.transform.GetChild(15).gameObject;
        GameObject touchpad = LControllerModel.transform.GetChild(12).gameObject;

        trigger.GetComponent<Renderer>().material.shader  = Shader.Find("Standard");
        touchpad.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
    }
}
