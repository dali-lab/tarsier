using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBugColor : MonoBehaviour
{

    public Color humanColor;
    public Color tarsierColor;
    public Material bugMaterial;

    private void Awake()
    {
        bugMaterial.color = humanColor;
    }

    public void SwitchColor(bool humanVision)
    {
        if (humanVision)
        {
            bugMaterial.color = humanColor;
        }
        else
        {
            bugMaterial.color = tarsierColor;
        }
    }
}
