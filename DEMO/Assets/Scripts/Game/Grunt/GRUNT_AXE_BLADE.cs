// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "GRUNT", "*" ) ]
    #endif

    public class GRUNT_AXE_BLADE : CONTACT_WEAPON
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
