// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using CORE;

// -- TYPES

namespace CORE
{
    public class VIRTUAL_DEVICE
    {
        // -- ATTRIBUTES

        public UnityEngine.XR.InputDeviceCharacteristics
            InputDeviceCharacteristics_;
        public UnityEngine.XR.InputDevice
            InputDevice_;
        public string
            Name;
        public long
            FrameIndex;
        public bool
            IsConnected,
            IsTracked,
            IsUpdated;
        public Vector3
            PositionVector;
        public Quaternion
            RotationQuaternion;

        // -- CONSTRUCTORS

        public VIRTUAL_DEVICE(
            UnityEngine.XR.InputDeviceCharacteristics input_device_characteristics
            )
        {
            InputDeviceCharacteristics_ = input_device_characteristics;
        }

        // -- INQUIRIES

        public void Log(
            )
        {
            Debug.Log( "Name : " + Name );
            Debug.Log( "IsConnected : " + IsConnected );
            Debug.Log( "IsUpdated : " + IsUpdated );
            Debug.Log( "IsTracked : " + IsTracked );
            Debug.Log( "PositionVector : " + PositionVector );
            Debug.Log( "RotationQuaternion : " + RotationQuaternion );
        }

        // ~~

        public void SetInputDevice(
            UnityEngine.XR.InputDevice input_device
            )
        {
            InputDevice_ = input_device;
            Name = input_device.name;
        }

        // ~~

        public bool Connect(
            )
        {
            List<UnityEngine.XR.InputDevice>
                input_device_list;

            input_device_list = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics( InputDeviceCharacteristics_, input_device_list );

            if ( input_device_list.Count > 0 )
            {
                SetInputDevice( input_device_list[ 0 ] );

                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }

            return IsConnected;
        }

        // ~~

        public void Update_(
            )
        {
            if ( !IsConnected )
            {
                if ( FrameIndex == 0 )
                {
                    Connect();
                }

                FrameIndex = ( FrameIndex + 1 ) & 63;
            }

            if ( IsConnected )
            {
                InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.isTracked, out IsTracked );
                InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.devicePosition, out PositionVector );
                InputDevice_.TryGetFeatureValue( UnityEngine.XR.CommonUsages.deviceRotation, out RotationQuaternion );

                IsUpdated = true;
            }
            else
            {
                IsUpdated = false;
            }
        }
    }
}
