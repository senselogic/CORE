// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using CORE;

// -- TYPES

namespace CORE
{
    public class VIRTUAL_INPUT
    {
        // -- ATTRIBUTES

        public VIRTUAL_HEAD_DEVICE
            HeadDevice;
        public VIRTUAL_LEFT_HAND_DEVICE
            LeftHandDevice;
        public VIRTUAL_RIGHT_HAND_DEVICE
            RightHandDevice;

        // -- CONSTRUCTORS

        public VIRTUAL_INPUT(
            float dead_zone = 0.2f,
            float treshold = 0.5f,
            float long_press_duration = 0.4f,
            float auto_press_duration = 0.4f
            )
        {
            HeadDevice = new VIRTUAL_HEAD_DEVICE();
            LeftHandDevice = new VIRTUAL_LEFT_HAND_DEVICE( dead_zone, treshold, long_press_duration, auto_press_duration );
            RightHandDevice = new VIRTUAL_RIGHT_HAND_DEVICE( dead_zone, treshold, long_press_duration, auto_press_duration );
        }

        // -- OPERATIONS

        public void Update_(
            )
        {
            HeadDevice.Update_();
            LeftHandDevice.Update_();
            RightHandDevice.Update_();
        }
    }
}
