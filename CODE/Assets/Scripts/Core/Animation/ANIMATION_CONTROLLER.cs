// -- IMPORTS

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using CORE;

// -- TYPES

namespace CORE
{
    public class ANIMATION_CONTROLLER : ANIMATION
    {
        // -- ATTRIBUTES

        public AnimatorControllerPlayable
            AnimatorControllerPlayable_;

        // -- CONSTRUCTORS

        public ANIMATION_CONTROLLER(
            string name,
            PlayableGraph playable_graph,
            AnimatorControllerPlayable animator_controller_playable
            ) :
            base( name, ANIMATION_TYPE.AnimatorController, playable_graph, false )
        {
            AnimatorControllerPlayable_ = animator_controller_playable;
        }

        // -- INQUIRIES

        public override void Log(
            TEXT text,
            int indentation_count
            )
        {
        }

        // -- OPERATIONS

        public override void ConnectChannel(
            AnimationMixerPlayable animation_mixer_playable,
            int channel_index
            )
        {
            PlayableGraph_.Connect( AnimatorControllerPlayable_, 0, animation_mixer_playable, channel_index );
        }

        // ~~

        public override void ConnectChannel(
            AnimationLayerMixerPlayable animation_layer_mixer_playable,
            int channel_index,
            bool channel_is_additive
            )
        {
            PlayableGraph_.Connect( AnimatorControllerPlayable_, 0, animation_layer_mixer_playable, channel_index );

            animation_layer_mixer_playable.SetLayerAdditive( ( uint )channel_index, channel_is_additive );
        }

        // ~~

        public override void ConnectOutput(
            AnimationPlayableOutput animation_playable_output
            )
        {
            animation_playable_output.SetSourcePlayable( AnimatorControllerPlayable_ );
        }
    }
}
