// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class ACTION
    {
        // -- ATTRIBUTES

        public ACTION
            SuperAction,
            PriorAction,
            NextAction,
            FirstSubAction,
            LastSubAction;
        public bool
            IsStarted,
            IsFinished;
        public float
            TimeStep,
            Time,
            Speed,
            Delay,
            Duration;
        public bool
            HasDuration;
        public INTERPOLATION_TYPE
            InterpolationType;
        public AnimationCurve
            AnimationCurve_;
        public GameObject
            GameObject_;
        public Transform
            Transform_;
        public RectTransform
            RectTransform_;
        public bool
            IsLocal,
            IsOffset,
            HasX,
            HasY,
            HasZ;

        // -- INQUIRIES

        public float GetTimeRatio(
            )
        {
            if ( Duration > 0.0f )
            {
                return Mathf.Clamp( ( Time - Delay ) / Duration, 0.0f, 1.0f );
            }
            else
            {
                return 1.0f;
            }
        }

        // ~~

        public float GetInterpolationRatio(
            )
        {
            float
                time_ratio;

            time_ratio = GetTimeRatio();

            if ( InterpolationType == INTERPOLATION_TYPE.AnimationCurve )
            {
                return AnimationCurve_.Evaluate( time_ratio );
            }
            else
            {
                return INTERPOLATION.GetRatio( time_ratio, InterpolationType );
            }
        }

        // -- OPERATIONS

        public virtual void Release(
            )
        {
        }

        // ~~

        public virtual void Initialize(
            )
        {
            SuperAction = null;
            PriorAction = null;
            NextAction = null;
            FirstSubAction = null;
            LastSubAction = null;
            IsStarted = false;
            IsFinished = false;
            TimeStep = 0.0f;
            Time = 0.0f;
            Speed = 1.0f;
            Delay = 0.0f;
            Duration = 0.0f;
            HasDuration = false;
            InterpolationType = INTERPOLATION_TYPE.Linear;
            AnimationCurve_ = null;
            GameObject_ = null;
            Transform_ = null;
            RectTransform_ = null;
            IsLocal = false;
            IsOffset = false;
            HasX = false;
            HasY = false;
            HasZ = false;
        }

        // ~~

        public virtual void Finalize_(
            )
        {
            Release();
        }

        // ~~

        public ACTION SetDelay(
            float delay
            )
        {
            Delay = delay;

            return this;
        }

        // ~~

        public ACTION SetDuration(
            float duration
            )
        {
            Duration = duration;
            HasDuration = true;

            return this;
        }

        // ~~

        public ACTION SetInterpolationType(
            INTERPOLATION_TYPE interpolation_type
            )
        {
            InterpolationType = interpolation_type;

            return this;
        }

        // ~~

        public ACTION SetAnimationCurve(
            AnimationCurve animation_curve
            )
        {
            AnimationCurve_ = animation_curve;

            return this;
        }

        // ~~

        public ACTION SetGameObject(
            GameObject game_object
            )
        {
            GameObject_ = game_object;

            return this;
        }

        // ~~

        public ACTION SetTransform(
            Transform transform
            )
        {
            Transform_ = transform;

            return this;
        }

        // ~~

        public ACTION SetRectTransform(
            RectTransform rect_transform
            )
        {
            RectTransform_ = rect_transform;

            return this;
        }

        // ~~

        public ACTION SetIsLocal(
            bool it_is_local
            )
        {
            IsLocal = it_is_local;

            return this;
        }

        // ~~

        public ACTION SetIsOffset(
            bool it_is_offset
            )
        {
            IsOffset = it_is_offset;

            return this;
        }

        // ~~

        public ACTION SetHasX(
            bool it_has_x
            )
        {
            HasX = it_has_x;

            return this;
        }

        // ~~

        public ACTION SetHasY(
            bool it_has_y
            )
        {
            HasY = it_has_y;

            return this;
        }

        // ~~

        public ACTION SetHasZ(
            bool it_has_z
            )
        {
            HasZ = it_has_z;

            return this;
        }

        // ~~

        public void AddFirstSubAction(
            ACTION sub_action
            )
        {
            Debug.Assert(
                sub_action.SuperAction == null
                && sub_action.PriorAction == null
                && sub_action.NextAction == null
                );

            sub_action.SuperAction = this;
            sub_action.NextAction = FirstSubAction;

            FirstSubAction.PriorAction = sub_action;

            if ( FirstSubAction == null )
            {
                FirstSubAction = sub_action;
                LastSubAction = sub_action;
            }
        }

        // ~~

        public void AddLastSubAction(
            ACTION sub_action
            )
        {
            Debug.Assert(
                sub_action.SuperAction == null
                && sub_action.PriorAction == null
                && sub_action.NextAction == null
                );

            sub_action.SuperAction = this;
            sub_action.PriorAction = LastSubAction;

            LastSubAction.NextAction = sub_action;

            if ( FirstSubAction == null )
            {
                FirstSubAction = sub_action;
                LastSubAction = sub_action;
            }
        }

        // ~~

        public void RemoveFirstSubAction(
            )
        {
            ACTION
                sub_action;

            Debug.Assert(
                FirstSubAction != null
                && LastSubAction != null
                );

            sub_action = FirstSubAction;

            if ( FirstSubAction == LastSubAction )
            {
                FirstSubAction = null;
                LastSubAction = null;
            }
            else
            {
                FirstSubAction = FirstSubAction.NextAction;
                FirstSubAction.PriorAction = null;

                sub_action.PriorAction = null;
                sub_action.NextAction = null;
            }

            sub_action.SuperAction = null;
        }

        // ~~

        public void RemoveLastSubAction(
            )
        {
            ACTION
                sub_action;

            Debug.Assert(
                FirstSubAction != null
                && LastSubAction != null
                );

            sub_action = LastSubAction;

            if ( FirstSubAction == LastSubAction )
            {
                FirstSubAction = null;
                LastSubAction = null;
            }
            else
            {
                LastSubAction = LastSubAction.PriorAction;
                LastSubAction.NextAction = null;

                sub_action.PriorAction = null;
                sub_action.NextAction = null;
            }

            sub_action.SuperAction = null;
        }

        // ~~

        public void RemoveSubAction(
            ACTION sub_action
            )
        {
            if ( FirstSubAction == sub_action )
            {
                RemoveFirstSubAction();
            }
            else if ( LastSubAction == sub_action )
            {
                RemoveLastSubAction();
            }
            else
            {
                Debug.Assert(
                    FirstSubAction != null
                    && LastSubAction != null
                    );

                Debug.Assert(
                    sub_action.PriorAction != null
                    && sub_action.NextAction != null
                    );

                sub_action.PriorAction.NextAction = sub_action.NextAction;
                sub_action.NextAction.PriorAction = sub_action.PriorAction;
            }
        }

        // ~~

        public virtual void Start(
            )
        {
        }

        // ~~

        public virtual void Finish(
            )
        {
            IsFinished = true;

            while ( FirstSubAction != null )
            {
                FirstSubAction.Finish();
            }

            if ( SuperAction != null )
            {
                SuperAction.RemoveSubAction( this );
            }

            Finalize_();
        }

        // ~~

        public virtual void UpdateProperty(
            )
        {
        }

        // ~~

        public virtual void Update(
            float time_step
            )
        {
            TimeStep = time_step;
            Time += time_step * Speed;

            if ( Time >= Delay )
            {
                if ( !IsStarted )
                {
                    IsStarted = true;

                    Start();
                }

                if ( !IsFinished )
                {
                    UpdateProperty();
                }

                if ( HasDuration
                     && Time >= Delay + Duration )
                {
                    Finish();
                }
            }
        }
    }
}
