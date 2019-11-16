using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatBug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEatBug(Collision col)
    {
        if (col.gameObject.name == "simple_katydid_tree")
        {
            Debug.Log("Collision Detected");
        }
    }
}
