// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class SCALING_ACTION : ACTION
    {
        // -- ATTRIBUTES

        static List<SCALING_ACTION>
            FreeScalingActionList = new List<SCALING_ACTION>();
        public Vector3
            StartingScalingVector,
            InitialScalingVector,
            FinalScalingVector;

        // -- OPERATIONS

        public SCALING_ACTION SetInitialScalingVector(
            Vector3 initial_scaling_vector
            )
        {
            InitialScalingVector = initial_scaling_vector;

            return this;
        }

        // ~~

        public SCALING_ACTION SetFinalScalingVector(
            Vector3 final_scaling_vector
            )
        {
            FinalScalingVector = final_scaling_vector;

            return this;
        }

        // ~~

        public static SCALING_ACTION Create(
            )
        {
            SCALING_ACTION
                scaling_action;

            if ( FreeScalingActionList.Count == 0 )
            {
                scaling_action = new SCALING_ACTION();
            }
            else
            {
                scaling_action = FreeScalingActionList[ FreeScalingActionList.Count - 1 ];
                FreeScalingActionList.RemoveAt( FreeScalingActionList.Count - 1 );
            }

            scaling_action.Initialize();

            return scaling_action;
        }

        // ~~

        public override void Release(
            )
        {
            FreeScalingActionList.Add( this );
        }

        // ~~

        public override void Initialize(
            )
        {
            base.Initialize();

            StartingScalingVector = Vector3.zero;
            InitialScalingVector = Vector3.zero;
            FinalScalingVector = Vector3.zero;
        }

        // ~~

        public Vector3 GetScalingVector(
            )
        {
            if ( Transform_ != null )
            {
                return Transform_.localScale;
            }
            else
            {
                return RectTransform_.localScale;
            }
        }

        // ~~

        public void SetScalingVector(
            Vector3 scaling_vector
            )
        {
            if ( Transform_ != null )
            {
                Transform_.localScale = scaling_vector;
            }
            else
            {
                RectTransform_.localScale = scaling_vector;
            }
        }

        // ~~

        public override void Start(
            )
        {
            StartingScalingVector = GetScalingVector();
        }

        // ~~

        public override void UpdateProperty(
            )
        {
            Vector3
                interpolated_scaling_vector,
                scaling_vector;

            interpolated_scaling_vector = Vector3.Lerp( InitialScalingVector, FinalScalingVector, GetInterpolationRatio() );

            if ( IsOffset )
            {
                interpolated_scaling_vector += StartingScalingVector;
            }

            if ( !HasX
                 || !HasY
                 || !HasZ )
            {
                scaling_vector = GetScalingVector();

                if ( !HasX )
                {
                    interpolated_scaling_vector.x = scaling_vector.x;
                }

                if ( !HasY )
                {
                    interpolated_scaling_vector.y = scaling_vector.y;
                }

                if ( !HasZ )
                {
                    interpolated_scaling_vector.z = scaling_vector.z;
                }
            }

            SetScalingVector( interpolated_scaling_vector );
        }
    }
}
