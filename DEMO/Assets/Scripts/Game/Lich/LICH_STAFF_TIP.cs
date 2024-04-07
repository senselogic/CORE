// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "LICH", "*" ) ]
    #endif

    public class LICH_STAFF_TIP : TEMPORAL_ENTITY
    {
        // -- CONSTANTS

        public const float
            MinimumShootDelay = 2.0f;

        // -- ATTRIBUTES

        public ENTITY
            OwnerEntity;
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

        public void ShootFireball(
            )
        {
            Quaternion
                fireball_orientation_quaternion;
            Vector3
                fireball_forward_axis_vector,
                fireball_position_vector,
                fireball_target_position_vector;
            LICH_STAFF_FIREBALL
                lich_staff_fireball;

            fireball_position_vector = GetPositionVector();
            fireball_orientation_quaternion = GetOrientationQuaternion();

            fireball_target_position_vector
                = LEVEL.Instance.Hero.GetPositionVector()
                  + new Vector3( Random.Range( -1.0f, 1.0f ), Random.Range( 0.0f, 1.0f ), Random.Range( -1.0f, 1.0f ) );

            fireball_forward_axis_vector = ( fireball_target_position_vector - fireball_position_vector ).normalized;

            lich_staff_fireball = LICH_STAFF_FIREBALL.Create( fireball_position_vector, fireball_orientation_quaternion );
            lich_staff_fireball.Initialize( OwnerEntity, fireball_forward_axis_vector );

            Shoots = false;

            SOUND.Create( GetPositionVector(), ShootAudioClip, SOUND_MASK.Disposable, 0, 0.3f, Random.Range( 1.0f, 1.5f ) );
        }

        // ~~

        public void LateUpdate(
            )
        {
            BeginUpdate();

            if ( Shoots )
            {
                ShootFireball();
            }

            EndUpdate();
        }
    }
}
