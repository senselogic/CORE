// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class SOUND_MIXER
    {
        // -- ATTRIBUTES

        public int
            GroupCount;
        public SOUND_CHANNEL[]
            ChannelArray;
        public int
            ChannelCount,
            ChannelIndex;
        public bool
            IsPlaying;

        // -- CONSTRUCTORS

        public SOUND_MIXER(
            GameObject game_object,
            int source_index = 0,
            int source_count = 0
            )
        {
            int
                channel_index;
            AudioSource[]
                audio_source_array;

            audio_source_array = game_object.GetComponents<AudioSource>();

            if ( source_count <= 0 )
            {
                source_count = audio_source_array.Length - source_index;
            }

            ChannelCount = source_count;
            ChannelArray = new SOUND_CHANNEL[ ChannelCount ];

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                ChannelArray[ channel_index ] = new SOUND_CHANNEL( audio_source_array[ source_index + channel_index ] );
            }

            ChannelIndex = -1;
        }

        // -- INQUIRIES

        public bool IsPlayingAudioClip(
            AudioClip audio_clip
            )
        {
            int
                channel_index;

            for ( channel_index = 0;
                  channel_index < ChannelCount;
                  ++channel_index )
            {
                if ( ChannelArray[ channel_index ].IsPlayingAudioClip( audio_clip ) )
                {
                    return true;
                }
            }

            return false;
        }

        // -- OPERATIONS

        public void PlayAudioClip(
            AudioClip audio_clip,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            int volume_scale_index = 0,
            float volume = 1.0f,
            float pitch = 1.0f,
            float transition_duration = 0.0f
            )
        {
            int
                channel_index,
                other_channel_index;

            channel_index = ( ChannelIndex + 1 ) % ChannelCount;

            ChannelArray[ channel_index ].PlayAudioClip(
                audio_clip,
                sound_mask,
                volume_scale_index,
                volume,
                pitch,
                transition_duration
                );

            if ( ( sound_mask & SOUND_MASK.Exclusive ) == SOUND_MASK.Exclusive
                 && ChannelCount > 1 )
            {
                for ( other_channel_index = 0;
                      other_channel_index < ChannelCount;
                      ++other_channel_index )
                {
                    if ( other_channel_index != channel_index )
                    {
                        ChannelArray[ other_channel_index ].Stop( transition_duration );
                    }
                }
            }

            ChannelIndex = channel_index;

            IsPlaying = true;
        }

        // ~~

        public void PlayRandomAudioClip(
            AudioClip[] audio_clip_array,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            int volume_scale_index = 0,
            float volume = 1.0f,
            float pitch = 1.0f,
            float transition_duration = 0.0f
            )
        {
            PlayAudioClip(
                audio_clip_array[ Random.Range( 0, audio_clip_array.Length ) ],
                sound_mask,
                volume_scale_index,
                volume,
                pitch,
                transition_duration
                );
        }

        // ~~

        public void Stop(
            float transition_duration = 0.0f
            )
        {
            int
                channel_index;

            if ( ChannelArray != null )
            {
                for ( channel_index = 0;
                      channel_index < ChannelCount;
                      ++channel_index )
                {
                    ChannelArray[ channel_index ].Stop( transition_duration );
                }
            }

            if ( transition_duration <= 0.0f )
            {
                IsPlaying = false;
            }
        }

        // ~~

        public void Update(
            float time_step
            )
        {
            int
                channel_index;

            if ( IsPlaying )
            {
                IsPlaying = false;

                for ( channel_index = 0;
                      channel_index < ChannelCount;
                      ++channel_index )
                {
                    ChannelArray[ channel_index ].Update( time_step );

                    if ( ChannelArray[ channel_index ].IsPlaying() )
                    {
                        IsPlaying = true;
                    }
                }
            }
        }
    }
}
