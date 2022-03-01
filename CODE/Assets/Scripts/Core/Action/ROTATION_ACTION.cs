// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class ROTATION_ACTION : ACTION
    {
        // -- ATTRIBUTES

        static List<ROTATION_ACTION>
            FreeRotationActionList = new List<ROTATION_ACTION>();
        public Vector3
            StartingRotationVector,
            InitialRotationVector,
            FinalRotationVector;

        // -- OPERATIONS

        public ROTATION_ACTION SetInitialRotationVector(
            Vector3 initial_rotation_vector
            )
        {
            InitialRotationVector = initial_rotation_vector;

            return this;
        }

        // ~~

        public ROTATION_ACTION SetFinalRotationVector(
            Vector3 final_rotation_vector
            )
        {
            FinalRotationVector = final_rotation_vector;

            return this;
        }

        // ~~

        public static ROTATION_ACTION Create(
            )
        {
            ROTATION_ACTION
                rotation_action;

            if ( FreeRotationActionList.Count == 0 )
            {
                rotation_action = new ROTATION_ACTION();
            }
            else
            {
                rotation_action = FreeRotationActionList[ FreeRotationActionList.Count - 1 ];
                FreeRotationActionList.RemoveAt( FreeRotationActionList.Count - 1 );
            }

            rotation_action.Initialize();

            return rotation_action;
        }

        // ~~

        public override void Release(
            )
        {
            FreeRotationActionList.Add( this );
        }

        // ~~

        public override void Initialize(
            )
        {
            base.Initialize();

            StartingRotationVector = Vector3.zero;
            InitialRotationVector = Vector3.zero;
            FinalRotationVector = Vector3.zero;
        }

        // ~~

        public Vector3 GetRotationVector(
            )
        {
            if ( Transform_ != null )
            {
                if ( IsLocal )
                {
                    return Transform_.localEulerAngles;
                }
                else
                {
                    return Transform_.rotation.eulerAngles;
                }
            }
            else
            {
                if ( IsLocal )
                {
                    return RectTransform_.localEulerAngles;
                }
                else
                {
                    return RectTransform_.rotation.eulerAngles;
                }
            }
        }

        // ~~

        public void SetRotationVector(
            Vector3 rotation_vector
            )
        {
            if ( Transform_ != null )
            {
                if ( IsLocal )
                {
                    Transform_.localEulerAngles = rotation_vector;
                }
                else
                {
                    Transform_.rotation = Quaternion.Euler( rotation_vector );
                }
            }
            else
            {
                if ( IsLocal )
                {
                    RectTransform_.localEulerAngles = rotation_vector;
                }
                else
                {
                    RectTransform_.rotation = Quaternion.Euler( rotation_vector );
                }
            }
        }

        // ~~

        public override void Start(
            )
        {
            StartingRotationVector = GetRotationVector();
        }

        // ~~

        public override void UpdateProperty(
            )
        {
            Vector3
                interpolated_rotation_vector,
                rotation_vector;

            interpolated_rotation_vector = Vector3.Lerp( InitialRotationVector, FinalRotationVector, GetInterpolationRatio() );

            if ( IsOffset )
            {
                interpolated_rotation_vector += StartingRotationVector;
            }

            if ( !HasX
                 || !HasY
                 || !HasZ )
            {
                rotation_vector = GetRotationVector();

                if ( !HasX )
                {
                    interpolated_rotation_vector.x = rotation_vector.x;
                }

                if ( !HasY )
                {
                    interpolated_rotation_vector.y = rotation_vector.y;
                }

                if ( !HasZ )
                {
                    interpolated_rotation_vector.z = rotation_vector.z;
                }
            }

            SetRotationVector( interpolated_rotation_vector );
        }
    }
}
