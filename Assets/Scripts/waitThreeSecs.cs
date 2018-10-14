using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitThreeSecs : MonoBehaviour {

    float timer;
    float holdDur = 3f;
    SteamVR_LoadLevel kkg;

    void Start()
    {
        StartCoroutine(load());
        kkg = GetComponent<SteamVR_LoadLevel>();
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(3);
        kkg.enabled = true;
    }
	// Update is called once per frame
	/*void Update () {
        if ()
		
	}*/
}
