using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the playing of the audio depending on the static humanVision boolean
/// </summary>

public class KatydidSoundManager : MonoBehaviour {

    public AudioClip humanKatydidAudio;
    public AudioClip tarsierKatydidAudio;

    public AudioSource audioSource;

    private void Start()
    {
        if (humanKatydidAudio == null || tarsierKatydidAudio == null)
        {
            string error = "";
            if (tarsierKatydidAudio == null)
                error = "The Tarsier Katydid Audio Clip has not been initialized in the inspector, please do this now. ";
            if (humanKatydidAudio == null)
                error += "The Human Katydid Audio Clip has not been initialized in the inspector, please do this now. ";
            Debug.LogError(error);
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from this gameobject.");
        }
    }

    public void PlayHumanAudioClip()
    {

        if (audioSource.clip != humanKatydidAudio)
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = humanKatydidAudio;
            audioSource.Play();
        }

    }

    public void PlayTarsierAudioClip()
    {
        if (audioSource.clip != tarsierKatydidAudio)
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = tarsierKatydidAudio;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (VRTK.Controller_Menu_Popup.humanVision)
        {
            PlayHumanAudioClip();
        }
        else
        {
            PlayTarsierAudioClip();
        }
    }
}

