using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PostProcessing;
using Wilberforce;


public class toggleEnable : MonoBehaviour
{

    public PostProcessingProfile ppProfile;
    public Colorblind cbProfile;
    //public bool isEnabled;
    //UnityEvent toggleEvent;

    // Use this for initialization
    void Start()
    {
        ppProfile = GetComponent<PostProcessingBehaviour>().profile;
        cbProfile = GetComponent<Colorblind>();
    }

    void onEnable()
    {
        print("Ben Coo-per");
        ppProfile.depthOfField.enabled = true;
        ppProfile.vignette.enabled = false;
        cbProfile.enabled = true;
    }
    void onDisable()
    {
        ppProfile.depthOfField.enabled = false;
        ppProfile.vignette.enabled = true;
        cbProfile.enabled = false;
    }
}
