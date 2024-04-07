// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class CONTACT_WEAPON : DAMAGING_ENTITY
    {
        // -- ATTRIBUTES

        public int
            Damage;
        public STATE
            MoveState,
            HurtState,
            State;

        // -- TYPES

        public class HURT_STATE : STATE
        {
            // -- ATTRIBUTES

            public CONTACT_WEAPON
                ContactWeapon;

            // -- CONSTRUCTORS

            public HURT_STATE(
                string name,
                CONTACT_WEAPON contact_weapon
                ) :
                base( name )
            {
                ContactWeapon = contact_weapon;
            }

            // -- OPERATIONS

            public override void Update(
                )
            {
                ContactWeapon.UpdateHurtState();
            }
        }

        // -- INQUIRIES

        public bool Moves(
            )
        {
            return State.HasState( MoveState );
        }

        // ~~

        public bool Hurts(
            )
        {
            return State.HasState( HurtState );
        }

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            Radius = 0.5f;
            RequiredFeatureMask = ( ulong )FEATURE_MASK.Hero;
            IsValidRaycastHitFunction = IsValidRaycastHit;
            Damage = 100;

            MoveState = new STATE( "Move" );
            HurtState = new HURT_STATE( "Hurt", this );

            State = new STATE( MoveState );
        }

        // ~~

        public void Initialize(
            ENTITY owner_entity
            )
        {
            OwnerEntity = owner_entity;
        }

        // ~~

        public void Move(
            )
        {
            State.ChangeState( MoveState );
        }

        // ~~

        public void Hurt(
            )
        {
            State.ChangeState( HurtState );
        }

        // ~~

        public void UpdateHurtState(
            )
        {
            RaycastHit
                raycast_hit;
            Vector3
                position_offset_vector,
                position_vector;
            DAMAGEABLE_ENTITY
                damageable_entity;

            position_vector = PositionVector;
            position_offset_vector = PositionVector - PriorPositionVector;

            if ( FindContact(
                     out raycast_hit,
                     out damageable_entity,
                     position_vector,
                     position_offset_vector
                     ) )
            {
                if ( damageable_entity != null )
                {
                    AddDamage( Damage, damageable_entity );
                }
            }
        }

        // ~~

        public void Update(
            )
        {
            BeginUpdate();
            State.Update();
            EndUpdate();
        }
    }
}
