// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class LEGS_CONTROLLER
    {
        // -- ATTRIBUTES

        public Transform
            GroundTransform,
            PelvisTransform;
        public LIMB_CONTROLLER
            LeftLimbController,
            RightLimbController;
        public uint
            GroundLayerMask;
        public IS_VALID_RAYCAST_HIT_DELEGATE
            IsValidRaycastHitFunction;
        public bool
            UpdatesGroundOffset;
        public float
            MinimumGroundOffset,
            MaximumGroundOffset,
            LeftGroundOffset,
            RightGroundOffset,
            PelvisOffset;

        // -- CONSTRUCTORS

        public LEGS_CONTROLLER(
            Transform ground_transform,
            Transform pelvis_transform,
            Transform left_hip_transform,
            Transform left_knee_transform,
            Transform left_ankle_transform,
            Transform right_hip_transform,
            Transform right_knee_transform,
            Transform right_ankle_transform,
            uint ground_layer_mask,
            IS_VALID_RAYCAST_HIT_DELEGATE is_valid_raycast_hit_delegate
            )
        {
            Debug.Assert( ground_transform != null );
            Debug.Assert( pelvis_transform != null );
            Debug.Assert( left_hip_transform != null );
            Debug.Assert( left_knee_transform != null );
            Debug.Assert( left_ankle_transform != null );
            Debug.Assert( right_hip_transform != null );
            Debug.Assert( right_knee_transform != null );
            Debug.Assert( right_ankle_transform != null );
            Debug.Assert( ground_layer_mask != 0 );

            GroundTransform = ground_transform;
            PelvisTransform = pelvis_transform;
            LeftLimbController = new LIMB_CONTROLLER( left_hip_transform, left_knee_transform, left_ankle_transform );
            RightLimbController = new LIMB_CONTROLLER( right_hip_transform, right_knee_transform, right_ankle_transform );
            GroundLayerMask = ground_layer_mask;
            IsValidRaycastHitFunction = is_valid_raycast_hit_delegate;
            UpdatesGroundOffset = true;
            MinimumGroundOffset = LeftLimbController.HingeEffectorDistance * -0.5f;
            MaximumGroundOffset = LeftLimbController.HingeEffectorDistance * 0.5f;
        }

        // -- OPERATIONS

        public void Update(
            float time_step
            )
        {
            float
                ground_height;
            Vector3
                pelvis_position_vector;

            if ( UpdatesGroundOffset )
            {
                ground_height = GroundTransform.position.y;

                LeftGroundOffset = LeftLimbController.GetGroundOffset( ground_height, MinimumGroundOffset, MaximumGroundOffset, GroundLayerMask, IsValidRaycastHitFunction );
                RightGroundOffset = RightLimbController.GetGroundOffset( ground_height, MinimumGroundOffset, MaximumGroundOffset, GroundLayerMask, IsValidRaycastHitFunction );
                PelvisOffset = Mathf.Lerp( PelvisOffset, Mathf.Min( LeftGroundOffset, RightGroundOffset ), time_step * 4.0f );

                LeftLimbController.EffectorTranslationVector.y = LeftGroundOffset - PelvisOffset;
                RightLimbController.EffectorTranslationVector.y = RightGroundOffset - PelvisOffset;
            }

            if ( PelvisOffset != 0.0f )
            {
                pelvis_position_vector = PelvisTransform.position;
                pelvis_position_vector.y += PelvisOffset;

                PelvisTransform.position = pelvis_position_vector;
            }

            LeftLimbController.Update();
            RightLimbController.Update();
        }
    }
}
