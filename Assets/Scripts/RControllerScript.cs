// Panel Menu|Prefabs|0130
namespace VRTK
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    ///  My own script, taken from Panel Menu but changed to be on a controller instead of on another object
    /// </summary>
    public class RControllerScript : MonoBehaviour
    {
        public enum TouchpadPressPosition
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }

        public GameObject canvasObject;
        public GameObject instructions;

        private Transform rotateTowards;
        [Tooltip("The scale multiplier, which relates to the scale of parent interactable object.")]
        public float zoomScaleMultiplier = 1f;
        [Tooltip("The PanelMenuItemController, which is triggered by pressing the trigger")]
        public VRTK_PanelMenuItemController PanelMenuItemController;

        // Relates to scale of canvas on panel items.
        protected const float CanvasScaleSize = 0.001f;

        protected VRTK_ControllerEvents controllerEvents;
        protected bool isShown = false;
        protected Coroutine tweenMenuScaleRoutine;

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
            controllerEvents.TriggerPressed += ControllerEvents_TriggerPressed;

        }

        private void OnDisable()
        {
            if (controllerEvents != null)
            {
                controllerEvents.ButtonOnePressed -= ControllerEvents_ButtonOnePressed;
                controllerEvents.ButtonTwoPressed -= ControllerEvents_ButtonTwoPressed;
                controllerEvents.TriggerPressed -= ControllerEvents_TriggerPressed;
            }
        }

        private void ControllerEvents_ButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
        {
            // panel = panel.SetActive;
        }

        private void ControllerEvents_ButtonOnePressed(object sender, ControllerInteractionEventArgs e)
        {
            if (!isShown)
            {
                // Toggle vision effects

            }
            else
            {
                PanelMenuItemController.SwipeBottom(gameObject);
            }

        }

        private void ControllerEvents_TriggerPressed(object sender, ControllerInteractionEventArgs e)
        {

        }




        protected virtual void Start()
        {

            if (canvasObject == null || canvasObject.GetComponent<Canvas>() == null)
            {
                VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "PanelMenuController", "Canvas", "a child"));
            }

            canvasObject.transform.localScale = Vector3.zero;
        }

        //private void Controller_Menu_Popup_DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
        //{
        //    // See if we're already in the mode - if so, we'll execute whatever option we're on, else we'll open the menu
        //    if (isShown)
        //    {
        //        HideMenu();
        //        gameObject.GetComponent<Select_Scene_Option>().Execute();
        //        UnbindControllerEvents();
        //    }
        //    else
        //    {
        //        ShowMenu();
        //        BindControllerEvents();
        //    }
        //}

        protected virtual void Update()
        {
            if (rotateTowards == null)
            {
                rotateTowards = VRTK_DeviceFinder.HeadsetTransform();
                if (rotateTowards == null)
                {
                    VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.COULD_NOT_FIND_OBJECT_FOR_ACTION, "PanelMenuController", "an object", "rotate towards"));
                }
            }

            if (isShown)
            {
                if (rotateTowards != null)
                {
                    transform.rotation = Quaternion.LookRotation((rotateTowards.position - transform.position) * -1, Vector3.up);
                }
            }
        }

        //protected virtual void BindControllerEvents()
        //{
        //    controllerEvents.TouchpadAxisChanged += ControllerEvents_TouchpadAxisChanged;
        //}

        //protected virtual void UnbindControllerEvents()
        //{
        //    controllerEvents.TouchpadAxisChanged -= ControllerEvents_TouchpadAxisChanged;

        //}

        protected virtual void InitTweenMenuScale(bool show)
        {
            if (tweenMenuScaleRoutine != null)
            {
                StopCoroutine(tweenMenuScaleRoutine);
            }
            if (enabled)
            {
                tweenMenuScaleRoutine = StartCoroutine(TweenMenuScale(show));
            }
        }

        protected virtual IEnumerator TweenMenuScale(bool show)
        {
            float targetScale = 0;
            Vector3 direction = -1 * Vector3.one;
            if (show)
            {
                canvasObject.transform.localScale = new Vector3(CanvasScaleSize, CanvasScaleSize, CanvasScaleSize);
                targetScale = zoomScaleMultiplier * CanvasScaleSize;
                direction = Vector3.one;
            }
            int i = 0;
            while (i < 250 && ((show && transform.localScale.x < targetScale) || (!show && transform.localScale.x > targetScale)))
            {
                canvasObject.transform.localScale += direction * Time.deltaTime * 4f * zoomScaleMultiplier;
                yield return true;
                i++;
            }
            canvasObject.transform.localScale = direction * targetScale;

            if (!show)
            {
                canvasObject.transform.localScale = Vector3.zero;
            }
        }

        //private void ControllerEvents_TouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        //{
        //    Debug.Log("Controller_Menu_Popup: Touchpad Axis Changed!!");

        //    float touchpadAngle = e.touchpadAngle;

        //    if ((touchpadAngle > 0 && touchpadAngle < 70 || touchpadAngle > 300))
        //    {
        //        PanelMenuItemController.SwipeTop(gameObject);

        //    } else if ((touchpadAngle > 0 && touchpadAngle < 240))
        //    {

        //        PanelMenuItemController.SwipeBottom(gameObject);

        //    } else
        //    {

        //    }
        //}



    }
}
