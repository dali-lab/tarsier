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

        // Swipe sensitivity / detection.
        protected const float AngleTolerance = 30f;
        protected const float SwipeMinDist = 0.2f;
        protected const float SwipeMinVelocity = 4.0f;

        protected VRTK_ControllerEvents controllerEvents;
        protected readonly Vector2 xAxis = new Vector2(1, 0);
        protected readonly Vector2 yAxis = new Vector2(0, 1);
        protected Vector2 touchStartPosition;
        protected Vector2 touchEndPosition;
        protected float touchStartTime;
        protected float currentAngle;
        protected bool isTrackingSwipe = false;
        protected bool isPendingSwipeCheck = false;
        protected bool isInPopup = false;
        protected bool isShown = false;
        protected Coroutine tweenMenuScaleRoutine;

        /// <summary>
        /// The ToggleMenu method is used to show or hide the menu.
        /// </summary>
        public virtual void ToggleMenu()
        {
            if (isShown)
            {
                HideMenu(true);
            }
            else
            {
                ShowMenu();
            }
        }

        /// <summary>
        /// The ShowMenu method is used to show the menu.
        /// </summary>
        public virtual void ShowMenu()
        {
            if (!isShown)
            {
                isShown = true;
                InitTweenMenuScale(isShown);
            }
        }

        /// <summary>
        /// The HideMenu method is used to hide the menu.
        /// </summary>
        /// <param name="force">If true then the menu is always hidden.</param>
        public virtual void HideMenu(bool force)
        {
            if (isShown && force)
            {
                isShown = false;
                InitTweenMenuScale(isShown);
            }
        }

        /// <summary>
        /// The HideMenuImmediate method is used to immediately hide the menu.
        /// </summary>
        public virtual void HideMenuImmediate()
        {
            if (isShown)
            {
                PanelMenuItemController.Hide(gameObject);
                HideMenu(true);
            }
            transform.localScale = Vector3.zero;
            canvasObject.transform.localScale = Vector3.zero;
            isShown = false;
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
            print("made it here");
            // See if we're already in the mode - if so, we'll execute whatever option we're on, else we'll open the menu
            if (isInPopup)
            {
                gameObject.GetComponent<Select_Scene_Option>().Execute();
            }
            else
            {
                PanelMenuItemController.Show(gameObject);
                ShowMenu();
                isInPopup = true;
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

            if (isPendingSwipeCheck)
            {
                CalculateSwipeAction();
            }
        }

        protected virtual void BindControllerEvents()
        {
            controllerEvents.TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouched);
            controllerEvents.TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadUntouched);
            controllerEvents.TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
        }

        protected virtual void UnbindControllerEvents()
        {
            controllerEvents.TouchpadTouchStart -= new ControllerInteractionEventHandler(DoTouchpadTouched);
            controllerEvents.TouchpadTouchEnd -= new ControllerInteractionEventHandler(DoTouchpadUntouched);
            controllerEvents.TouchpadAxisChanged -= new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
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
                targetScale = zoomScaleMultiplier;
                direction = Vector3.one;
            }
            int i = 0;
            while (i < 250 && ((show && transform.localScale.x < targetScale) || (!show && transform.localScale.x > targetScale)))
            {
                transform.localScale += direction * Time.deltaTime * 4f * zoomScaleMultiplier;
                yield return true;
                i++;
            }
            transform.localScale = direction * targetScale;

            if (!show)
            {
                canvasObject.transform.localScale = Vector3.zero;
            }
        }

        protected virtual void DoTouchpadTouched(object sender, ControllerInteractionEventArgs e)
        {
            touchStartPosition = new Vector2(e.touchpadAxis.x, e.touchpadAxis.y);
            touchStartTime = Time.time;
            isTrackingSwipe = true;
        }

        protected virtual void DoTouchpadUntouched(object sender, ControllerInteractionEventArgs e)
        {
            isTrackingSwipe = false;
            isPendingSwipeCheck = true;
        }

        protected virtual void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            ChangeAngle(CalculateAngle(e));

            if (isTrackingSwipe)
            {
                touchEndPosition = new Vector2(e.touchpadAxis.x, e.touchpadAxis.y);
            }
        }

        protected virtual void ChangeAngle(float angle, object sender = null)
        {
            currentAngle = angle;
        }

        protected virtual void CalculateSwipeAction()
        {
            isPendingSwipeCheck = false;

            float deltaTime = Time.time - touchStartTime;
            Vector2 swipeVector = touchEndPosition - touchStartPosition;
            float velocity = swipeVector.magnitude / deltaTime;

            if ((velocity > SwipeMinVelocity) && (swipeVector.magnitude > SwipeMinDist))
            {
                swipeVector.Normalize();
                float angleOfSwipe = Vector2.Dot(swipeVector, xAxis);
                angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

                // Left / right
                if (angleOfSwipe < AngleTolerance)
                {
                    OnSwipeRight();
                }
                else if ((180.0f - angleOfSwipe) < AngleTolerance)
                {
                    OnSwipeLeft();
                }
                else
                {
                    // Top / bottom
                    angleOfSwipe = Vector2.Dot(swipeVector, yAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                    if (angleOfSwipe < AngleTolerance)
                    {
                        OnSwipeTop();
                    }
                    else if ((180.0f - angleOfSwipe) < AngleTolerance)
                    {
                        OnSwipeBottom();
                    }
                }
            }
        }

        protected virtual void OnSwipeLeft()
        {
            PanelMenuItemController.SwipeLeft(gameObject);
        }

        protected virtual void OnSwipeRight()
        {
            PanelMenuItemController.SwipeRight(gameObject);
        }

        protected virtual void OnSwipeTop()
        {
            PanelMenuItemController.SwipeTop(gameObject);
        }

        protected virtual void OnSwipeBottom()
        {
            PanelMenuItemController.SwipeBottom(gameObject);
        }

        protected virtual float CalculateAngle(ControllerInteractionEventArgs e)
        {
            return e.touchpadAngle;
        }

        protected virtual float NormAngle(float currentDegree, float maxAngle = 360)
        {
            if (currentDegree < 0) currentDegree = currentDegree + maxAngle;
            return currentDegree % maxAngle;
        }

        protected virtual bool CheckAnglePosition(float currentDegree, float tolerance, float targetDegree)
        {
            float lowerBound = NormAngle(currentDegree - tolerance);
            float upperBound = NormAngle(currentDegree + tolerance);

            if (lowerBound > upperBound)
            {
                return targetDegree >= lowerBound || targetDegree <= upperBound;
            }
            return targetDegree >= lowerBound && targetDegree <= upperBound;
        }
    }
}
