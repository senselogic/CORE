// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class GAME_FAIL_MISSION_STATE : STATE
    {
        // -- ATTRIBUTES

        public GAME
            Game;

        // -- CONSTRUCTORS

        public GAME_FAIL_MISSION_STATE(
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
            Game.EnterFailMissionState( old_state, new_state );
        }

        // ~~

        public override void ExitState(
            STATE old_state,
            STATE new_state
            )
        {
            Game.ExitFailMissionState( old_state, new_state );
        }
    }
}
