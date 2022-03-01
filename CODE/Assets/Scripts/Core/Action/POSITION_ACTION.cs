// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class POSITION_ACTION : ACTION
    {
        // -- ATTRIBUTES

        static List<POSITION_ACTION>
            FreePositionActionList = new List<POSITION_ACTION>();
        public Vector3
            StartingPositionVector,
            InitialPositionVector,
            FinalPositionVector;

        // -- OPERATIONS

        public POSITION_ACTION SetInitialPositionVector(
            Vector3 initial_position_vector
            )
        {
            InitialPositionVector = initial_position_vector;

            return this;
        }

        // ~~

        public POSITION_ACTION SetFinalPositionVector(
            Vector3 final_position_vector
            )
        {
            FinalPositionVector = final_position_vector;

            return this;
        }

        // ~~

        public static POSITION_ACTION Create(
            )
        {
            POSITION_ACTION
                position_action;

            if ( FreePositionActionList.Count == 0 )
            {
                position_action = new POSITION_ACTION();
            }
            else
            {
                position_action = FreePositionActionList[ FreePositionActionList.Count - 1 ];
                FreePositionActionList.RemoveAt( FreePositionActionList.Count - 1 );
            }

            position_action.Initialize();

            return position_action;
        }

        // ~~

        public override void Release(
            )
        {
            FreePositionActionList.Add( this );
        }

        // ~~

        public override void Initialize(
            )
        {
            base.Initialize();

            StartingPositionVector = Vector3.zero;
            InitialPositionVector = Vector3.zero;
            FinalPositionVector = Vector3.zero;
        }

        // ~~

        public Vector3 GetPositionVector(
            )
        {
            if ( Transform_ != null )
            {
                if ( IsLocal )
                {
                    return Transform_.localPosition;
                }
                else
                {
                    return Transform_.position;
                }
            }
            else
            {
                if ( IsLocal )
                {
                    return RectTransform_.localPosition;
                }
                else
                {
                    return RectTransform_.position;
                }
            }
        }

        // ~~

        public void SetPositionVector(
            Vector3 position_vector
            )
        {
            if ( Transform_ != null )
            {
                if ( IsLocal )
                {
                    Transform_.localPosition = position_vector;
                }
                else
                {
                    Transform_.position = position_vector;
                }
            }
            else
            {
                if ( IsLocal )
                {
                    RectTransform_.localPosition = position_vector;
                }
                else
                {
                    RectTransform_.position = position_vector;
                }
            }
        }

        // ~~

        public override void Start(
            )
        {
            StartingPositionVector = GetPositionVector();
        }

        // ~~

        public override void UpdateProperty(
            )
        {
            Vector3
                interpolated_position_vector,
                position_vector;

            interpolated_position_vector = Vector3.Lerp( InitialPositionVector, FinalPositionVector, GetInterpolationRatio() );

            if ( IsOffset )
            {
                interpolated_position_vector += StartingPositionVector;
            }

            if ( !HasX
                 || !HasY
                 || !HasZ )
            {
                position_vector = GetPositionVector();

                if ( !HasX )
                {
                    interpolated_position_vector.x = position_vector.x;
                }

                if ( !HasY )
                {
                    interpolated_position_vector.y = position_vector.y;
                }

                if ( !HasZ )
                {
                    interpolated_position_vector.z = position_vector.z;
                }
            }

            SetPositionVector( interpolated_position_vector );
        }
    }
}
