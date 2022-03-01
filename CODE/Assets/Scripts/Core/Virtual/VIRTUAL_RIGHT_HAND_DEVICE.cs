// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using CORE;

// -- TYPES

namespace CORE
{
    public class VIRTUAL_RIGHT_HAND_DEVICE : VIRTUAL_HAND_DEVICE
    {
        // -- CONSTRUCTORS

        public VIRTUAL_RIGHT_HAND_DEVICE(
            float dead_zone = 0.2f,
            float treshold = 0.5f,
            float long_press_duration = 0.4f,
            float auto_press_duration = 0.4f
            ) :
            base(
                UnityEngine.XR.InputDeviceCharacteristics.Controller
                | UnityEngine.XR.InputDeviceCharacteristics.TrackedDevice
                | UnityEngine.XR.InputDeviceCharacteristics.Right,
                dead_zone,
                treshold,
                long_press_duration,
                auto_press_duration
                )
        {
        }
    }
}
