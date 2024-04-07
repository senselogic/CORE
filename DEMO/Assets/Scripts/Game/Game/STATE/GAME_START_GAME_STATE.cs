// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class GAME_START_GAME_STATE : STATE
    {
        // -- ATTRIBUTES

        public GAME
            Game;

        // -- CONSTRUCTORS

        public GAME_START_GAME_STATE(
            string name,
            GAME game
            ) :
            base( name )
        {
            Game = game;
        }

        // -- OPERATIONS

        public override void EnterState(
            STATE old_state,
            STATE new_state
            )
        {
            Game.EnterStartGameState( old_state, new_state );
        }

        // ~~

        public override void ExitState(
            STATE old_state,
            STATE new_state
            )
        {
            Game.ExitStartGameState( old_state, new_state );
        }
    }
}
