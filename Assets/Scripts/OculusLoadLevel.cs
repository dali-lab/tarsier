// Edited by Amon to black out the cameras when loading scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class OculusLoadLevel : MonoBehaviour
{

    public string levelName;
    public GameObject headsetFader; // Any object containing the "VRTK_HeadsetFade" script

    private bool loading = false;

    IEnumerator LoadYourSceneAsync()
    {
        // Get the HeadsetFade script, and start an instant fade to black (gradual fades wont work as the FPS is near-zero during scene loads)
        VRTK_HeadsetFade fader = headsetFader.GetComponent<VRTK_HeadsetFade>();
        fader.Fade(Color.black, 0f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone)
        {
            loading = true;
            yield return null;
        }

        if (asyncLoad.isDone)
        {
            loading = false;
            fader.Unfade(0f); // When the scene is loaded, unfade the camera(s)
        }
    }

    public void Trigger()
    {
        if (!string.IsNullOrEmpty(levelName))
            StartCoroutine(LoadYourSceneAsync());
    }
}
