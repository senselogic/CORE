// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class SOUND_CHANNEL
    {
        // -- ATTRIBUTES

        public AudioSource
            AudioSource;
        public AudioClip
            AudioClip_;
        public SOUND_MASK
            Mask;
        public int
            VolumeScaleIndex;
        public ANIMATION_PARAMETER
            VolumeAnimationParameter;
        public float
            Volume,
            Pitch,
            Time,
            Duration;
        public bool
            IsUpdated,
            IsPaused;

        // -- CONSTRUCTORS

        public SOUND_CHANNEL(
            AudioSource audio_source
            )
        {
            AudioSource = audio_source;
        }

        // -- INQUIRIES

        public float GetVolume(
            )
        {
            return VolumeAnimationParameter.FinalValue;
        }

        // ~~

        public bool IsLoop(
            )
        {
            return ( Mask & SOUND_MASK.Loop ) == SOUND_MASK.Loop;
        }

        // ~~

        public bool IsPlaying(
            )
        {
            return
                ( Mask & SOUND_MASK.Loop ) == SOUND_MASK.Loop
                || Time * Pitch < Duration;
        }

        // ~~

        public bool IsFinished(
            )
        {
            return Time * Pitch >= Duration;
        }

        // ~~

        public bool IsMute(
            )
        {
            return VolumeAnimationParameter.FinalValue <= 0.001f;
        }

        // ~~

        public bool IsReleasable(
            )
        {
            return
                ( Mask & SOUND_MASK.Disposable ) == SOUND_MASK.Disposable
                && Time * Pitch >= Duration;
        }

        // ~~

        public bool IsPlayingAudioClip(
            AudioClip audio_clip
            )
        {
            return
                AudioClip_ == audio_clip
                && IsPlaying()
                && !IsMute();
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
            AudioClip_ = audio_clip;
            Mask = sound_mask;

            AudioSource.Stop();
            AudioSource.clip = audio_clip;
            AudioSource.loop = IsLoop();
            AudioSource.pitch = pitch;

            VolumeScaleIndex = volume_scale_index;

            if ( transition_duration <= 0.0f )
            {
                Volume = volume * APPLICATION.VolumeScaleArray[ VolumeScaleIndex ];
                VolumeAnimationParameter.SetValue( volume );
            }
            else
            {
                Volume = 0.0f;
                VolumeAnimationParameter.SetFinalValue( 0.0f, volume, volume / transition_duration );
            }

            AudioSource.volume = Volume;
            AudioSource.Play();

            Pitch = pitch;
            Time = 0.0f;
            Duration = audio_clip.length;
            IsUpdated = true;
            IsPaused = false;
        }

        // ~~

        public void SetVolume(
            float volume,
            float transition_speed = 0.0f
            )
        {
            if ( transition_speed <= 0.0f )
            {
                Volume = volume * APPLICATION.VolumeScaleArray[ VolumeScaleIndex ];
                VolumeAnimationParameter.SetValue( volume );

                AudioSource.volume = Volume;
            }
            else
            {
                VolumeAnimationParameter.SetFinalValue( volume, transition_speed );
            }
        }

        // ~~

        public void Stop(
            float transition_duration = 0.0f
            )
        {
            if ( transition_duration <= 0.0f )
            {
                Volume = 0.0f;
                VolumeAnimationParameter.SetValue( 0.0f );

                AudioSource.Stop();
                AudioSource.volume = 0.0f;
            }
            else
            {
                VolumeAnimationParameter.SetFinalValue( 0.0f, VolumeAnimationParameter.Value / transition_duration );
            }
        }

        // ~~

        public void Pause(
            )
        {
            AudioSource.Pause();

            IsPaused = true;
        }

        // ~~

        public void Resume(
            )
        {
            AudioSource.Play();

            IsPaused = false;
        }

        // ~~

        public void Update(
            float time_offset
            )
        {
            float
                volume;

            Time += time_offset;

            VolumeAnimationParameter.Update( time_offset );
            VolumeAnimationParameter.Apply();

            volume = VolumeAnimationParameter.Value * APPLICATION.VolumeScaleArray[ VolumeScaleIndex ];

            if ( Volume != volume )
            {
                Volume = volume;

                AudioSource.volume = Volume;
            }

            if ( IsPaused != ( time_offset == 0.0f ) )
            {
                IsPaused = !IsPaused;

                if ( IsPaused )
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
    }
}
