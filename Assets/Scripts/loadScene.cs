using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadScene : MonoBehaviour {

    SteamVR_LoadLevel kkg;

    public void Start()
    {
        print("hiii im startttingggg");

        kkg = GetComponent<SteamVR_LoadLevel>();
    }


	void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("MainCamera")))
        {

            kkg.enabled = true;
            
        }
    }
}
