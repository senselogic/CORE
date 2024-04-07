// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class HERO_WALK_STATE : STATE
    {
        // -- ATTRIBUTES

        public HERO
            Hero;

        // -- CONSTRUCTORS

        public HERO_WALK_STATE(
            string name,
            HERO hero
            ) :
            base( name )
        {
            Hero = hero;
        }

        // -- OPERATIONS

        public override void EnterState(
            STATE old_state,
            STATE new_state
            )
        {
            Hero.EnterWalkState( old_state, new_state );
        }

        // ~~

        public override void Update(
            )
        {
            Hero.UpdateWalkState();
        }
    }
}
