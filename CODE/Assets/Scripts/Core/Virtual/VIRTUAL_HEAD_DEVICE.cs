// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using CORE;

// -- TYPES

namespace CORE
{
    public class VIRTUAL_HEAD_DEVICE : VIRTUAL_DEVICE
    {
        // -- CONSTRUCTORS

        public VIRTUAL_HEAD_DEVICE(
            ) :
            base(
                UnityEngine.XR.InputDeviceCharacteristics.TrackedDevice
                | UnityEngine.XR.InputDeviceCharacteristics.HeadMounted
                )
        {
        }
    }
}
