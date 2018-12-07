using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Track : MonoBehaviour {

    Transform cam = null;

    public float dist = 10f;
    public float xOffset = 0f;
    public float yOffset = 0f;

	void Start () {
        CheckCamera();
	}
	
	void Update () {
        CheckCamera();

        gameObject.transform.position = cam.position + cam.forward * dist + xOffset * cam.right + yOffset * cam.up;
        gameObject.transform.rotation = cam.rotation;
	}

    private void CheckCamera()
    {
        if (!cam)
            cam = VRTK_DeviceFinder.HeadsetTransform();
    }
}
