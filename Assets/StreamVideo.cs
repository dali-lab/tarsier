using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
        
public class StreamVideo : MonoBehaviour {
    public RawImage image;
    public VideoPlayer videoPlayer;
    public VideoSource videoSource;

	// Use this for initialization
	void Start () {
        Application.runInBackground = true;
        //StartCoroutine(playVideo());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
