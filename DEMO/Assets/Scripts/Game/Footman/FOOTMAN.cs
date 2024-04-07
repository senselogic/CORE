// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class FOOTMAN : ENEMY
    {
        // -- ATTRIBUTES

        public FOOTMAN_SPEAR_TIP
            SpearTip;

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

            MaximumHealth = 50.0f;
            KillScore = 50;

            SideDistanceFactor = 0.25f;
            MinimumAttackDistance = 3.0f;
            MaximumAttackDistance = 4.0f;
            MaximumAttackAngle = 60.0f;

            base.OnEnable();

            SpearTip = FindSubEntityByType<FOOTMAN_SPEAR_TIP>();
            SpearTip.Initialize( this );
        }

        // ~~

        public override void UpdateAttackState(
            )
        {
            base.UpdateAttackState();

            if ( SpearTip.Moves()
                 && AttackState.Time > AttackAnimationClip.length * 0.2f )
            {
                SpearTip.Hurt();
            }
            else if ( SpearTip.Hurts()
                      && AttackState.Time > AttackAnimationClip.length * 0.4f )
            {
                SpearTip.Move();
            }
        }

        // ~~

        public override void ExitAttackState(
            STATE old_state,
            STATE new_state
            )
        {
            base.ExitAttackState( old_state, new_state );

            SpearTip.Move();
        }
    }
}
