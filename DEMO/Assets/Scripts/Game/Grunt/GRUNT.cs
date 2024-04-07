// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class GRUNT : ENEMY
    {
        // -- ATTRIBUTES

        public GRUNT_AXE_BLADE
            AxeBlade;

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

            MaximumHealth = 100.0f;
            KillScore = 100;

            SideDistanceFactor = 0.25f;
            MinimumAttackDistance = 3.0f;
            MaximumAttackDistance = 4.0f;
            MaximumAttackAngle = 60.0f;

            base.OnEnable();

            AxeBlade = FindSubEntityByType<GRUNT_AXE_BLADE>();
            AxeBlade.Initialize( this );
        }

        // ~~

        public override void UpdateAttackState(
            )
        {
            base.UpdateAttackState();

            if ( AxeBlade.Moves()
                 && AttackState.Time > AttackAnimationClip.length * 0.2f )
            {
                AxeBlade.Hurt();
            }
            else if ( AxeBlade.Hurts()
                      && AttackState.Time > AttackAnimationClip.length * 0.4f )
            {
                AxeBlade.Move();
            }
        }

        // ~~

        public override void ExitAttackState(
            STATE old_state,
            STATE new_state
            )
        {
            base.ExitAttackState( old_state, new_state );

            AxeBlade.Move();
        }
    }
}
