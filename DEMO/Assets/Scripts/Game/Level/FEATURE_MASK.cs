// -- IMPORTS

using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public enum FEATURE_MASK : ulong
    {
        // -- CONSTANTS

        Owner = 1 << FEATURE_INDEX.Owner,
        Health = 1 << FEATURE_INDEX.Health,
        Alive = 1 << FEATURE_INDEX.Alive,
        Head = 1 << FEATURE_INDEX.Head,
        Chest = 1 << FEATURE_INDEX.Chest,
        Stomach = 1 << FEATURE_INDEX.Stomach,
        Limb = 1 << FEATURE_INDEX.Limb,
        Hero = 1 << FEATURE_INDEX.Hero,
        Enemy = 1 << FEATURE_INDEX.Enemy,
        Patrolling = 1 << FEATURE_INDEX.Patrolling,
        Chasing = 1 << FEATURE_INDEX.Chasing
    }
}
