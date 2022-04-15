// -- IMPORTS

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using CORE;

// -- TYPES

namespace CORE
{
    public class ANIMATED_ENTITY : FALLING_ENTITY
    {
        // -- ATTRIBUTES

        public Animator
            Animator_;
        public PlayableGraph
            PlayableGraph_;
        public AnimationPlayableOutput
            PlayableOutput;
        public ANIMATION
            Animation;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            Animator_ = GetComponent<Animator>();
            PlayableGraph_ = PlayableGraph.Create();
            PlayableGraph_.SetTimeUpdateMode( DirectorUpdateMode.Manual );
            PlayableOutput = AnimationPlayableOutput.Create( PlayableGraph_, "Animation", Animator_);
        }

        // ~~

        public ANIMATION CreateAnimation(
            string name,
            AnimationClip animation_clip
            )
        {
            return
                new ANIMATION(
                    name,
                    PlayableGraph_,
                    AnimationClipPlayable.Create( PlayableGraph_, animation_clip ),
                    animation_clip
                    );
        }

        // ~~

        public ANIMATION_MIXER CreateAnimationMixer(
            string name,
            int channel_count
            )
        {
            return
                new ANIMATION_MIXER(
                    name,
                    PlayableGraph_,
                    AnimationMixerPlayable.Create( PlayableGraph_, channel_count ),
                    channel_count
                    );
        }

        // ~~

        public ANIMATION_MIXER CreateAnimationLayerMixer(
            string name,
            int channel_count
            )
        {
            return
                new ANIMATION_MIXER(
                    name,
                    PlayableGraph_,
                    AnimationLayerMixerPlayable.Create( PlayableGraph_, channel_count ),
                    channel_count
                    );
        }

        // ~~

        public ANIMATION_CONTROLLER CreateAnimationController(
            string name,
            RuntimeAnimatorController runtime_animator_controller
            )
        {
            return
                new ANIMATION_CONTROLLER(
                    name,
                    PlayableGraph_,
                    AnimatorControllerPlayable.Create( PlayableGraph_, runtime_animator_controller )
                    );
        }

        // ~~

        public void Play(
            ANIMATION animation
            )
        {
            animation.ConnectOutput( PlayableOutput );

            PlayableGraph_.Play();
        }

        // ~~

        public void SetAnimation(
            ANIMATION animation
            )
        {
            Animation = animation;

            Play( animation );
        }

        // ~~

        public new void EndUpdate(
            )
        {
            Vector3
                orientation_euler_vector;

            Animation.Update( TimeStep );

            if ( TimeStep > 0.0f )
            {
                if ( Animation.RotationVelocityVector.x != 0.0f
                     || Animation.RotationVelocityVector.y != 0.0f
                     || Animation.RotationVelocityVector.z != 0.0f )
                {
                    orientation_euler_vector = OrientationQuaternion.eulerAngles;

                    OrientationQuaternion
                        = Quaternion.Euler(
                               orientation_euler_vector.x + Animation.RotationVelocityVector.x * TimeStep,
                               orientation_euler_vector.y + Animation.RotationVelocityVector.y * TimeStep,
                               orientation_euler_vector.z + Animation.RotationVelocityVector.z * TimeStep
                               );
                }

                if ( Animation.TranslationVelocityVector.x != 0.0f
                     || Animation.TranslationVelocityVector.y != 0.0f
                     || Animation.TranslationVelocityVector.z != 0.0f )
                {
                    PositionVector += OrientationQuaternion * ( Animation.TranslationVelocityVector * TimeStep );
                }
            }

            Animator_.Update( TimeStep);
            PlayableGraph_.Evaluate( TimeStep );

            base.EndUpdate();
        }

        // ~~

        public void OnDestroy(
            )
        {
            PlayableGraph_.Destroy();
        }
    }
}
