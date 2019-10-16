using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStarter : MonoBehaviour {

	void Start () {
        StartCoroutine(LateStart(.5f));
	}

    private IEnumerator LateStart(float startTime)
    {
        yield return new WaitForSeconds(startTime);


        ControlsChangeHelper controls = gameObject.GetComponent<ControlsChangeHelper>();

        controls.SceneSwitchControl(true);
        controls.TeleportControl(true);
        controls.VisionControl(true);
        controls.StartupControl(false);

        //switch (SceneManager.GetActiveScene().buildIndex)
        //{
        //    // Tutorial
        //    case 0:
        //        controls.SceneSwitchControl(false);
        //        controls.TeleportControl(false);
        //        controls.VisionControl(false);
        //        controls.StartupControl(true);
        //        break;
        //    // DOF
        //    case 1:
        //        if (Global.guided)
        //        {
        //            controls.SceneSwitchControl(true);
        //            controls.TeleportControl(true);
        //            controls.VisionControl(true);
        //            controls.StartupControl(false);
        //        }
        //        else
        //        {
        //            controls.SceneSwitchControl(true);
        //            controls.TeleportControl(true);
        //            controls.VisionControl(true);
        //            controls.StartupControl(false);
        //        }
        //        break;
        //    // Brightness
        //    case 2:
        //        if (Global.guided)
        //        {
        //            controls.SceneSwitchControl(false);
        //            controls.TeleportControl(true);
        //            controls.VisionControl(true);
        //            controls.StartupControl(false);
        //        }
        //        else
        //        {
        //            controls.SceneSwitchControl(true);
        //            controls.TeleportControl(true);
        //            controls.VisionControl(true);
        //            controls.StartupControl(false);
        //        }
        //        break;
        //    // Forest
        //    case 3:
        //        controls.SceneSwitchControl(true);
        //        controls.TeleportControl(true);
        //        controls.VisionControl(true);
        //        controls.StartupControl(false);
        //        break;
        //    default:
        //        Debug.Log("This should not happen");
        //        controls.SceneSwitchControl(false);
        //        controls.TeleportControl(false);
        //        controls.VisionControl(false);
        //        controls.StartupControl(false);
        //        break;
        //}
    }
}
