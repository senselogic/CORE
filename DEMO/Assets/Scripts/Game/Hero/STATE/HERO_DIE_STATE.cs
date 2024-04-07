// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class HERO_DIE_STATE : STATE
    {
        // -- ATTRIBUTES

        public HERO
            Hero;

        // -- CONSTRUCTORS

        public HERO_DIE_STATE(
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
            Hero.EnterDieState( old_state, new_state );
        }

        // ~~

        public override void Update(
            )
        {
            Hero.UpdateDieState();
        }
    }
}
