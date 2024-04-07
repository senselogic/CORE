// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "GOLEM", "*" ) ]
    #endif

    public class GOLEM_FIST : CONTACT_WEAPON
    {
        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            Radius = 0.5f;
            RequiredFeatureMask = ( ulong )FEATURE_MASK.Hero;
            Damage = 100;
        }
    }
}
