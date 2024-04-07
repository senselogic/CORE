// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class GOLEM : ENEMY
    {
        // -- ATTRIBUTES

        public GOLEM_FIST
            LeftFist,
            RightFist;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            MaximumAimXRotationSpeed = 120.0f;
            MaximumWalkYRotationSpeed = 120.0f;
            MaximumWalkXTranslationSpeed = 0.0f;
            MaximumWalkZTranslationSpeed = 4.5f;
            WalkLeftAnimationSpeed = -1.0f;
            WalkRightAnimationSpeed = 1.0f;
            WalkBackwardAnimationSpeed = -1.5f;
            WalkForwardAnimationSpeed = 1.5f;

            MaximumHealth = 200.0f;
            KillScore = 200;

            SideDistanceFactor = 0.25f;
            MinimumAttackDistance = 3.0f;
            MaximumAttackDistance = 4.0f;
            MaximumAttackAngle = 60.0f;

            base.OnEnable();

            LeftFist = FindSubEntityByName<GOLEM_FIST>( "LeftFist" );
            LeftFist.Initialize( this );

            RightFist = FindSubEntityByName<GOLEM_FIST>( "RightFist" );
            RightFist.Initialize( this );
        }

        // ~~

        public override void UpdateAttackState(
            )
        {
            base.UpdateAttackState();

            if ( RightFist.Moves()
                 && AttackState.Time > AttackAnimationClip.length * 0.4f )
            {
                RightFist.Hurt();
            }
            else if ( RightFist.Hurts()
                      && AttackState.Time > AttackAnimationClip.length * 0.8f )
            {
                RightFist.Move();
            }
        }

        // ~~

        public override void ExitAttackState(
            STATE old_state,
            STATE new_state
            )
        {
            base.ExitAttackState( old_state, new_state );

            RightFist.Move();
        }
    }
}
