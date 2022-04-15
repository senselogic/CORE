// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class FALLING_ENTITY : DYNAMIC_ENTITY
    {
        // -- ATTRIBUTES

        public Vector3
            TranslationVelocityVector,
            LocalTranslationVelocityVector,
            GravityVelocityVector,
            GravityAccelerationVector;
        public uint
            GroundLayerMask;
        public float
            GroundSphereRadius;
        public Vector3
            GroundSphereOffsetVector;
        public IS_VALID_COLLIDER_DELEGATE
            IsValidColliderDelegate;
        public bool
            IsGrounded;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            GravityAccelerationVector.y = -10.0f;
        }

        // ~~

        public new void BeginUpdate(
            )
        {
            base.BeginUpdate();

            if ( TimeStep > 0.0f )
            {
                TranslationVelocityVector = ( OldPositionVector - PriorPositionVector ) / TimeStep;
            }

            LocalTranslationVelocityVector
                = Vector3.Lerp(
                      LocalTranslationVelocityVector,
                      Quaternion.Inverse( OldOrientationQuaternion ) * TranslationVelocityVector,
                      TimeStep * 8.0f
                      );

            if ( GroundSphereRadius > 0.0f
                 && GroundLayerMask != 0 )
            {
                IsGrounded
                    = HasContact(
                          OldPositionVector + GroundSphereOffsetVector,
                          GroundSphereRadius,
                          IsValidColliderDelegate,
                          GroundLayerMask
                          );
            }
        }

        // ~~

        public new void EndUpdate(
            )
        {
            GravityVelocityVector += GravityAccelerationVector * TimeStep;

            if ( IsGrounded )
            {
                GravityVelocityVector.y = 0.0f;
            }

            PositionVector += GravityVelocityVector * TimeStep;

            base.EndUpdate();
        }
    }
}
