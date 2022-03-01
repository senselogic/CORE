// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class DYNAMIC_STATE : STATE
    {
        // -- TYPES

        public delegate bool CAN_ENTER_STATE_FUNCTION(
            STATE old_state,
            STATE new_state
            );

        // ~~

        public delegate bool CAN_EXIT_STATE_FUNCTION(
            STATE old_state,
            STATE new_state
            );

        // ~~

        public delegate void ENTER_STATE_FUNCTION(
            STATE old_state,
            STATE new_state
            );

        // ~~

        public delegate void UPDATE_FUNCTION(
            );

        // ~~

        public delegate void EXIT_STATE_FUNCTION(
            STATE old_state,
            STATE new_state
            );

        // -- ATTRIBUTES

        public CAN_ENTER_STATE_FUNCTION
            CanEnterStateFunction;
        public CAN_EXIT_STATE_FUNCTION
            CanExitStateFunction;
        public ENTER_STATE_FUNCTION
            EnterStateFunction;
        public UPDATE_FUNCTION
            BeginUpdateFunction,
            UpdateFunction,
            EndUpdateFunction;
        public EXIT_STATE_FUNCTION
            ExitStateFunction;

        // -- CONSTRUCTORS

        public DYNAMIC_STATE(
            ) :
            base()
        {
            State = null;
            CanEnterStateFunction = null;
            CanExitStateFunction = null;
            EnterStateFunction = null;
            BeginUpdateFunction = null;
            UpdateFunction = null;
            EndUpdateFunction = null;
            ExitStateFunction = null;
        }

        // ~~

        public DYNAMIC_STATE(
            STATE state
            ) :
            base( state )
        {
            CanEnterStateFunction = null;
            CanExitStateFunction = null;
            EnterStateFunction = null;
            BeginUpdateFunction = null;
            UpdateFunction = null;
            EndUpdateFunction = null;
            ExitStateFunction = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name
            ) :
            base( name )
        {
            CanEnterStateFunction = null;
            CanExitStateFunction = null;
            EnterStateFunction = null;
            BeginUpdateFunction = null;
            UpdateFunction = null;
            EndUpdateFunction = null;
            ExitStateFunction = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            UPDATE_FUNCTION update_function = null
            ) :
            base( name )
        {
            CanEnterStateFunction = null;
            CanExitStateFunction = null;
            EnterStateFunction = null;
            BeginUpdateFunction = null;
            UpdateFunction = update_function;
            EndUpdateFunction = null;
            ExitStateFunction = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            ENTER_STATE_FUNCTION enter_state_function = null,
            UPDATE_FUNCTION update_function = null
            ) :
            base( name )
        {
            CanEnterStateFunction = null;
            CanExitStateFunction = null;
            EnterStateFunction = enter_state_function;
            BeginUpdateFunction = null;
            UpdateFunction = update_function;
            EndUpdateFunction = null;
            ExitStateFunction = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            ENTER_STATE_FUNCTION enter_state_function = null,
            UPDATE_FUNCTION update_function = null,
            EXIT_STATE_FUNCTION exit_state_function = null
            ) :
            base( name )
        {
            CanEnterStateFunction = null;
            CanExitStateFunction = null;
            EnterStateFunction = enter_state_function;
            BeginUpdateFunction = null;
            UpdateFunction = update_function;
            EndUpdateFunction = null;
            ExitStateFunction = exit_state_function;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            ENTER_STATE_FUNCTION enter_state_function = null,
            UPDATE_FUNCTION begin_update_function = null,
            UPDATE_FUNCTION update_function = null,
            UPDATE_FUNCTION end_update_function = null,
            EXIT_STATE_FUNCTION exit_state_function = null
            ) :
            base( name )
        {
            CanEnterStateFunction = null;
            CanExitStateFunction = null;
            EnterStateFunction = enter_state_function;
            BeginUpdateFunction = begin_update_function;
            UpdateFunction = update_function;
            EndUpdateFunction = end_update_function;
            ExitStateFunction = exit_state_function;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            CAN_ENTER_STATE_FUNCTION can_enter_state_function = null,
            CAN_EXIT_STATE_FUNCTION can_exit_state_function = null,
            ENTER_STATE_FUNCTION enter_state_function = null,
            UPDATE_FUNCTION update_function = null,
            EXIT_STATE_FUNCTION exit_state_function = null
            ) :
            base( name )
        {
            CanEnterStateFunction = can_enter_state_function;
            CanExitStateFunction = can_exit_state_function;
            EnterStateFunction = enter_state_function;
            BeginUpdateFunction = null;
            UpdateFunction = update_function;
            EndUpdateFunction = null;
            ExitStateFunction = exit_state_function;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            CAN_ENTER_STATE_FUNCTION can_enter_state_function = null,
            CAN_EXIT_STATE_FUNCTION can_exit_state_function = null,
            ENTER_STATE_FUNCTION enter_state_function = null,
            UPDATE_FUNCTION begin_update_function = null,
            UPDATE_FUNCTION update_function = null,
            UPDATE_FUNCTION end_update_function = null,
            EXIT_STATE_FUNCTION exit_state_function = null
            ) :
            base( name )
        {
            CanEnterStateFunction = can_enter_state_function;
            CanExitStateFunction = can_exit_state_function;
            EnterStateFunction = enter_state_function;
            BeginUpdateFunction = begin_update_function;
            UpdateFunction = update_function;
            EndUpdateFunction = end_update_function;
            ExitStateFunction = exit_state_function;
        }

        // -- INQUIRIES

        public override bool CanEnterState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( State != null )
            {
                return State.CanEnterState( old_state, new_state );
            }
            else if ( CanEnterStateFunction != null )
            {
                return CanEnterStateFunction( old_state, new_state );
            }
            else
            {
                return true;
            }
        }

        // ~~

        public override bool CanExitState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( State != null )
            {
                return State.CanExitState( old_state, new_state );
            }
            else if ( CanExitStateFunction != null )
            {
                return CanExitStateFunction( old_state, new_state );
            }
            else
            {
                return true;
            }
        }

        // -- OPERATIONS

        public override void EnterState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( EnterStateFunction != null )
            {
                EnterStateFunction( old_state, new_state );
            }

            if ( State != null )
            {
                State.EnterState( old_state, new_state );
            }
        }

        // ~~

        public override void BeginUpdate(
            )
        {
            if ( State != null )
            {
                State.BeginUpdate();
            }
            else if ( BeginUpdateFunction != null )
            {
                BeginUpdateFunction();
            }
        }

        // ~~

        public override void Update(
            )
        {
            if ( State != null )
            {
                State.Update();
            }
            else if ( UpdateFunction != null )
            {
                UpdateFunction();
            }
        }

        // ~~

        public override void EndUpdate(
            )
        {
            if ( State != null )
            {
                State.EndUpdate();
            }
            else if ( EndUpdateFunction != null )
            {
                EndUpdateFunction();
            }
        }

        // ~~

        public override void ExitState(
            STATE old_state,
            STATE new_state
            )
        {
            if ( State != null )
            {
                State.ExitState( old_state, new_state );
            }

            if ( ExitStateFunction != null )
            {
                ExitStateFunction( old_state, new_state);
            }
        }
    }
}
