using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OculusLoadLevel : MonoBehaviour {

    public string levelName;
    private bool loading = false;

    IEnumerator LoadYourSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone)
        {
            loading = true;
            yield return null;
        }

        if (asyncLoad.isDone)
        {
            loading = false;
        }
    }

    public void Trigger()
    {
        Debug.Log("Inside Trigger");
        if (!string.IsNullOrEmpty(levelName))
            StartCoroutine(LoadYourSceneAsync());
    }
}
