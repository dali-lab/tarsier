using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour {

    public GameObject LChoice;
    public GameObject RChoice;

    public GameObject Warning;

    private bool left = true;

    void Start()
    {
        LChoice.SetActive(left);
        Warning.SetActive(!left);
        RChoice.SetActive(!left);

        Global.guided = left;
    }
	
	public void ToggleSlider()
    {
        left = !left;

        LChoice.SetActive(left);
        Warning.SetActive(!left);
        RChoice.SetActive(!left);

        Global.guided = left;
    }
}
