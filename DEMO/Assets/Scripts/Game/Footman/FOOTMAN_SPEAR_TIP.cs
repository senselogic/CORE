// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "FOOTMAN", "*" ) ]
    #endif

    public class FOOTMAN_SPEAR_TIP : CONTACT_WEAPON
    {
        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            Radius = 0.25f;
            RequiredFeatureMask = ( ulong )FEATURE_MASK.Hero;
            Damage = 100;
        }
    }
}
