// -- IMPORTS

using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public enum FEATURE_INDEX : int
    {
        // -- CONSTANTS

        Owner = ENTITY.OwnerFeatureIndex,
        Health = ENTITY.HealthFeatureIndex,
        Alive = ENTITY.AliveFeatureIndex,
        Head,
        Chest,
        Stomach,
        Limb,
        Hero,
        Enemy,
        Patrolling,
        Chasing
    }
}
