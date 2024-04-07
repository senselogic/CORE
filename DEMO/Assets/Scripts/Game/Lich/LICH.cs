// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class LICH : ENEMY
    {
        // -- ATTRIBUTES

        public LICH_STAFF_TIP
            StaffTip;
        public bool
            HasShot;

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

            MaximumHealth = 75.0f;
            KillScore = 75;

            SideDistanceFactor = 0.25f;
            MinimumAttackDistance = 5.0f;
            MaximumAttackDistance = 30.0f;
            MaximumAttackAngle = 30.0f;

            base.OnEnable();

            StaffTip = FindSubEntityByType<LICH_STAFF_TIP>();
            StaffTip.Initialize( this );
        }

        // ~~

        public override void EnterAttackState(
            STATE old_state,
            STATE new_state
            )
        {
            base.EnterAttackState( old_state, new_state );

            HasShot = false;
        }

        // ~~

        public override void UpdateAttackState(
            )
        {
            base.UpdateAttackState();

            if ( !HasShot
                 && AttackState.Time > AttackAnimationClip.length * 0.5f )
            {
                StaffTip.Shoot();

                HasShot = true;
            }
        }
    }
}
