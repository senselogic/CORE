// -- IMPORTS

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using CORE;

// -- TYPES

namespace CORE
{
    public class ANIMATION_MIXER : ANIMATION
    {
        // -- ATTRIBUTES

        public AnimationMixerPlayable
            MixerPlayable;
        public AnimationLayerMixerPlayable
            LayerMixerPlayable;

        // -- CONSTRUCTORS

        public ANIMATION_MIXER(
            string name,
            ANIMATION_TYPE animation_type,
            PlayableGraph playable_graph,
            int channel_count
            ) :
            base( name, animation_type, playable_graph, true )
        {
            Debug.Assert( channel_count > 0 );

            ChannelCount = channel_count;
            ChannelArray = new ANIMATION_CHANNEL[ channel_count ];
        }

        // ~~

        public ANIMATION_MIXER(
            string name,
            PlayableGraph playable_graph,
            AnimationMixerPlayable animation_mixer_playable,
            int channel_count
            ) :
            this( name, ANIMATION_TYPE.Mixer, playable_graph, channel_count )
        {
            MixerPlayable = animation_mixer_playable;
        }

        // ~~

        public ANIMATION_MIXER(
            string name,
            PlayableGraph playable_graph,
            AnimationLayerMixerPlayable animation_layer_mixer_playable,
            int channel_count
            ) :
            this( name, ANIMATION_TYPE.LayerMixer, playable_graph, channel_count )
        {
            LayerMixerPlayable = animation_layer_mixer_playable;
        }

        // -- INQUIRIES

        public override void Log(
            TEXT text,
            int indentation_count
            )
        {
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel;

            text.AddText( Name, " (" );

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];

                text.AddText(
                    animation_channel.WeightParameter.FinalValue,
                    "/",
                    animation_channel.WeightParameter.Value,
                    ":",
                    animation_channel.GetSpeed(),
                    ":",
                    animation_channel.GetTime()
                    );

                if ( channel_index < ChannelCount - 1 )
                {
                    text.AddText( " " );
                }
            }

