// -- IMPORTS

using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public struct ENEMY_INPUT
    {
        // -- ATTRIBUTES

        public float
            WalkYRotationSpeed,
            WalkXTranslationSpeed,
            WalkZTranslationSpeed;
        public bool
            IsAttacking;
    }
}
