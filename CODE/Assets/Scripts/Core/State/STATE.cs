// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class STATE
    {
        // -- ATTRIBUTES

        public STATE
            SuperState,
            State;
        public string
            Name;
        public float
            Time;

        // -- CONSTRUCTORS

        public STATE(
            )
        {
            SuperState = null;
            State = null;
            Name = "";
            Time = 0.0f;
        }

        // ~~

        public STATE(
            STATE state
            )
        {
            State = state;

            State.EnterState( null, state );
        }

        // ~~

        public STATE(
            string name,
            STATE super_state = null
            )
        {
            SuperState = super_state;
            State = null;
            Name = name;
            Time = 0.0f;
        }

        // -- INQUIRIES

        public STATE GetSubState(
            STATE base_state
            )
        {
            STATE
                sub_state;

            for ( sub_state = this;
                  sub_state != null;
                  sub_state = sub_state.SuperState )
            {
                if ( sub_state.SuperState == base_state )
                {
                    return sub_state;
                }
            }

            return sub_state;
        }

        // ~~

        public bool HasBaseState(
            STATE base_state
            )
        {
            STATE
                super_state;

            for ( super_state = this;
                  super_state != null;
                  super_state = super_state.SuperState )
            {
                if ( super_state.SuperState == base_state )
                {
                    return true;
                }
            }

            return false;
        }

        // ~~

        public bool HasState(
            STATE state
            )
        {
            return State == state;
        }

        // ~~

        public virtual bool CanEnterState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( State != null )
            {
                return State.CanEnterState( old_state, new_state );
            }
            else
            {
                return true;
            }
        }

        // ~~

        public virtual bool CanExitState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( State != null )
            {
                return State.CanExitState( old_state, new_state );
            }
            else
            {
                return true;
            }
        }

        // -- OPERATIONS

        public virtual void EnterState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( State != null )
            {
                State.EnterState( old_state, new_state );
            }
        }

        // ~~

        public virtual void BeginUpdate(
            )
        {
            if ( State != null )
            {
                State.BeginUpdate();
            }
        }

        // ~~

        public virtual void Update(
            )
        {
            if ( State != null )
            {
                State.Update();
            }
        }

        // ~~

        public virtual void EndUpdate(
            )
        {
            if ( State != null )
            {
                State.EndUpdate();
            }
        }

        // ~~

        public virtual void ExitState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( State != null )
            {
                State.ExitState( old_state, new_state );
            }
        }

        // ~~

        public void SetState(
            STATE new_state
            )
        {
            State = new_state;

            EnterState( null, new_state );
        }

        // ~~

        public bool ChangeState(
            STATE new_state
            )
        {
            STATE
                base_state,
                old_state,
                sub_state;

            old_state = State;

            if ( CanExitState( old_state, new_state )
                 && CanEnterState( old_state, new_state ) )
            {
                base_state = old_state;

                while ( base_state != null
                        && base_state != new_state
                        && !new_state.HasBaseState( base_state ) )
                {
                    State = base_state;
                    base_state.ExitState( old_state, new_state );
                    base_state = base_state.SuperState;
                }

                while ( base_state != new_state )
                {
                    sub_state = new_state.GetSubState( base_state );
                    State = sub_state;
                    sub_state.EnterState( old_state, new_state );

                    base_state = sub_state;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
