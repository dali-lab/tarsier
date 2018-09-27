using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticPulseWall : MonoBehaviour {

    public bool hapticFlag;
    private int i = 0;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
 
	// Use this for initialization
	private void Awake() {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        print(trackedObject);
	}

    public void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.tag);
        if ((other.CompareTag("Wall")))
        {
            Debug.Log("WAAAALLLLL");
            if (!trackedObject)
                Debug.Log("trigger null");
            else
            {
                Debug.Log("trigger ok!!");
                Debug.Log(trackedObject);
            }
            //Debug.Log((int)trackedObject.index);
            //device = SteamVR_Controller.Input((int)trackedObject.index);
            StartCoroutine("buzz");
        }
    }

    void FixedUpdate()
    {
        //device = SteamVR_Controller.Input((int)trackedObject.index);
    }

    IEnumerator buzz()
    {
        Debug.Log("in buzzzz");
        Debug.Log(trackedObject);
        for (float i = 0; i < 2000; i += Time.deltaTime)
        {
            device = SteamVR_Controller.Input((int)trackedObject.index);   
            device.TriggerHapticPulse(3000);
            yield return null;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Wall")))
        {
            //hapticFlag = false;
        }
    }


    // Update is called once per frame
    public void Update() {
        //device = SteamVR_Controller.Input((int)trackedObject.index);
        if (!trackedObject)
            Debug.Log("update null");
        else
            Debug.Log("update ok");

        /* if (hapticFlag)
         {
             print("kristie");
         } else
         {
             print(hapticFlag+" stephanie");
         }*/
        //print(hapticFlag);
        //if (this.hapticFlag)
        //{
        //    device.TriggerHapticPulse(1000);
        //    print("AIYAH");
        // }
    }

}
