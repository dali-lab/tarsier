using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

// @author: MixPixVisuals on Youtube (Creating a simple auto-focus in Unity 5)
// updated by Stephen Liao for new Post Processing Stack and smoother focus by using gradual updating
// updated by Naman Goyal, to change to Updating every .1 seconds through the Update instead of InvokeRepeating
// updated by Naman Goyal, to make looking at UI popups focus immediately
// updated by Naman Goyal, to use a Lerp function instead of Stephen's gradual updater
public class Refocus : MonoBehaviour {
    public PostProcessingProfile profile;
    private float updateRate = .05f;
    private float refocusDuration = .2f;

    private float time_since_last_update = 0f;

    static int mainMask = (1 << 0) | (1 << 5);
    RaycastHit hit;
    public float HighApertureVal = 20f;

    void Update () {
        time_since_last_update += Time.deltaTime;
        Debug.Log(Time.timeScale);
        if (time_since_last_update < updateRate) return;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, mainMask))
        {
            DepthOfFieldModel.Settings dofSettings = profile.depthOfField.settings;

            // If hit a UI object, focus immediately
            if (hit.transform.gameObject.layer == 5)
            {
                dofSettings.focusDistance = hit.distance;
                dofSettings.aperture = HighApertureVal;
            }
            // Else gradually focus in
            else
            {
                dofSettings.focusDistance = Mathf.Lerp(dofSettings.focusDistance, hit.distance, updateRate / refocusDuration);
                dofSettings.aperture = 5.6f;
            }
            profile.depthOfField.settings = dofSettings;
        }

        time_since_last_update = 0f;
	}
}
