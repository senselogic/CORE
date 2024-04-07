// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "INPUT", "*" ) ]
    #endif

    public class HERO : CHARACTER
    {
        // -- ATTRIBUTES

        public HERO_INPUT
            Input;
        public HERO_LASER_GUN_MUZZLE
            LaserGunMuzzle;
        public AudioClip
            DieAudioClip;
        public AnimationClip
            StandAnimationClip,
            WalkLeftAnimationClip,
            WalkRightAnimationClip,
            WalkBackwardAnimationClip,
            WalkForwardAnimationClip,
            DieAnimationClip;
        public ANIMATION
            StandAnimation,
            WalkLeftAnimation,
            WalkRightAnimation,
            WalkBackwardAnimation,
            WalkForwardAnimation,
            DieAnimation;
        public ANIMATION_MIXER
            AnimationMixer,
            WalkAnimationMixer;
        public ANIMATION_CHANNEL
            WalkAnimationChannel,
            StandAnimationChannel,
            WalkLeftAnimationChannel,
            WalkRightAnimationChannel,
            WalkBackwardAnimationChannel,
            WalkForwardAnimationChannel,
            DieAnimationChannel;
        public LEGS_CONTROLLER
            LegsController;
        public HERO_WALK_STATE
            WalkState;
        public HERO_DIE_STATE
            DieState;
        public STATE
            State;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            MaximumAimXRotationSpeed = 120.0f;
            MaximumWalkYRotationSpeed = 120.0f;
            MaximumWalkXTranslationSpeed = 3.0f;
            MaximumWalkZTranslationSpeed = 4.5f;
            WalkLeftAnimationSpeed = -1.0f;
            WalkRightAnimationSpeed = 1.0f;
            WalkBackwardAnimationSpeed = -1.5f;
            WalkForwardAnimationSpeed = 1.5f;

            MaximumHealth = 100.0f;

            HealingPerSecond = 10.0f;
            MinimumHealingDelay = 5.0f;

            base.OnEnable();

            AddFeatureMask( ( ulong )FEATURE_MASK.Hero );

            LaserGunMuzzle = FindSubEntityByType<HERO_LASER_GUN_MUZZLE>();
            LaserGunMuzzle.Initialize( this );

            StandAnimation = CreateAnimation( "StandAnimation", StandAnimationClip );
            WalkLeftAnimation = CreateAnimation( "WalkLeftAnimation", WalkLeftAnimationClip );
            WalkLeftAnimation.TranslationVelocityVector.x = WalkLeftAnimationSpeed;
            WalkRightAnimation = CreateAnimation( "WalkRightAnimation", WalkRightAnimationClip );
            WalkRightAnimation.TranslationVelocityVector.x = WalkRightAnimationSpeed;
            WalkBackwardAnimation = CreateAnimation( "WalkBackwardAnimation",  WalkBackwardAnimationClip );
            WalkBackwardAnimation.TranslationVelocityVector.z = WalkBackwardAnimationSpeed;
            WalkForwardAnimation = CreateAnimation( "WalkForwardAnimation", WalkForwardAnimationClip );
            WalkForwardAnimation.TranslationVelocityVector.z = WalkForwardAnimationSpeed;
            DieAnimation = CreateAnimation( "DieAnimation", DieAnimationClip );

            WalkAnimationMixer = CreateAnimationMixer( "WalkAnimationMixer", 5 );
            WalkAnimationMixer.IsSynchronized = true;
            WalkAnimationMixer.DenormalizationExponent = 2.3f;

            StandAnimationChannel = WalkAnimationMixer.CreateChannel( StandAnimation );
            WalkLeftAnimationChannel = WalkAnimationMixer.CreateChannel( WalkLeftAnimation );
            WalkLeftAnimationChannel.IsSynchronized = true;
            WalkLeftAnimationChannel.IsDenormalized = true;
            WalkRightAnimationChannel = WalkAnimationMixer.CreateChannel( WalkRightAnimation );
            WalkRightAnimationChannel.IsSynchronized = true;
            WalkRightAnimationChannel.IsDenormalized = true;
            WalkBackwardAnimationChannel = WalkAnimationMixer.CreateChannel( WalkBackwardAnimation );
            WalkBackwardAnimationChannel.IsSynchronized = true;
            WalkBackwardAnimationChannel.IsDenormalized = true;
            WalkForwardAnimationChannel = WalkAnimationMixer.CreateChannel( WalkForwardAnimation );
            WalkForwardAnimationChannel.IsSynchronized = true;
            WalkForwardAnimationChannel.IsDenormalized = true;

            AnimationMixer = CreateAnimationMixer( "AnimationMixer", 2 );

            WalkAnimationChannel = AnimationMixer.CreateChannel( WalkAnimationMixer );
            DieAnimationChannel = AnimationMixer.CreateChannel( DieAnimation );

            SetAnimation( AnimationMixer );

            LegsController
                = new LEGS_CONTROLLER(
                      transform,
                      FindSubTransformByName( "Hips" ),
                      FindSubTransformByName( "UpperLeg_Left" ),
                      FindSubTransformByName( "LowerLeg_Left" ),
                      FindSubTransformByName( "Foot_Left" ),
                      FindSubTransformByName( "UpperLeg_Right" ),
                      FindSubTransformByName( "LowerLeg_Right" ),
                      FindSubTransformByName( "Foot_Right" ),
                      ( uint )LAYER_MASK.Ground,
                      IsValidRaycastHitFunction
                      );

            WalkState = new HERO_WALK_STATE( "WalkState", this );
            DieState = new HERO_DIE_STATE( "DieState", this );

            State = new STATE( WalkState );
        }

        // ~~

        public void UpdateInput(
            )
        {
            Input.AimXRotationSpeed = INPUT.AimVerticallyAxis * MaximumAimXRotationSpeed;
            Input.WalkYRotationSpeed = INPUT.AimHorizontallyAxis * MaximumWalkYRotationSpeed;
            Input.WalkXTranslationSpeed = INPUT.MoveLaterallyAxis * MaximumWalkXTranslationSpeed;
            Input.WalkZTranslationSpeed = INPUT.MoveFrontallyAxis * MaximumWalkZTranslationSpeed;
            Input.IsShooting = INPUT.ShootButtonIsPressed;
        }

        // ~~

        public void UpdateWalkAnimation(
            )
        {
            float
                walk_backward_animation_speed,
                walk_forward_animation_speed,
                walk_left_animation_speed,
                walk_right_animation_speed;

            WalkAnimationMixer.ClearWeights( 10.0f );
            WalkAnimationMixer.AddedRotationVelocityVector.y = Input.WalkYRotationSpeed;

            walk_left_animation_speed = Mathf.Max( Input.WalkXTranslationSpeed / WalkLeftAnimationSpeed, 0.0f );
            walk_right_animation_speed = Mathf.Max( Input.WalkXTranslationSpeed / WalkRightAnimationSpeed, 0.0f );
            walk_backward_animation_speed = Mathf.Max( Input.WalkZTranslationSpeed / WalkBackwardAnimationSpeed, 0.0f );
            walk_forward_animation_speed = Mathf.Max( Input.WalkZTranslationSpeed / WalkForwardAnimationSpeed, 0.0f );

            WalkLeftAnimationChannel.SetWishedSpeed( walk_left_animation_speed, 0.2f );
            WalkRightAnimationChannel.SetWishedSpeed( walk_right_animation_speed, 0.2f );
            WalkBackwardAnimationChannel.SetWishedSpeed( walk_backward_animation_speed, 0.2f );
            WalkForwardAnimationChannel.SetWishedSpeed( walk_forward_animation_speed, 0.2f );
            TranslationVelocityVector.y = 0.0f;
        }

        // ~~

        public override void Die(
            )
        {
            base.Die();

            State.ChangeState( DieState );
        }

        // ~~

        public void EnterWalkState(
            STATE old_state,
            STATE new_state
            )
        {
            AnimationMixer.ClearWeights( 10.0f );
            WalkAnimationChannel.SetWeight( 1.0f );

            if ( WalkAnimationChannel.GetOldWeight() == 0.0f )
            {
                WalkAnimationChannel.SetTime( 0.0f );
            }
        }

        // ~~

        public void UpdateWalkState(
            )
        {
            UpdateWalkAnimation();

            if ( Input.IsShooting )
            {
                LaserGunMuzzle.Shoot();

                HealingDelay = 0.0f;
            }
            else
            {
                Heal();
            }
        }

        // ~~

        public void EnterDieState(
            STATE old_state,
            STATE new_state
            )
        {
            AnimationMixer.ClearWeights( 10.0f );
            DieAnimationChannel.SetWeight( 1.0f );
            DieAnimationChannel.SetTime( 0.0f );

            SOUND.Create( GetPositionVector(), DieAudioClip, SOUND_MASK.Disposable );
        }

        // ~~

        public void UpdateDieState(
            )
        {
            DieState.Time += TimeStep;
        }

        // ~~

        public void Update(
            )
        {
            if ( !IsFrozen )
            {
                UpdateInput();
            }

            BeginUpdate();
            State.Update();
            EndUpdate();
        }

        // ~~

        public void LateUpdate(
            )
        {
            LegsController.Update( TimeStep );
        }
    }
}
