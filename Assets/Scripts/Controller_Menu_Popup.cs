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

        public GameObject canvasObject;
        public GameObject vision;
        public GameObject scenePanel;

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

        private float waitTime = 0.3f;
        private float timer = 0.0f;
        private bool timerStart = false;

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
            canvasObject.SetActive(true);
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
            if (isShown)
            {
                gameObject.GetComponent<Select_Scene_Option>().Execute();
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
            scenePanel.SetActive(!scenePanel.activeSelf);
        }

        private void ControllerEvents_JoystickMoved(object sender, ControllerInteractionEventArgs e)
        {
            float touchpadAngle = e.touchpadAngle;

            if (isShown)
            {
                if (!timerStart && (touchpadAngle >= 10f && touchpadAngle <= 90f) || (touchpadAngle >= 270f && touchpadAngle <= 350f))
                {
                    PanelMenuItemController.SwipeTop(gameObject);
                    timerStart = true;

                } else if (!timerStart && touchpadAngle >= 100f && touchpadAngle <= 260f)
                {
                    PanelMenuItemController.SwipeBottom(gameObject);
                    timerStart = true;
                }
            }
            
        }

        private void ControllerEvents_ButtonOnePressed(object sender, ControllerInteractionEventArgs e)
        {
            // Toggle vision effects
            // vision.GetComponent<Refocus>().enabled = !vision.GetComponent<Refocus>().enabled;
            vision.GetComponent<Wilberforce.Colorblind>().enabled = !vision.GetComponent<Wilberforce.Colorblind>().enabled;
            vision.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour2>().enabled = !vision.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour2>().enabled;
            vision.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = !vision.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled;
            vision.GetComponent<DOFManager>().enabled = !vision.GetComponent<DOFManager>().enabled;

        }

        /// <summary>
        /// The ShowMenu method is used to show the menu.
        /// </summary>
        public virtual void ShowMenu()
        {
            if (!isShown)
            {
                PanelMenuItemController.Show(gameObject);
            }
            isShown = true;
            //InitTweenMenuScale(isShown);
            canvasObject.transform.localScale = Vector3.one * CanvasScaleSize;
            gameObject.GetComponent<Select_Scene_Option>().ResetOption();
        }

        /// <summary>
        /// The HideMenu method is used to hide the menu.
        /// </summary>
        public virtual void HideMenu()
        {
            if (isShown)
            {
                PanelMenuItemController.Hide(gameObject);
            }
            isShown = false;
            //InitTweenMenuScale(isShown);
            canvasObject.transform.localScale = Vector3.zero;
        }

        protected virtual void Start()
        {

            if (canvasObject == null || canvasObject.GetComponent<Canvas>() == null)
            {
                VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "PanelMenuController", "Canvas", "a child"));
            }

            canvasObject.transform.localScale = Vector3.zero;
        }


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

            if (timer > waitTime)
            {
                timer = timer - waitTime;
                timerStart = false;
            } else if (timerStart)
            {
                timer += Time.deltaTime;
            }
        }
       

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
        
        
    }
}
