using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomly starts playing Katydid sounds
/// in the VRTK.Controller_Menu_Popup
/// </summary>
public class StartKatydidSounds : MonoBehaviour {

    public AudioSource audioSource;

    private void Start()
    {

        Invoke("playSound", Random.Range(0.0f, 10.0f));
    }

    public void playSound()
    {

        if (!audioSource.isPlaying)
        {
            Debug.Log("test");
            audioSource.Play();

        }

    }
}
