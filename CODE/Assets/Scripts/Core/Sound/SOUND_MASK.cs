// -- IMPORTS

using CORE;

// -- TYPES

namespace CORE
{
    public enum SOUND_MASK : uint
    {
        // -- CONSTANTS

        None = 0,
        Loop = 1 << 0,
        Exclusive = 1 << 1,
        Disposable = 1 << 2
    }
}
