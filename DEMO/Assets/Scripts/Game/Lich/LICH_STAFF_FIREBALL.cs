// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "LICH", "*" ) ]
    #endif

    public class LICH_STAFF_FIREBALL : DAMAGING_ENTITY
    {
        // -- CONSTANTS

        public const float
            TranslationSpeed = 50.0f;

        // -- ATTRIBUTES

        public Vector3
            TranslationVelocityVector;
        public float
            FlyTime;
        public static POOL_<LICH_STAFF_FIREBALL>
            Pool;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            Radius = 0.25f;
            RequiredFeatureMask = ( ulong )FEATURE_MASK.Hero;
            IsValidRaycastHitFunction = IsValidRaycastHit;
        }

        // ~~

        public void Initialize(
            ENTITY owner_entity,
            Vector3 forward_axis_vector
            )
        {
            OwnerEntity = owner_entity;
            TranslationVelocityVector = forward_axis_vector * TranslationSpeed;
            FlyTime = 0.0f;
        }

        // ~~

        public void Release(
            )
        {
            Pool.ReleaseEntity( this );
        }

        // ~~

        public void Update(
            )
        {
            RaycastHit
                raycast_hit;
            Vector3
                position_offset_vector,
                position_vector;
            DAMAGEABLE_ENTITY
                damageable_entity;

            BeginUpdate();

            FlyTime += TimeStep;

            position_vector = GetPositionVector();
            position_offset_vector = TranslationVelocityVector * TimeStep;

            if ( FindContact(
                     out raycast_hit,
                     out damageable_entity,
                     position_vector,
                     position_offset_vector
                     ) )
            {
                if ( damageable_entity != null )
                {
                    AddDamage( 25, damageable_entity );
                }

                Release();
            }
            else if ( FlyTime > 1.0f )
            {
                Release();
            }
            else
            {
                SetPositionVector( position_vector + position_offset_vector );
            }

            EndUpdate();
        }

        // ~~

        public static void InitializePool(
            )
        {
            if ( Pool == null )
            {
                Pool = new POOL_<LICH_STAFF_FIREBALL>( "Prefabs/LichStaffFireball", 2 );
            }
        }

        // ~~

        public static LICH_STAFF_FIREBALL Create(
            Vector3 position_vector,
            Quaternion orientation_quaternion
            )
        {
            return Pool.CreateEntity( position_vector, orientation_quaternion );
        }
    }
}
