// Panel Menu|Prefabs|0130
namespace VRTK
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Adds a top-level controller to handle the display of up to four child PanelMenuItemController items which are displayed as a canvas UI panel.
    /// </summary>
    /// <remarks>
    /// **Prefab Usage:**
    ///  * Place the `VRTK/Prefabs/PanelMenu/PanelMenu` prefab as a child of the `VRTK_InteractableObject` the panel menu is for.
    ///  * Optionally remove the panel control menu item child GameObjects if they are not required, e.g. `PanelTopControls`.
    ///  * Set the panel menu item controllers on the `VRTK_PanelMenuController` script to determine which panel control menu items are available.
    ///  * The available panel control menu items can be activated by pressing the corresponding direction on the touchpad.
    /// </remarks>
    /// <example>
    /// `040_Controls_Panel_Menu` contains three basic interactive object examples of the PanelMenu in use.
    /// </example>
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

        protected virtual void Awake()
        {

            controllerEvents = gameObject.GetComponent<VRTK_ControllerEvents>();
        }

        protected virtual void Start()
        {
            controllerEvents.TriggerClicked += Controller_Menu_Popup_TriggerClicked;

            if (canvasObject == null || canvasObject.GetComponent<Canvas>() == null)
            {
                VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "PanelMenuController", "Canvas", "a child"));
            }

            canvasObject.transform.localScale = Vector3.zero;
        }

        private void Controller_Menu_Popup_TriggerClicked(object sender, ControllerInteractionEventArgs e)
        {
            // See if we're already in the mode - if so, we'll execute whatever option we're on, else we'll open the menu
            if (isShown)
            {
                gameObject.GetComponent<Select_Scene_Option>().Execute();
                UnbindControllerEvents();
            }
            else
            {
                ShowMenu();
                BindControllerEvents();
            }
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
        }

        protected virtual void BindControllerEvents()
        {
            controllerEvents.TouchpadPressed += ControllerEvents_TouchpadPressed;
        }

        protected virtual void UnbindControllerEvents()
        {
            controllerEvents.TouchpadPressed -= ControllerEvents_TouchpadPressed;
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

        private void ControllerEvents_TouchpadPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (e.touchpadAxis.y >= 0.5)
            {
                PanelMenuItemController.SwipeTop(gameObject);
            }
            else
            {
                PanelMenuItemController.SwipeBottom(gameObject);
            }
        }
    }
}
