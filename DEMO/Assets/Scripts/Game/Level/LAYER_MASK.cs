// -- IMPORTS

using GAME_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public enum LAYER_MASK : uint
    {
        // -- CONSTANTS

        Water = 1 << LAYER_INDEX.Water,
        Trigger = 1 << LAYER_INDEX.Trigger,
        Ground = ~( uint )( Water | Trigger )
    }
}
