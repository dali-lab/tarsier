// Panel Menu|Prefabs|0130
namespace VRTK
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    ///  My own script, taken from Panel Menu but changed to be on a controller instead of on another object
    /// </summary>
    public class Controller_Menu_Popup : MonoBehaviour
    {
        public enum TouchpadPressPosition
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }

        public GameObject vision;
        public GameObject scenePanel;
        public static bool humanVision;

        private Transform rotateTowards;
        [Tooltip("The scale multiplier, which relates to the scale of parent interactable object.")]
        public float zoomScaleMultiplier = 1f;
        [Tooltip("The PanelMenuItemController, which is triggered by pressing the trigger")]
        public VRTK_PanelMenuItemController PanelMenuItemController;

        // Relates to scale of canvas on panel items.
        protected const float CanvasScaleSize = 0.001f;

        protected VRTK_ControllerEvents controllerEvents;

        private void Awake()
        {
            //resets each scene to human vision
            vision.GetComponent<Wilberforce.Colorblind>().enabled = false;
            vision.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour2>().enabled = true;
            vision.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
            vision.GetComponent<DOFManager>().enabled = false;
            humanVision = true;
        }

        private void OnEnable()
        {
            controllerEvents = GetComponent<VRTK_ControllerEvents>();
            if (controllerEvents == null)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
                return;
            }

            controllerEvents.ButtonOnePressed += ControllerEvents_ButtonOnePressed;
            controllerEvents.ButtonTwoPressed += ControllerEvents_ButtonTwoPressed;
            controllerEvents.TouchpadAxisChanged += ControllerEvents_JoystickMoved;
            scenePanel.SetActive(false);

        }

        private void OnDisable()
        {
            if (controllerEvents != null)
            {
                controllerEvents.ButtonOnePressed -= ControllerEvents_ButtonOnePressed;
                controllerEvents.ButtonTwoPressed -= ControllerEvents_ButtonTwoPressed;
                controllerEvents.TouchpadAxisChanged -= ControllerEvents_JoystickMoved;
            }
        }


        private void ControllerEvents_ButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
        {
            scenePanel.SetActive(!scenePanel.activeSelf);
        }

        private void ControllerEvents_JoystickMoved(object sender, ControllerInteractionEventArgs e)
        {
        }

        private void ControllerEvents_ButtonOnePressed(object sender, ControllerInteractionEventArgs e)
        {
            // Toggle vision effects
            vision.GetComponent<Wilberforce.Colorblind>().enabled = !vision.GetComponent<Wilberforce.Colorblind>().enabled;
            vision.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour2>().enabled = !vision.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour2>().enabled;
            vision.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = !vision.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled;
            vision.GetComponent<DOFManager>().enabled = !vision.GetComponent<DOFManager>().enabled;

            //change boolean to reflect the changed vision
            humanVision = !humanVision;
        }     
    }
}
