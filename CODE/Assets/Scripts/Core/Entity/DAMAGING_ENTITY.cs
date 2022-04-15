// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class DAMAGING_ENTITY : DYNAMIC_ENTITY
    {
        // -- ATTRIBUTES

        public ENTITY
            OwnerEntity;
        public float
            Radius;
        public ulong
            RequiredFeatureMask,
            AllowedFeatureMask,
            ForbiddenFeatureMask;
        public IS_VALID_RAYCAST_HIT_DELEGATE
            IsValidRaycastHitDelegate;

        // -- INQUIRIES

        public override ENTITY GetOwnerEntity(
            )
        {
            return OwnerEntity;
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
                || damageable_entity.GetOwnerEntity() != OwnerEntity;
        }

        // ~~

        public bool FindContact(
            out RaycastHit raycast_hit,
            out DAMAGEABLE_ENTITY damageable_entity,
            Vector3 position_vector,
            Vector3 position_offset_vector
            )
        {
            if ( FindContact(
                     out raycast_hit,
                     position_vector,
                     Radius,
                     position_offset_vector.normalized,
                     position_offset_vector.magnitude,
                     IsValidRaycastHitDelegate
                     ) )
            {
                damageable_entity = raycast_hit.transform.gameObject.GetComponent<DAMAGEABLE_ENTITY>();

                return true;
            }
            else
            {
                damageable_entity = null;

                return false;
            }
        }

        // -- OPERATIONS

        public bool AddDamage(
            int damage,
            DAMAGEABLE_ENTITY damageable_entity
            )
        {
            if ( damageable_entity.OwnerEntity.HasFeatureMask( RequiredFeatureMask, AllowedFeatureMask, ForbiddenFeatureMask ) )
            {
                damageable_entity.OwnerEntity.AddDamage( damage );

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
