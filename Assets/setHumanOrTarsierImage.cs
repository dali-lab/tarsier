using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setHumanOrTarsierImage : MonoBehaviour {

    Image currentModeImage;
    public Sprite tarsierImage;
    public Sprite humanImage;

    // Use this for initialization
    void Start()
    {
        currentModeImage = GetComponent<Image>();
        // default is human
        currentModeImage.sprite = humanImage;
    }

    public void setHumanImage()
    {
        currentModeImage.sprite = humanImage;
    }

    public void setTarsierImage()
    {
        currentModeImage.sprite = tarsierImage;
    }
}

