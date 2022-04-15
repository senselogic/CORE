// -- IMPORTS

using CORE;

// -- TYPES

namespace CORE
{
    public struct ANIMATION_PARAMETER
    {
        // -- ATTRIBUTES

        public float
            OldValue,
            Value,
            FinalValue,
            TransitionSpeed;

        // -- OPERATIONS

        public void SetValue(
            float value
            )
        {
            OldValue = value;
            Value = value;
            FinalValue = value;
            TransitionSpeed = 0.0f;
        }

        // ~~

        public void SetFinalValue(
            float final_value,
            float transition_speed
            )
        {
            FinalValue = final_value;
            TransitionSpeed = transition_speed;
        }

        // ~~

        public void SetFinalValue(
            float old_value,
            float final_value,
            float transition_speed
            )
        {
            OldValue = old_value;
            Value = old_value;
            FinalValue = final_value;
            TransitionSpeed = transition_speed;
        }

        // ~~

        public void Update(
            float time_step
            )
        {
            if ( Value != FinalValue )
            {
                if ( TransitionSpeed <= 0.0f )
                {
                    Value = FinalValue;
                }
                else if ( Value < FinalValue )
                {
                    Value += TransitionSpeed * time_step;

                    if ( Value > FinalValue )
                    {
                        Value = FinalValue;
                    }
                }
                else if ( Value > FinalValue )
                {
                    Value -= TransitionSpeed * time_step;

                    if ( Value < FinalValue )
                    {
                        Value = FinalValue;
                    }
                }
            }
        }

        // ~~

        public bool Apply(
            )
        {
            if ( OldValue != Value )
            {
                OldValue = Value;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
