// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class SOUND : ATTACHED_ENTITY
    {
        // -- ATTRIBUTES

        public AudioSource
            AudioSource;
        public SOUND_CHANNEL
            Channel;
        public static POOL_<SOUND>
            Pool;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            AudioSource = GetComponent<AudioSource>();

            if ( Channel == null )
            {
                Channel = new SOUND_CHANNEL( AudioSource );
            }
        }

        // ~~

        public void Update(
            )
        {
            BeginUpdate();

            if ( Channel != null )
            {
                Channel.Update( TimeStep );

                if ( Channel.IsReleasable() )
                {
                    Release();
                }
            }

            EndUpdate();
        }

        // ~~

        public void Release(
            )
        {
            AudioSource.Stop();

            Pool.ReleaseEntity( this );
        }

        // ~~

        public void Release(
            float transition_duration
            )
        {
            Channel.Mask |= SOUND_MASK.Disposable;
            Channel.Time = 0.0f;
            Channel.Duration = transition_duration;
        }

        // ~~

        public static void InitializePool(
            )
        {
            if ( Pool == null )
            {
                Pool = new POOL_<SOUND>( "Prefabs/AudioEffect", 8 );
            }
        }

        // ~~

        public static SOUND Create(
            Transform transform,
            AudioClip audio_clip,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            int volume_scale_index = 0,
            float volume = 1.0f,
            float pitch = 1.0f,
            float transition_duration = 0.0f
            )
        {
            SOUND
                sound;

            sound = Pool.CreateEntity( transform.position, transform.rotation );
            sound.ReferenceTransform = transform;

            sound.Channel.PlayAudioClip(
                audio_clip,
                sound_mask,
                volume_scale_index,
                volume,
                pitch,
                transition_duration
                );

            return sound;
        }

        // ~~

        public static SOUND Create(
            Transform transform,
            AudioClip[] audio_clip_array,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            int volume_scale_index = 0,
            float volume = 1.0f,
            float pitch = 1.0f,
            float transition_duration = 0.0f
            )
        {
            return
                Create(
                    transform,
                    audio_clip_array[ Random.Range( 0, audio_clip_array.Length ) ],
                    sound_mask,
                    volume_scale_index,
                    volume,
                    pitch,
                    transition_duration
                    );
        }

        // ~~

        public static SOUND Create(
            Vector3 position_vector,
            AudioClip audio_clip,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            int volume_scale_index = 0,
            float volume = 1.0f,
            float pitch = 1.0f,
            float transition_duration = 0.0f
            )
        {
            SOUND
                sound;

            sound = Pool.CreateEntity( position_vector, Quaternion.identity );
            sound.ReferenceTransform = null;

            sound.Channel.PlayAudioClip(
                audio_clip,
                sound_mask,
                volume_scale_index,
                volume,
                pitch,
                transition_duration
                );

            return sound;
        }

        // ~~

        public static SOUND Create(
            Vector3 position_vector,
            AudioClip[] audio_clip_array,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            int volume_scale_index = 0,
            float volume = 1.0f,
            float pitch = 1.0f,
            float transition_duration = 0.0f
            )
        {
            return
                Create(
                    position_vector,
                    audio_clip_array[ Random.Range( 0, audio_clip_array.Length ) ],
                    sound_mask,
                    volume_scale_index,
                    volume,
                    pitch,
                    transition_duration
                    );
        }
    }
}
