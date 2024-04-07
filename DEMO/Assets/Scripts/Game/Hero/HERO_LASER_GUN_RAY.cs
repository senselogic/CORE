// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class HERO_LASER_GUN_RAY : DAMAGING_ENTITY
    {
        // -- CONSTANTS

        public const float
            TranslationSpeed = 50.0f;

        // -- ATTRIBUTES

        public Vector3
            TranslationVelocityVector;
        public float
            FlyTime;
        public static POOL_<HERO_LASER_GUN_RAY>
            Pool;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            Radius = 0.1f;
            RequiredFeatureMask = ( ulong )FEATURE_MASK.Enemy;
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
            Quaternion
                hit_orientation_quaternion;
            RaycastHit
                raycast_hit;
            Vector3
                hit_position_vector,
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
                hit_position_vector = raycast_hit.point;
                hit_orientation_quaternion = Quaternion.LookRotation( GetOrientationQuaternion() * Vector3.back );

                if ( damageable_entity != null )
                {
                    AddDamage( 10, damageable_entity );

                    BLOOD_SPRAY.Create( hit_position_vector, hit_orientation_quaternion );
                }
                else
                {
                    ENERGY_SPRAY.Create( hit_position_vector, hit_orientation_quaternion );
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
                Pool = new POOL_<HERO_LASER_GUN_RAY>( "Prefabs/HeroLaserGunRay", 8 );
            }
        }

        // ~~

        public static HERO_LASER_GUN_RAY Create(
            Vector3 position_vector,
            Quaternion orientation_quaternion
            )
        {
            return Pool.CreateEntity( position_vector, orientation_quaternion );
        }
    }
}
