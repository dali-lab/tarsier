using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ReenableTeleport : MonoBehaviour {

    private VRTK_ControllerEvents events;
    public static GameObject smoothed_thing;

    protected virtual void Awake()
    {
        events = gameObject.GetComponent<VRTK_ControllerEvents>();
    }

    protected virtual void OnEnable()
    {
        events.TriggerHairlineStart += Events_TriggerHairlineStart;
    }

    protected virtual void OnDisable()
    {
        events.TriggerHairlineStart -= Events_TriggerHairlineStart;
    }

    private void Events_TriggerHairlineStart(object sender, ControllerInteractionEventArgs e)
    {
        if (smoothed_thing) 
        {
            smoothed_thing.SetActive(true);
        }
    }
}
