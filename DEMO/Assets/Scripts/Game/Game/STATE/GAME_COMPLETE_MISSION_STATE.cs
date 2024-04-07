// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class GAME_COMPLETE_MISSION_STATE : STATE
    {
        // -- ATTRIBUTES

        public GAME
            Game;

        // -- CONSTRUCTORS

        public GAME_COMPLETE_MISSION_STATE(
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
            Game.EnterCompleteMissionState( old_state, new_state );
        }

        // ~~

        public override void ExitState(
            STATE old_state,
            STATE new_state
            )
        {
            Game.ExitCompleteMissionState( old_state, new_state );
        }
    }
}
