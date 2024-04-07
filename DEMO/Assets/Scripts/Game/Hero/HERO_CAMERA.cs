// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "HERO", "*" ) ]
    #endif

    public class HERO_CAMERA : DYNAMIC_ENTITY
    {
        // -- CONSTANTS

        public const float
            MinimumXAngle = -15.0f,
            MaximumXAngle = 5.0f,
            MaximumBackwardSpeed = 5.0f,
            MinimumBackwardSpeedFactor = 0.02f,
            MaximumBackwardSpeedFactor = 5.0f,
            MinimumHeroDistance = 0.0f,
            MaximumHeroDistance = 5.0f,
            MaximumTargetDistance = 100.0f,
            MaximumHeroDistanceFactor = 0.9f;

        // -- ATTRIBUTES

        public Vector3
            PivotOffsetVector;
        public float
            XAngle,
            YAngle,
            HeroDistance,
            TargetDistance;
        public Vector3
            PivotPositionVector;

        // -- INQUIRIES

        public Vector3 GetTargetPositionVector(
            )
        {
            RaycastHit
                raycast_hit;
            Vector3
                forward_axis_vector;

            forward_axis_vector = OrientationQuaternion * Vector3.forward;

            if ( FindContact(
                     out raycast_hit,
                     PositionVector,
                     0.5f,
                     forward_axis_vector,
                     MaximumTargetDistance,
                     LEVEL.Instance.Hero.IsValidRaycastHitFunction
                     ) )
            {
                return raycast_hit.point;
            }
            else
            {
                return PositionVector + forward_axis_vector * MaximumTargetDistance;
            }
        }

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            PivotOffsetVector.Set( 0.0f, 2.0f, 0.0f );

            XAngle = 0.0f;
            YAngle = 0.0f;
            HeroDistance = MaximumHeroDistance;
            TargetDistance = MaximumTargetDistance;
        }

        // ~~

        public void UpdateOrientation(
            )
        {
            XAngle
                = Mathf.Clamp(
                      XAngle - LEVEL.Instance.Hero.Input.AimXRotationSpeed * TimeStep,
                      MinimumXAngle,
                      MaximumXAngle
                      );

            YAngle = LEVEL.Instance.Hero.GetOrientationQuaternion().eulerAngles.y;

            OrientationQuaternion = Quaternion.Euler( XAngle, YAngle, 0.0f );
        }

        // ~~

        public void UpdatePosition(
            )
        {
            float
                backward_speed,
                hero_distance;
            RaycastHit
                raycast_hit;
            Vector3
                backward_axis_vector;

            PivotPositionVector = LEVEL.Instance.Hero.GetPositionVector() + PivotOffsetVector;

            backward_axis_vector = OrientationQuaternion * Vector3.back;

            if ( FindContact(
                     out raycast_hit,
                     PivotPositionVector,
                     0.5f,
                     backward_axis_vector,
                     MaximumHeroDistance,
                     LEVEL.Instance.Hero.IsValidRaycastHitFunction
                     ) )
            {
                hero_distance = raycast_hit.distance * MaximumHeroDistanceFactor;
            }
            else
            {
                hero_distance = MaximumHeroDistance;
            }

            hero_distance
                = Mathf.Clamp(
                      hero_distance,
                      MinimumHeroDistance,
                      MaximumHeroDistance
                      );

            if ( hero_distance > HeroDistance )
            {
                backward_speed
                    = Mathf.Clamp(
                          MaximumBackwardSpeed * MaximumBackwardSpeedFactor * ( hero_distance - HeroDistance ) / MaximumHeroDistance,
                          MaximumBackwardSpeed * MinimumBackwardSpeedFactor,
                          MaximumBackwardSpeed
                          );

                HeroDistance
                    = Mathf.Clamp(
                          HeroDistance + backward_speed * TimeStep,
                          MinimumHeroDistance,
                          hero_distance
                          );
            }
            else
            {
                HeroDistance = hero_distance;
            }

            PositionVector = PivotPositionVector + backward_axis_vector * HeroDistance;
        }

        // ~~

        public void LateUpdate(
            )
        {
            BeginUpdate();
            UpdateOrientation();
            UpdatePosition();
            EndUpdate();
        }
    }
}
