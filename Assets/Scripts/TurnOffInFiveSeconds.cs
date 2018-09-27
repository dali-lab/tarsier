using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffInFiveSeconds : MonoBehaviour {

    private Text welcomeText;

	// Use this for initialization
	void Start () {
        welcomeText = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update() {
        if (Time.timeSinceLevelLoad > 15)
        {
            welcomeText.enabled = false;
        }
	}
}
