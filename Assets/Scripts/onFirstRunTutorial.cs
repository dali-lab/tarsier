using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;


public class onFirstRunTutorial : MonoBehaviour {

    public VRTK_ControllerTooltips.TooltipButtons element;
    public VRTK_ControllerTooltips VRTK_ControllerTooltips;


    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Running");

            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            VRTK_ControllerTooltips.ToggleTips(true, element);

        }
        else
        {
            Debug.Log("NOT First Time Running");

            VRTK_ControllerTooltips.ToggleTips(false, element);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
