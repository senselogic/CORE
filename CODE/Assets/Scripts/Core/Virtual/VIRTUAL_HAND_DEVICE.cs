// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using CORE;

// -- TYPES

namespace CORE
{
    public class VIRTUAL_HAND_DEVICE : VIRTUAL_DEVICE
    {
        // -- ATTRIBUTES

        public bool
            HasGripButton,
            HasTriggerButton,
            HasPrimaryButton,
            HasPrimaryAxisButton,
            HasPrimaryAxisVector,
            HasSecondaryButton,
            HasSecondaryAxisButton,
            HasSecondaryAxisVector;
        public VIRTUAL_DEVICE_BUTTON
            GripButton,
            TriggerButton,
            PrimaryButton,
            PrimaryAxisButton,
            PrimaryAxisLeftButton,
            PrimaryAxisRightButton,
            PrimaryAxisDownButton,
            PrimaryAxisUpButton,
            SecondaryButton,
            SecondaryAxisButton,
            SecondaryAxisLeftButton,
            SecondaryAxisRightButton,
            SecondaryAxisDownButton,
            SecondaryAxisUpButton;
        public Vector2
            SecondaryAxisVector,
            PrimaryAxisVector;

        // -- CONSTRUCTORS

        public VIRTUAL_HAND_DEVICE(
            UnityEngine.XR.InputDeviceCharacteristics input_device_characteristics,
            float dead_zone = 0.2f,
            float treshold = 0.5f,
            float long_press_duration = 0.4f,
            float auto_press_duration = 0.4f
            ) :
            base( input_device_characteristics )
        {
            GripButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            TriggerButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            PrimaryButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            PrimaryAxisButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            PrimaryAxisLeftButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            PrimaryAxisRightButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            PrimaryAxisDownButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            PrimaryAxisUpButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            SecondaryButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            SecondaryAxisButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            SecondaryAxisLeftButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            SecondaryAxisRightButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            SecondaryAxisDownButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
            SecondaryAxisUpButton.Initialize( dead_zone, treshold, long_press_duration, auto_press_duration );
        }

        // -- INQUIRIES

        public new void Log(
            )
        {
            base.Log();

            Debug.Log( "GripButtonIsPressed : " + GripButton.IsPressed );
            Debug.Log( "TriggerButtonIsPressed : " + TriggerButton.IsPressed );
            Debug.Log( "PrimaryButtonIsPressed : " + PrimaryButton.IsPressed );
            Debug.Log( "PrimaryAxisButtonIsPressed : " + PrimaryAxisButton.IsPressed );
            Debug.Log( "PrimaryAxisVector : " + PrimaryAxisVector );
            Debug.Log( "SecondaryButtonIsPressed : " + SecondaryButton.IsPressed );
            Debug.Log( "SecondaryAxisButtonIsPressed : " + SecondaryAxisButton.IsPressed );
            Debug.Log( "SecondaryAxisVector : " + SecondaryAxisVector );
        }

        // ~~

        public new void Update_(
            )
        {
            bool
                primary_button_is_pressed,
                secondary_button_is_pressed,
                grip_button_is_pressed,
                primary_axis_button_is_pressed,
                secondary_axis_button_is_pressed,
                trigger_button_is_pressed;

            base.Update_();

            if ( IsUpdated )
            {
                HasGripButton = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.gripButton, out grip_button_is_pressed );
                HasTriggerButton = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.triggerButton, out trigger_button_is_pressed );
                HasPrimaryButton = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.primaryButton, out primary_button_is_pressed );
                HasPrimaryAxisButton = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary_axis_button_is_pressed );
                HasPrimaryAxisVector = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.primary2DAxis, out PrimaryAxisVector );
                HasSecondaryButton = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.secondaryButton, out secondary_button_is_pressed );
                HasSecondaryAxisButton = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.secondary2DAxisClick, out secondary_axis_button_is_pressed );
                HasSecondaryAxisVector = InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.secondary2DAxis, out SecondaryAxisVector );

                if ( PrimaryAxisVector.x >= -PrimaryAxisLeftButton.DeadZone
                     && PrimaryAxisVector.x <= PrimaryAxisRightButton.DeadZone )
                {
                    PrimaryAxisVector.x = 0.0f;
                }

                if ( PrimaryAxisVector.y >= -PrimaryAxisDownButton.DeadZone
                     && PrimaryAxisVector.y <= PrimaryAxisUpButton.DeadZone )
                {
                    PrimaryAxisVector.y = 0.0f;
                }

                if ( SecondaryAxisVector.x >= -SecondaryAxisLeftButton.DeadZone
                     && SecondaryAxisVector.x <= SecondaryAxisRightButton.DeadZone )
                {
                    SecondaryAxisVector.x = 0.0f;
                }

                if ( SecondaryAxisVector.y >= -SecondaryAxisDownButton.DeadZone
                     && SecondaryAxisVector.y <= SecondaryAxisUpButton.DeadZone )
                {
                    SecondaryAxisVector.y = 0.0f;
                }

                GripButton.Update_( grip_button_is_pressed );
                TriggerButton.Update_( trigger_button_is_pressed );

                PrimaryButton.Update_( primary_button_is_pressed );
                PrimaryAxisButton.Update_( primary_axis_button_is_pressed );
                PrimaryAxisLeftButton.Update_( PrimaryAxisVector.x < -PrimaryAxisLeftButton.Treshold );
                PrimaryAxisRightButton.Update_( PrimaryAxisVector.x > PrimaryAxisRightButton.Treshold );
                PrimaryAxisDownButton.Update_( PrimaryAxisVector.y < -PrimaryAxisDownButton.Treshold );
                PrimaryAxisUpButton.Update_( PrimaryAxisVector.y > PrimaryAxisUpButton.Treshold );

                SecondaryButton.Update_( secondary_button_is_pressed );
                SecondaryAxisButton.Update_( secondary_axis_button_is_pressed );
                SecondaryAxisLeftButton.Update_( SecondaryAxisVector.x < -SecondaryAxisLeftButton.Treshold );
                SecondaryAxisRightButton.Update_( SecondaryAxisVector.x > SecondaryAxisRightButton.Treshold );
                SecondaryAxisDownButton.Update_( SecondaryAxisVector.y < -SecondaryAxisDownButton.Treshold );
                SecondaryAxisUpButton.Update_( SecondaryAxisVector.y > SecondaryAxisUpButton.Treshold );
            }
        }
    }
}
