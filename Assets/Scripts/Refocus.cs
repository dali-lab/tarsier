using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

// @author: MixPixVisuals on Youtube (Creating a simple auto-focus in Unity 5)
// updated by Stephen Liao for new Post Processing Stack and smoother focus by using gradual updating
public class Refocus : MonoBehaviour {
    public PostProcessingProfile profile;
    private float updateRate = .1f;
    private float refocusDuration = 2f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("RefocusUpdate", 0, updateRate);
    }
	
	void RefocusUpdate () {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            DepthOfFieldModel.Settings dofSettings = profile.depthOfField.settings;
            dofSettings.focusDistance = getGradualFocusDistance(hit.distance, dofSettings.focusDistance);
            profile.depthOfField.settings = dofSettings;
        }

	}

    float getGradualFocusDistance (float currentFocusDistance, float oldFocusDistance)
    {
        float deltaFocusDistance = currentFocusDistance - oldFocusDistance;
        float stepSize = refocusDuration / updateRate;
        float change = deltaFocusDistance / stepSize;
        //if (System.Math.Abs(oldFocusDistance - currentFocusDistance) > 1f & System.Math.Abs(change) < 1f)
        //{
        //    change = System.Math.Sign(change) * 1f;
        //}
        float focusDistance = oldFocusDistance + change;
        return focusDistance;
    }
}
