// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class CHARACTER : CHARACTER_ENTITY
    {
        // -- ATTRIBUTES

        public float
            MaximumAimXRotationSpeed,
            MaximumWalkYRotationSpeed,
            MaximumWalkXTranslationSpeed,
            MaximumWalkZTranslationSpeed,
            WalkLeftAnimationSpeed,
            WalkRightAnimationSpeed,
            WalkBackwardAnimationSpeed,
            WalkForwardAnimationSpeed,
            RunForwardAnimationSpeed;
        public float
            MaximumHealth,
            Health,
            HealingPerSecond,
            MinimumHealingDelay,
            HealingDelay,
            EmergedHeight;
        public bool
            IsFrozen;
        public IS_VALID_RAYCAST_HIT_FUNCTION
            IsValidRaycastHitFunction;
        public GRID_REFERENCE
            GridReference;

        // -- INQUIRIES

        public bool IsValidCollider(
            Collider collider
            )
        {
            DAMAGEABLE_ENTITY
                damageable_entity;

            damageable_entity = collider.gameObject.GetComponent<DAMAGEABLE_ENTITY>();

            return
                damageable_entity == null
                || damageable_entity.GetOwnerEntity() != GetOwnerEntity();
        }

        // ~~

        public bool IsValidRaycastHit(
            ref RaycastHit raycast_hit
            )
        {
            DAMAGEABLE_ENTITY
                damageable_entity;

            damageable_entity = raycast_hit.transform.gameObject.GetComponent<DAMAGEABLE_ENTITY>();

            return
                damageable_entity == null
                || damageable_entity.GetOwnerEntity() != GetOwnerEntity();
        }

        // ~~

        public bool IsAlive(
            )
        {
            return Health > 0.0f;
        }

        // ~~

        public override float GetHealth(
            )
        {
            return Health;
        }

        // ~~

        public bool HasRunAnimation(
            )
        {
            return RunForwardAnimationSpeed > 0.0f;
        }

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            AddFeatureMask( ( ulong )( FEATURE_MASK.Owner | FEATURE_MASK.Health | FEATURE_MASK.Alive ) );

            GroundLayerMask = ( uint )LAYER_MASK.Ground;
            GroundSphereRadius = 0.02f;
            IsValidColliderFunction = IsValidCollider;
            IsValidRaycastHitFunction = IsValidRaycastHit;

            Health = MaximumHealth;
            EmergedHeight = 4.0f;

            IsFrozen = true;

            GridReference = LEVEL.Instance.CharacterGrid.CreateReference( this );
        }

        // ~~

        public void OnDisable(
            )
        {
            GridReference.Remove();
            GridReference = null;
        }

        // ~~

        public virtual void Die(
            )
        {
            RemoveFeatureMask( ( ulong )FEATURE_MASK.Alive );
        }

        // ~~

        public override bool AddDamage(
            int damage
            )
        {
            HealingDelay = 0.0f;

            if ( Health > 0.0f )
            {
                Health = Mathf.Max( Health - damage, 0.0f );

                if ( Health == 0.0f )
                {
                    Die();
                }
            }

            return true;
        }

        // ~~

        public void Heal(
            )
        {
            HealingDelay += TimeStep;

            if ( HealingDelay > MinimumHealingDelay )
            {
                while ( HealingDelay > MinimumHealingDelay )
                {
                    if ( Health < MaximumHealth )
                    {
                        Health += ( HealingDelay - MinimumHealingDelay ) * HealingPerSecond;

                        if ( Health > MaximumHealth )
                        {
                            Health = MaximumHealth;
                        }
                    }

                    HealingDelay = MinimumHealingDelay;
                }
            }
        }

        // ~~

        public new void EndUpdate(
            )
        {
            base.EndUpdate();

            GridReference.Update();
        }
    }
}
