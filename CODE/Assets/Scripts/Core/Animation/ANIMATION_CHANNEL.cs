// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class ANIMATION_CHANNEL
    {
        // -- ATTRIBUTES

        public ANIMATION
            Animation;
        public ANIMATION_PARAMETER
            WeightParameter;
        public float
            TimeStep,
            SpeedFactor;
        public bool
            IsNormalized,
            IsSynchronized,
            IsDenormalized;

        // -- CONSTRUCTORS

        public ANIMATION_CHANNEL(
            ANIMATION animation
            )
        {
            Animation = animation;
            IsNormalized = true;
        }

        // -- INQUIRIES

        public float GetOldWeight(
            )
        {
            return WeightParameter.OldValue;
        }

        // ~~

        public float GetWeight(
            )
        {
            return WeightParameter.FinalValue;
        }

        // ~~

        public float GetSpeed(
            )
        {
            if ( Animation.Type == ANIMATION_TYPE.Clip )
            {
                return Animation.GetSpeed();
            }
            else
            {
                return 0.0f;
            }
        }

        // ~~

        public float GetTime(
            )
        {
            if ( Animation.Type == ANIMATION_TYPE.Clip )
            {
                return Animation.GetTime();
            }
            else
            {
                return 0.0f;
            }
        }

        // ~~

        public float GetDuration(
            )
        {
            if ( Animation.Type == ANIMATION_TYPE.Clip )
            {
                return Animation.GetDuration();
            }
            else
            {
                return 0.0f;
            }
        }

        // ~~

        public WrapMode GetWrapMode(
            )
        {
            if ( Animation.Type == ANIMATION_TYPE.Clip )
            {
                return Animation.GetWrapMode();
            }
            else
            {
                return WrapMode.Default;
            }
        }

        // -- OPERATIONS

        public void SetWeight(
            float weight
            )
        {
            WeightParameter.FinalValue = weight;
        }

        // ~~

        public void SetWeight(
            float weight,
            float transition_speed
            )
        {
            WeightParameter.FinalValue = weight;
            WeightParameter.TransitionSpeed = transition_speed;
        }

        // ~~

        public void SetSpeed(
            float animation_speed
            )
        {
            Animation.SetSpeed( animation_speed );
        }

        // ~~

        public void SetWishedSpeed(
            float wished_speed,
            float minimum_speed,
            float speed_factor = 1.0f
            )
        {
            if ( wished_speed <= minimum_speed )
            {
                WeightParameter.FinalValue = 0.0f;
                Animation.SetSpeed( 0.0f );
            }
            else if ( wished_speed <= 1.0f )
            {
                WeightParameter.FinalValue = wished_speed;
                Animation.SetSpeed( speed_factor );
            }
            else
            {
                WeightParameter.FinalValue = 1.0f;
                Animation.SetSpeed( wished_speed * speed_factor );
            }
        }

        // ~~

        public void SetTime(
            float animation_time
            )
        {
            Animation.SetTime( animation_time );
        }
    }
}
