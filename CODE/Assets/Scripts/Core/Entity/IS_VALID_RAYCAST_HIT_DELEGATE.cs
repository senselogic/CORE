// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public delegate bool IS_VALID_RAYCAST_HIT_DELEGATE(
        ref RaycastHit raycast_hit
        );
}
