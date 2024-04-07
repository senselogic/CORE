// -- IMPORTS

using UnityEngine.AI;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class ENEMY_CHASE_STATE : STATE
    {
        // -- ATTRIBUTES

        public ENEMY
            Enemy;

        // -- CONSTRUCTORS

        public ENEMY_CHASE_STATE(
            string name,
            ENEMY enemy
            ) :
            base( name )
        {
            Enemy = enemy;
        }

        // -- OPERATIONS

        public override void EnterState(
            STATE old_state,
            STATE new_state
            )
        {
            Enemy.EnterChaseState( old_state, new_state );
        }

        // ~~

        public override void Update(
            )
        {
            Enemy.UpdateChaseState();
        }

        // ~~

        public override void ExitState(
            STATE old_state,
            STATE new_state
            )
        {
            Enemy.ExitChaseState( old_state, new_state );
        }
    }
}
