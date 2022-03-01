// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public struct VIRTUAL_DEVICE_BUTTON
    {
        // -- ATTRIBUTES

        public float
            DeadZone,
            Treshold,
            Value,
            LongPressDuration,
            LongPressTime,
            AutoPressDuration,
            AutoPressTime;
        public int
            AutoPressIndex;
        public bool
            WasPressed,
            WasLongPressed,
            WasAutoPressed,
            IsPressed,
            IsJustPressed,
            IsLongPressed,
            IsJustLongPressed,
            IsAutoPressed,
            IsJustAutoPressed,
            IsJustKeyPressed,
            IsJustReleased,
            IsJustShortReleased,
            IsJustLongReleased;

        // -- INQUIRIES

        // -- OPERATIONS

        public void Initialize(
            float dead_zone,
            float treshold,
            float long_press_duration,
            float auto_press_duration
            )
        {
            DeadZone = dead_zone;
            Treshold = treshold;
            Value = 0.0f;
            LongPressDuration = long_press_duration;
            LongPressTime = 0.0f;
            AutoPressDuration = auto_press_duration;
            AutoPressTime = 0.0f;
            AutoPressIndex = 0;
            WasPressed = false;
            WasLongPressed = false;
            WasAutoPressed = false;
            IsPressed = false;
            IsJustPressed = false;
            IsLongPressed = false;
            IsJustLongPressed = false;
            IsAutoPressed = false;
            IsJustAutoPressed = false;
            IsJustKeyPressed = false;
            IsJustReleased = false;
            IsJustShortReleased = false;
            IsJustLongReleased = false;
        }

        // ~~

        public void Update_(
            )
        {
            if ( IsPressed )
            {
                LongPressTime += Time.deltaTime;
                AutoPressTime += Time.deltaTime;
            }

            IsJustPressed = IsPressed && !WasPressed;
            IsLongPressed = IsPressed && LongPressTime >= LongPressDuration;
            IsJustLongPressed = IsLongPressed && !WasLongPressed;
            IsAutoPressed = IsPressed && AutoPressTime >= AutoPressDuration;
            IsJustAutoPressed = IsAutoPressed && !WasAutoPressed;
            IsJustKeyPressed = IsJustPressed || IsJustAutoPressed;
            IsJustReleased = !IsPressed && WasPressed;
            IsJustShortReleased = IsJustReleased && LongPressTime < LongPressDuration;
            IsJustLongReleased = IsJustReleased && LongPressTime >= LongPressDuration;

            if ( !IsPressed )
            {
                LongPressTime = 0.0f;
                AutoPressTime = 0.0f;
            }
            else if ( IsJustAutoPressed )
            {
                AutoPressTime = 0.0f;
                ++AutoPressIndex;
            }

            WasPressed = IsPressed;
            WasLongPressed = IsLongPressed;
            WasAutoPressed = IsAutoPressed;
        }

        // ~~

        public void Update_(
            float button_value
            )
        {
            if ( button_value < -DeadZone
                 || button_value > DeadZone )
            {
                Value = button_value;
            }
            else
            {
                Value = 0.0f;
            }

            IsPressed = ( Value < -Treshold || Value > Treshold );

            Update_();
        }

        // ~~

        public void Update_(
            bool button_is_pressed
            )
        {
            if ( button_is_pressed )
            {
                Value = 1.0f;
            }
            else
            {
                Value = 0.0f;
            }

            IsPressed = button_is_pressed;

            Update_();
        }
    }
}
