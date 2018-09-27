using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class hitWall : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    
    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObj.index);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Wall")))
        {
            device.TriggerHapticPulse(1000);
        }
    }
}
