using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Interactable_Object_Grab : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GetComponent<VRTK_InteractableObject>() == null)
        {
            Debug.LogError("ahhhhhh it must be attched to an object that has the interactable object script attached");
            return;
        }

        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
	}

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("haha die bug die");
        gameObject.SetActive(false);
    }
}
