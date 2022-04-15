// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class PARTICLE_ENTITY : ATTACHED_ENTITY
    {
        // -- ATTRIBUTES

        public ParticleSystem
            ParticleSystem_;
        public bool
            IsPaused;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            Duration = 0.5f;
            ParticleSystem_ = GetComponent<ParticleSystem>();
            IsPaused = false;
        }

        // ~~

        public void Pause(
            )
        {
            ParticleSystem_.Pause();
        }

        // ~~

        public void Resume(
            )
        {
            ParticleSystem_.Play();
        }

        // ~~

        public virtual void Release(
            )
        {
        }

        // ~~

        public void Update(
            )
        {
            BeginUpdate();

            if ( IsPaused != ( TimeStep == 0.0f ) )
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

            Time += TimeStep;

            if ( Time >= Duration )
            {
                Time = 0.0f;

                if ( !ParticleSystem_.IsAlive( true ) )
                {
                    Release();
                }
            }

            EndUpdate();
        }
    }
}
