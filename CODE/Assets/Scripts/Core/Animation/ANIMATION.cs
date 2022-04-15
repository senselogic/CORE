// -- IMPORTS

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using CORE;

// -- TYPES

namespace CORE
{
    public class ANIMATION
    {
        // -- ATTRIBUTES

        public string
            Name;
        public ANIMATION_TYPE
            Type;
        public PlayableGraph
            PlayableGraph_;
        public AnimationClipPlayable
            ClipPlayable;
        public AnimationClip
            Clip;
        public int
            ChannelIndex,
            ChannelCount;
        public ANIMATION_CHANNEL[]
            ChannelArray;
        public ANIMATION_CHANNEL
            DefaultChannel;
        public bool
            IsUpdated,
            IsSynchronized;
        public Vector3
            TranslationVelocityVector,
            RotationVelocityVector,
            MixedTranslationVelocityVector,
            MixedRotationVelocityVector,
            AddedTranslationVelocityVector,
            AddedRotationVelocityVector;
        public float
            MixedTranslationVelocityFactor,
            MixedRotationVelocityFactor,
            AddedTranslationVelocityFactor,
            AddedRotationVelocityFactor,
            DenormalizationExponent;

        // -- CONSTRUCTORS

        public ANIMATION(
            string name,
            ANIMATION_TYPE animation_type,
            PlayableGraph playable_graph,
            bool animation_is_updated
            )
        {
            Name = name;
            Type = animation_type;
            PlayableGraph_ = playable_graph;
            IsUpdated = animation_is_updated;
            MixedTranslationVelocityFactor = 1.0f;
            MixedRotationVelocityFactor = 1.0f;
            AddedTranslationVelocityFactor = 1.0f;
            AddedRotationVelocityFactor = 1.0f;
            DenormalizationExponent = 1.0f;
        }

        // ~~

        public ANIMATION(
            string name,
            PlayableGraph playable_graph,
            AnimationClipPlayable animation_clip_playable,
            AnimationClip animation_clip
            ) :
            this( name, ANIMATION_TYPE.Clip, playable_graph, false )
        {
            ClipPlayable = animation_clip_playable;
            Clip = animation_clip;
        }

        // -- INQUIRIES

        public float GetSpeed(
            )
        {
            Debug.Assert( Type == ANIMATION_TYPE.Clip );

            return ( float )ClipPlayable.GetSpeed();
        }

        // ~~

        public float GetTime(
            )
        {
            Debug.Assert( Type == ANIMATION_TYPE.Clip );

            return ( float )ClipPlayable.GetTime();
        }

        // ~~

        public float GetDuration(
            )
        {
            Debug.Assert( Type == ANIMATION_TYPE.Clip );

            return Clip.length;
        }

        // ~~

        public WrapMode GetWrapMode(
            )
        {
            Debug.Assert( Type == ANIMATION_TYPE.Clip );

            return Clip.wrapMode;
        }

        // ~~

        public virtual void Log(
            TEXT text,
            int indentation_count
            )
        {
            text.AddText(
                Name,
                "(",
                GetSpeed(),
                ":",
                GetTime(),
                ")\n"
                );
        }

        // ~~

        public virtual void Log(
            )
        {
            TEXT
                text;

            text = new TEXT();

            Log( text, 0 );

            Debug.Log( text.GetString() );
        }

        // -- OPERATIONS

        public virtual void ConnectChannel(
            AnimationMixerPlayable animation_mixer_playable,
            int channel_index
            )
        {
            PlayableGraph_.Connect( ClipPlayable, 0, animation_mixer_playable, channel_index );
        }

        // ~~

        public virtual void ConnectChannel(
            AnimationLayerMixerPlayable animation_layer_mixer_playable,
            int channel_index,
            bool channel_is_additive
            )
        {
            PlayableGraph_.Connect( ClipPlayable, 0, animation_layer_mixer_playable, channel_index );

            animation_layer_mixer_playable.SetLayerAdditive( ( uint )channel_index, channel_is_additive );
        }

        // ~~

        public virtual void ConnectOutput(
            AnimationPlayableOutput animation_playable_output
            )
        {
            animation_playable_output.SetSourcePlayable( ClipPlayable );
        }

        // ~~

        public virtual void Update(
            float time_step
            )
        {
        }

        // ~~

        public virtual void SetSpeed(
            float speed
            )
        {
            Debug.Assert( Type == ANIMATION_TYPE.Clip );

            ClipPlayable.SetSpeed( speed );
        }

        // ~~

        public virtual void SetTime(
            float time
            )
        {
            Debug.Assert( Type == ANIMATION_TYPE.Clip );

            ClipPlayable.SetTime( time );
        }

        // ~~

        public virtual void SetWrapMode(
            WrapMode wrap_mode
            )
        {
            Debug.Assert( Type == ANIMATION_TYPE.Clip );

            Clip.wrapMode = wrap_mode;
        }
    }
}
