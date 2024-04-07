// -- IMPORTS

using UnityEngine.AI;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class ENEMY_SUFFER_ANIMATION_STATE : STATE
    {
        // -- ATTRIBUTES

        public ENEMY
            Enemy;

        // -- CONSTRUCTORS

        public ENEMY_SUFFER_ANIMATION_STATE(
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
            Enemy.EnterSufferAnimationState( old_state, new_state );
        }

        // ~~

        public override void Update(
            )
        {
            Enemy.UpdateSufferAnimationState();
        }
    }
}
