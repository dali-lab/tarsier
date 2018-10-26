using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

// @author: MixPixVisuals on Youtube (Creating a simple auto-focus in Unity 5)
// updated by Stephen Liao for new Post Processing Stack and smoother focus by using gradual updating
// updated by Naman Goyal, to change to Updating every .1 seconds through the Update instead of InvokeRepeating
public class Refocus : MonoBehaviour {
    public PostProcessingProfile profile;
    private float updateRate = .1f;
    private float refocusDuration = .6f;

    private float time_since_last_update = 0f;

    int mask = 1;
    RaycastHit hit;

    void Update () {
        time_since_last_update += Time.deltaTime;
        Debug.Log(Time.timeScale);
        if (time_since_last_update < .1) return;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, mask))
        {
            DepthOfFieldModel.Settings dofSettings = profile.depthOfField.settings;
            dofSettings.focusDistance = getGradualFocusDistance(hit.distance, dofSettings.focusDistance);
            profile.depthOfField.settings = dofSettings;
        }
        time_since_last_update = 0f;

	}

    float getGradualFocusDistance (float currentFocusDistance, float oldFocusDistance)
    {
        float deltaFocusDistance = currentFocusDistance - oldFocusDistance;
        float stepSize = refocusDuration / updateRate;
        float change = deltaFocusDistance / stepSize;

        float focusDistance = oldFocusDistance + change;
        return focusDistance;
    }
}
