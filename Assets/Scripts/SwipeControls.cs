using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeControls : MonoBehaviour
{
    #region Instance
    private static SwipeControls instance;
    public static SwipeControls Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Object.FindFirstObjectByType<SwipeControls>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned SwipeControls", typeof(SwipeControls)).GetComponent<SwipeControls>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    private float deadzone = 5f;

    public bool swipeleft, swiperight;
    private Vector2 swipedelta, starttouch;
    private float lasttap;
    private float sqrdeadzone;

    #region public properties
    public Vector2 Swipedelta { get { return swipedelta; } }
    public bool Swipeleft { get { return swipeleft; } }
    public bool Swiperight { get { return swiperight; } }
    #endregion

    private void Start()
    {
        sqrdeadzone = deadzone * deadzone;
    }

    public void LateUpdate()
    {
        swipeleft = swiperight = false;

        UpdateMobile();
        UpdateKeyboard();
    }

    public void UpdateKeyboard()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.leftArrowKey.wasPressedThisFrame)
            {
                swipeleft = true;
            }
            if (keyboard.rightArrowKey.wasPressedThisFrame)
            {
                swiperight = true;
            }
        }
    }

    public void UpdateMobile()
    {
        Touchscreen touchscreen = Touchscreen.current;

        if (touchscreen == null)
            return;

        if (touchscreen.touches.Count > 0)
        {
            var touch = touchscreen.touches[0];
            var phase = (int)touch.phase.ReadValue();

            if (phase == 0) // TouchPhase.Began
            {
                starttouch = touch.position.ReadValue();               
                Debug.Log(Time.time - lasttap);
                lasttap = Time.time;
            }
            else if (phase == 3 || phase == 4) // TouchPhase.Ended || TouchPhase.Canceled
            {
                starttouch = swipedelta = Vector2.zero;
            }

            if (touchscreen.touches.Count > 0)
            {
                swipedelta = touchscreen.touches[0].position.ReadValue() - starttouch;
            }
        }

        swipedelta = Vector2.zero;

        if(starttouch != Vector2.zero && touchscreen.touches.Count > 0)
        {
            swipedelta = touchscreen.touches[0].position.ReadValue() - starttouch;
        }

        if(swipedelta.sqrMagnitude > sqrdeadzone)
        {
            float x = swipedelta.x;
            float y = swipedelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeleft = true;
                }
                else
                {
                    swiperight = true;
                }
            }           
            starttouch = swipedelta = Vector2.zero;
        }
    }

}
