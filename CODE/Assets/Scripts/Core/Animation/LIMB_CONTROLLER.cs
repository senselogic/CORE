// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class LIMB_CONTROLLER
    {
        // -- ATTRIBUTES

        public Transform
            PivotTransform,
            HingeTransform,
            EffectorTransform;
        public float
            Influence,
            PivotHingeDistance,
            HingeEffectorDistance;
        public Vector3
            EffectorTranslationVector,
            LocalSideAxisVector;
        public bool
            HasSideAxisVector,
            UpdatesSideAxisVector;

        // -- CONSTRUCTORS

        public LIMB_CONTROLLER(
            Transform pivot_transform,
            Transform hinge_transform,
            Transform effector_transform
            )
        {
            Debug.Assert( pivot_transform != null );
            Debug.Assert( hinge_transform != null );
            Debug.Assert( effector_transform != null );

            PivotTransform = pivot_transform;
            HingeTransform = hinge_transform;
            EffectorTransform = effector_transform;
            Influence = 1.0f;
            PivotHingeDistance = Vector3.Distance( PivotTransform.position, HingeTransform.position );
            HingeEffectorDistance = Vector3.Distance( HingeTransform.position, EffectorTransform.position );
        }

        // -- INQUIRIES

        public float GetAngle(
            float first_side_length,
            float second_side_length,
            float opposite_side_length
            )
        {
            float
                angle_cosinus;

            angle_cosinus
                = ( first_side_length * first_side_length
                    + second_side_length * second_side_length
                    - opposite_side_length * opposite_side_length )
                  / ( first_side_length * second_side_length * 2.0f );

            return Mathf.Acos( Mathf.Clamp( angle_cosinus, -1.0f, 1.0f ) ) * Mathf.Rad2Deg;
        }

        // ~~

        public float GetGroundOffset(
            float ground_height,
            float minimum_ground_offset,
            float maximum_ground_offset,
            uint ground_layer_mask,
            IS_VALID_RAYCAST_HIT_DELEGATE is_valid_raycast_hit_delegate
            )
        {
            RaycastHit
                raycast_hit;
            Vector3
                raycast_position_vector;

            raycast_position_vector = EffectorTransform.position;
            raycast_position_vector.y += HingeEffectorDistance * 2.0f;

            if ( ENTITY.FindContact(
                     out raycast_hit,
                     raycast_position_vector,
                     0.0f,
                     Vector3.down,
                     HingeEffectorDistance * 3.0f,
                     is_valid_raycast_hit_delegate,
                     ground_layer_mask
                     ) )
            {
                return Mathf.Clamp( raycast_hit.point.y - ground_height, minimum_ground_offset, maximum_ground_offset );
            }
            else
            {
                return minimum_ground_offset;
            }
        }

        // -- OPERATIONS

        public void Update(
            )
        {
            float
                pivot_offset_angle,
                pivot_effector_distance,
                pivot_target_distance,
                hinge_offset_angle,
                old_pivot_angle,
                old_hinge_angle,
                new_pivot_angle,
                new_hinge_angle,
                pivot_alignment_angle;
            Quaternion
                effector_orientation_quaternion,
                pivot_rotation_quaternion,
                hinge_rotation_quaternion;
            Vector3
                effector_position_vector,
                pivot_effector_axis_vector,
                pivot_target_axis_vector,
                pivot_position_vector,
                hinge_pivot_axis_vector,
                hinge_position_vector,
                hinge_effector_axis_vector,
                side_axis_vector,
                target_position_vector;

            if ( Influence > 0.0f
                 && ( EffectorTranslationVector.x != 0.0f
                      || EffectorTranslationVector.y != 0.0f
                      || EffectorTranslationVector.z != 0.0f ) )
            {
                pivot_position_vector = PivotTransform.position;
                hinge_position_vector = HingeTransform.position;
                effector_position_vector = EffectorTransform.position;
                effector_orientation_quaternion = EffectorTransform.rotation;

                target_position_vector = effector_position_vector + EffectorTranslationVector * Influence;

                pivot_target_distance = Vector3.Distance( pivot_position_vector, target_position_vector );
                pivot_target_axis_vector = ( target_position_vector - pivot_position_vector ).normalized;

                if ( pivot_target_distance > ( PivotHingeDistance + HingeEffectorDistance ) * 0.99f )
                {
                    pivot_target_distance = ( PivotHingeDistance + HingeEffectorDistance ) * 0.99f;

                    target_position_vector = pivot_position_vector + pivot_target_axis_vector * pivot_target_distance;
                }

                pivot_effector_distance = Vector3.Distance( pivot_position_vector, effector_position_vector );
                pivot_effector_axis_vector = ( effector_position_vector - pivot_position_vector ).normalized;

                if ( ( !HasSideAxisVector
                       || UpdatesSideAxisVector )
                     && pivot_effector_distance < ( PivotHingeDistance + HingeEffectorDistance ) * 0.99f )
                {
                    hinge_pivot_axis_vector = ( pivot_position_vector - hinge_position_vector ).normalized;
                    hinge_effector_axis_vector = ( effector_position_vector - hinge_position_vector ).normalized;
                    side_axis_vector = Vector3.Cross( hinge_pivot_axis_vector, hinge_effector_axis_vector );
                    LocalSideAxisVector = Quaternion.Inverse( PivotTransform.rotation ) * side_axis_vector;
                    HasSideAxisVector = true;
                }

                if ( HasSideAxisVector )
                {
                    side_axis_vector = PivotTransform.rotation * LocalSideAxisVector;

                    old_hinge_angle = GetAngle( PivotHingeDistance, HingeEffectorDistance, pivot_effector_distance );
                    new_hinge_angle = GetAngle( PivotHingeDistance, HingeEffectorDistance, pivot_target_distance );

                    hinge_offset_angle = new_hinge_angle - old_hinge_angle;

                    if ( hinge_offset_angle != 0.0f )
                    {
                        hinge_rotation_quaternion = Quaternion.AngleAxis( hinge_offset_angle, side_axis_vector );
                        HingeTransform.rotation = hinge_rotation_quaternion * HingeTransform.rotation;
                    }

                    old_pivot_angle = GetAngle( pivot_effector_distance, PivotHingeDistance, HingeEffectorDistance );
                    new_pivot_angle = GetAngle( pivot_target_distance, PivotHingeDistance, HingeEffectorDistance );

                    pivot_alignment_angle
                        = Vector3.SignedAngle( pivot_effector_axis_vector, pivot_target_axis_vector, side_axis_vector );

                    pivot_offset_angle = new_pivot_angle - old_pivot_angle;
                    pivot_offset_angle += pivot_alignment_angle;

                    if ( pivot_offset_angle != 0.0f )
                    {
                        pivot_rotation_quaternion = Quaternion.AngleAxis( pivot_offset_angle, side_axis_vector );
                        PivotTransform.rotation = pivot_rotation_quaternion * PivotTransform.rotation;
                    }

                    EffectorTransform.rotation = effector_orientation_quaternion;
                }
            }
        }
    }
}
