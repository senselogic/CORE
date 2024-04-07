// -- IMPORTS

using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public struct HERO_INPUT
    {
        // -- ATTRIBUTES

        public float
            AimXRotationSpeed,
            WalkYRotationSpeed,
            WalkXTranslationSpeed,
            WalkZTranslationSpeed;
        public bool
            IsShooting;
    }
}
