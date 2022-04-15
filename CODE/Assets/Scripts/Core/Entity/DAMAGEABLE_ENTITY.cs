// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class DAMAGEABLE_ENTITY : ENTITY
    {
        // -- ATTRIBUTES

        public ENTITY
            OwnerEntity;

        // -- INQUIRIES

        public override ENTITY GetOwnerEntity(
            )
        {
            return OwnerEntity;
        }

        // -- OPERATIONS

        public void OnEnable(
            )
        {
            OwnerEntity
                = FindSuperEntityByFeatureMask<ENTITY>(
                      transform,
                      1 << OwnerFeatureIndex
                      | 1 << HealthFeatureIndex
                      );
        }
    }
}