            text.AddText( ")\n" );

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];

                text.AddRepeatedText( "    ", indentation_count + 1 );
                text.AddText(
                    "[",
                    animation_channel.WeightParameter.FinalValue,
                    "/",
                    animation_channel.WeightParameter.Value,
                    ":",
                    animation_channel.GetSpeed(),
                    ",",
                    animation_channel.GetTime(),
                    "] "
                    );

                animation_channel.Animation.Log( text, indentation_count + 1 );
            }
        }

        // -- OPERATIONS

        public override void ConnectChannel(
            AnimationMixerPlayable animation_mixer_playable,
            int channel_index
            )
        {
            if ( Type == ANIMATION_TYPE.Mixer )
            {
                PlayableGraph_.Connect( MixerPlayable, 0, animation_mixer_playable, channel_index );
            }
            else
            {
                PlayableGraph_.Connect( LayerMixerPlayable, 0, animation_mixer_playable, channel_index );
            }
        }

        // ~~

        public override void ConnectChannel(
            AnimationLayerMixerPlayable animation_layer_mixer_playable,
            int channel_index,
            bool channel_is_additive
            )
        {
            if ( Type == ANIMATION_TYPE.Mixer )
            {
                PlayableGraph_.Connect( MixerPlayable, 0, animation_layer_mixer_playable, channel_index );
            }
            else
            {
                PlayableGraph_.Connect( LayerMixerPlayable, 0, animation_layer_mixer_playable, channel_index );

                animation_layer_mixer_playable.SetLayerAdditive( ( uint )channel_index, channel_is_additive );
            }
        }

        // ~~

        public override void ConnectOutput(
            AnimationPlayableOutput animation_playable_output
            )
        {
            if ( Type == ANIMATION_TYPE.Mixer )
            {
                animation_playable_output.SetSourcePlayable( MixerPlayable );
            }
            else
            {
                animation_playable_output.SetSourcePlayable( LayerMixerPlayable );
            }
        }

        // ~~

        public ANIMATION_CHANNEL CreateChannel(
            ANIMATION animation,
            bool channel_is_additive = false
            )
        {
            ANIMATION_CHANNEL
                animation_channel;

            Debug.Assert( Type == ANIMATION_TYPE.Mixer || Type == ANIMATION_TYPE.LayerMixer );
            Debug.Assert( ChannelCount > 0 && ChannelIndex < ChannelCount );

            animation_channel = new ANIMATION_CHANNEL( animation );
            ChannelArray[ ChannelIndex ] = animation_channel;

            if ( Type == ANIMATION_TYPE.Mixer )
            {
                animation.ConnectChannel( MixerPlayable, ChannelIndex );
            }
            else
            {
                animation.ConnectChannel( LayerMixerPlayable, ChannelIndex, channel_is_additive );
            }

            if ( ChannelIndex == 0 )
            {
                DefaultChannel = animation_channel;
            }

            ++ChannelIndex;

            return animation_channel;
        }

        // ~~

        public void SetDefaultChannel(
            ANIMATION_CHANNEL default_animation_channel
            )
        {
            DefaultChannel = default_animation_channel;
        }

        // ~~

        public void ClearWeights(
            )
        {
            int
                channel_index;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                ChannelArray[ channel_index ].WeightParameter.FinalValue = 0.0f;
            }
        }

        // ~~

        public void ClearWeights(
            float transition_speed
            )
        {
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];
                animation_channel.WeightParameter.FinalValue = 0.0f;
                animation_channel.WeightParameter.TransitionSpeed = transition_speed;
            }
        }

        // ~~

        public void NormalizeWeights(
            )
        {
            float
                one_over_weight_sum,
                weight_sum;
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel;

            if ( ChannelCount > 0 )
            {
                weight_sum = 0.0f;

                for ( channel_index = 0;
                      channel_index < ChannelCount;
                      ++channel_index )
                {
                    animation_channel = ChannelArray[ channel_index ];

                    if ( animation_channel.IsNormalized )
                    {
                        weight_sum += animation_channel.WeightParameter.FinalValue;
                    }
                }

                if ( weight_sum <= 1.0f )
                {
                    DefaultChannel.WeightParameter.FinalValue
                        = 1.0f - ( weight_sum - DefaultChannel.WeightParameter.FinalValue );
                }
                else
                {
                    one_over_weight_sum = 1.0f / weight_sum;

                    for ( channel_index = 0;
                          channel_index < ChannelCount;
                          ++channel_index )
                    {
                        animation_channel = ChannelArray[ channel_index ];

                        if ( animation_channel.IsNormalized )
                        {
                            animation_channel.WeightParameter.FinalValue *= one_over_weight_sum;
                        }
                    }
                }
            }
        }

        // ~~

        public void UpdateWeights(
            float time_step
            )
        {
            float
                one_over_weight_sum,
                weight_sum;
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel;

            Debug.Assert( ChannelCount > 0 );

            weight_sum = 0.0f;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];
                animation_channel.WeightParameter.Update( time_step );
                animation_channel.SpeedFactor = 1.0f;

                if ( animation_channel.IsNormalized )
                {
                    weight_sum += animation_channel.WeightParameter.Value;
                }
            }

            if ( weight_sum <= 1.0f )
            {
                DefaultChannel.WeightParameter.Value
                    = 1.0f - ( weight_sum - DefaultChannel.WeightParameter.Value );
            }
            else
            {
                one_over_weight_sum = 1.0f / weight_sum;

                for ( channel_index = 0;
                      channel_index < ChannelCount;
                      ++channel_index )
                {
                    animation_channel = ChannelArray[ channel_index ];

                    if ( animation_channel.IsNormalized )
                    {
                        animation_channel.WeightParameter.Value *= one_over_weight_sum;
                        animation_channel.SpeedFactor = weight_sum;
                    }

                    if ( animation_channel.IsDenormalized
                         && DenormalizationExponent > 1.0f )
                    {
                        animation_channel.SpeedFactor = Mathf.Pow( animation_channel.SpeedFactor, DenormalizationExponent );
                    }
                }
            }
        }

        // ~~

        public void SynchronizeChannels(
            float time_step
            )
        {
            float
                animation_channel_speed,
                reference_animation_channel_duration,
                reference_animation_channel_speed,
                reference_animation_channel_time;
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel,
                reference_animation_channel;

            reference_animation_channel = null;
            reference_animation_channel_speed = 0.0f;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];

                if ( animation_channel.IsSynchronized
                     && animation_channel.WeightParameter.Value > 0.0f )
                {
                    animation_channel_speed = animation_channel.Animation.GetSpeed();

                    if ( reference_animation_channel == null
                         || animation_channel_speed > reference_animation_channel_speed )
                    {
                        reference_animation_channel = animation_channel;
                        reference_animation_channel_speed = animation_channel_speed;
                    }
                }
            }

            if ( reference_animation_channel != null )
            {
                reference_animation_channel_time = reference_animation_channel.Animation.GetTime();
                reference_animation_channel_duration = reference_animation_channel.Animation.GetDuration();

                for ( channel_index = 0;
                      channel_index < ChannelCount;
                      ++channel_index )
                {
                    animation_channel = ChannelArray[ channel_index ];

                    if ( animation_channel.IsSynchronized
                         && animation_channel != reference_animation_channel )
                    {
                        animation_channel.Animation.SetTime(
                            ( reference_animation_channel_time + reference_animation_channel_speed * time_step )
                            * ( animation_channel.Animation.GetDuration() / reference_animation_channel_duration )
                            - animation_channel.Animation.GetSpeed() * time_step
                            );
                    }
                }
            }
        }

        // ~~

        public void ApplyWeights(
            float time_step
            )
        {
            float
                animation_channel_weight,
                velocity_factor;
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel;

            Debug.Assert( Type == ANIMATION_TYPE.Mixer || Type == ANIMATION_TYPE.LayerMixer );

            MixedTranslationVelocityVector = Vector3.zero;
            MixedRotationVelocityVector = Vector3.zero;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];
                animation_channel_weight = animation_channel.WeightParameter.Value;

                if ( animation_channel.WeightParameter.Apply() )
                {
                    if ( Type == ANIMATION_TYPE.Mixer )
                    {
                        MixerPlayable.SetInputWeight( channel_index, animation_channel.WeightParameter.Value );
                    }
                    else
                    {
                        LayerMixerPlayable.SetInputWeight( channel_index, animation_channel.WeightParameter.Value );
                    }
                }

                if ( animation_channel.Animation.IsUpdated )
                {
                    if ( animation_channel.WeightParameter.Value > 0.0f )
                    {
                        animation_channel.Animation.Update( animation_channel.TimeStep + time_step );
                        animation_channel.TimeStep = 0.0f;
                    }
                    else
                    {
                        animation_channel.TimeStep += time_step;
                    }
                }

                if ( animation_channel_weight > 0.0f )
                {
                    if ( animation_channel.Animation.ChannelCount > 0 )
                    {
                        velocity_factor = animation_channel_weight * animation_channel.SpeedFactor;

                        MixedTranslationVelocityVector += animation_channel.Animation.TranslationVelocityVector * velocity_factor;
                        MixedRotationVelocityVector += animation_channel.Animation.RotationVelocityVector * velocity_factor;
                    }
                    else
                    {
                        velocity_factor = animation_channel_weight * animation_channel.SpeedFactor * animation_channel.Animation.GetSpeed();

                        MixedTranslationVelocityVector += animation_channel.Animation.TranslationVelocityVector * velocity_factor;
                        MixedRotationVelocityVector += animation_channel.Animation.RotationVelocityVector * velocity_factor;
                    }
                }
            }

            TranslationVelocityVector
                = MixedTranslationVelocityVector * MixedTranslationVelocityFactor
                  + AddedTranslationVelocityVector * AddedTranslationVelocityFactor;

            RotationVelocityVector
                = MixedRotationVelocityVector * MixedRotationVelocityFactor
                  + AddedRotationVelocityVector * AddedRotationVelocityFactor;
        }

        // ~~

        public override void Update(
            float time_step
            )
        {
            Debug.Assert( ChannelCount > 0 );

            UpdateWeights( time_step );

            if ( IsSynchronized )
            {
                SynchronizeChannels( time_step);
            }

            ApplyWeights( time_step );
        }

        // ~~

        public override void SetSpeed(
            float speed
            )
        {
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];
                animation_channel.SetSpeed( speed );
            }
        }

        // ~~

        public override void SetTime(
            float time
            )
        {
            int
                channel_index;
            ANIMATION_CHANNEL
                animation_channel;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                animation_channel = ChannelArray[ channel_index ];
                animation_channel.SetTime( time );
            }
        }
    }
}
