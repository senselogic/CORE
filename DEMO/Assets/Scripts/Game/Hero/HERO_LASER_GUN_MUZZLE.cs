// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "HERO HERO_CAMERA", "*" ) ]
    #endif

    public class HERO_LASER_GUN_MUZZLE : TEMPORAL_ENTITY
    {
        // -- CONSTANTS

        public const float
            MinimumShootDelay = 0.2f;

        // -- ATTRIBUTES

        public ENTITY
            OwnerEntity;
        public float
            ShootDelay;
        public bool
            Shoots;
        public AudioClip
            ShootAudioClip;

        // -- OPERATIONS

        public void Initialize(
            ENTITY owner_entity
            )
        {
            OwnerEntity = owner_entity;
        }

        // ~~

        public void Shoot(
            )
        {
            Shoots = true;
        }

        // ~~

        public void ShootRay(
            )
        {
            Quaternion
                laser_ray_orientation_quaternion;
            Vector3
                camera_target_position_vector,
                laser_ray_forward_axis_vector,
                laser_ray_position_vector;
            HERO_LASER_GUN_RAY
                hero_laser_gun_ray;

            camera_target_position_vector = LEVEL.Instance.HeroCamera.GetTargetPositionVector();
            laser_ray_position_vector = GetPositionVector();
            laser_ray_position_vector.y += LEVEL.Instance.Hero.LegsController.PelvisOffset;
            laser_ray_forward_axis_vector = ( camera_target_position_vector - laser_ray_position_vector ).normalized;
            laser_ray_orientation_quaternion = Quaternion.LookRotation( laser_ray_forward_axis_vector );

            hero_laser_gun_ray = HERO_LASER_GUN_RAY.Create( laser_ray_position_vector, laser_ray_orientation_quaternion );
            hero_laser_gun_ray.Initialize( OwnerEntity, laser_ray_forward_axis_vector );

            ShootDelay = MinimumShootDelay;

            Shoots = false;

            SOUND.Create( GetPositionVector(), ShootAudioClip, SOUND_MASK.Disposable, 0, 0.3f, Random.Range( 1.0f, 1.5f ) );
        }

        // ~~

        public void LateUpdate(
            )
        {
            BeginUpdate();
            ShootDelay -= TimeStep;

            if ( Shoots
                 && ShootDelay <= 0.0f )
            {
                ShootRay();
            }

            EndUpdate();
        }
    }
}
