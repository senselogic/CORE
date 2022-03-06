// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class DYNAMIC_STATE : STATE
    {
        // -- TYPES

        public delegate bool CAN_ENTER_STATE_DELEGATE(
            STATE old_state,
            STATE new_state
            );

        // ~~

        public delegate bool CAN_EXIT_STATE_DELEGATE(
            STATE old_state,
            STATE new_state
            );

        // ~~

        public delegate void ENTER_STATE_DELEGATE(
            STATE old_state,
            STATE new_state
            );

        // ~~

        public delegate void UPDATE_DELEGATE(
            );

        // ~~

        public delegate void EXIT_STATE_DELEGATE(
            STATE old_state,
            STATE new_state
            );

        // -- ATTRIBUTES

        public CAN_ENTER_STATE_DELEGATE
            CanEnterStateDelegate;
        public CAN_EXIT_STATE_DELEGATE
            CanExitStateDelegate;
        public ENTER_STATE_DELEGATE
            EnterStateDelegate;
        public UPDATE_DELEGATE
            BeginUpdateDelegate,
            UpdateDelegate,
            EndUpdateDelegate;
        public EXIT_STATE_DELEGATE
            ExitStateDelegate;

        // -- CONSTRUCTORS

        public DYNAMIC_STATE(
            ) :
            base()
        {
            State = null;
            CanEnterStateDelegate = null;
            CanExitStateDelegate = null;
            EnterStateDelegate = null;
            BeginUpdateDelegate = null;
            UpdateDelegate = null;
            EndUpdateDelegate = null;
            ExitStateDelegate = null;
        }

        // ~~

        public DYNAMIC_STATE(
            STATE state
            ) :
            base( state )
        {
            CanEnterStateDelegate = null;
            CanExitStateDelegate = null;
            EnterStateDelegate = null;
            BeginUpdateDelegate = null;
            UpdateDelegate = null;
            EndUpdateDelegate = null;
            ExitStateDelegate = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name
            ) :
            base( name )
        {
            CanEnterStateDelegate = null;
            CanExitStateDelegate = null;
            EnterStateDelegate = null;
            BeginUpdateDelegate = null;
            UpdateDelegate = null;
            EndUpdateDelegate = null;
            ExitStateDelegate = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            UPDATE_DELEGATE update_delegate = null
            ) :
            base( name )
        {
            CanEnterStateDelegate = null;
            CanExitStateDelegate = null;
            EnterStateDelegate = null;
            BeginUpdateDelegate = null;
            UpdateDelegate = update_delegate;
            EndUpdateDelegate = null;
            ExitStateDelegate = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            ENTER_STATE_DELEGATE enter_state_delegate = null,
            UPDATE_DELEGATE update_delegate = null
            ) :
            base( name )
        {
            CanEnterStateDelegate = null;
            CanExitStateDelegate = null;
            EnterStateDelegate = enter_state_delegate;
            BeginUpdateDelegate = null;
            UpdateDelegate = update_delegate;
            EndUpdateDelegate = null;
            ExitStateDelegate = null;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            ENTER_STATE_DELEGATE enter_state_delegate = null,
            UPDATE_DELEGATE update_delegate = null,
            EXIT_STATE_DELEGATE exit_state_delegate = null
            ) :
            base( name )
        {
            CanEnterStateDelegate = null;
            CanExitStateDelegate = null;
            EnterStateDelegate = enter_state_delegate;
            BeginUpdateDelegate = null;
            UpdateDelegate = update_delegate;
            EndUpdateDelegate = null;
            ExitStateDelegate = exit_state_delegate;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            ENTER_STATE_DELEGATE enter_state_delegate = null,
            UPDATE_DELEGATE begin_update_delegate = null,
            UPDATE_DELEGATE update_delegate = null,
            UPDATE_DELEGATE end_update_delegate = null,
            EXIT_STATE_DELEGATE exit_state_delegate = null
            ) :
            base( name )
        {
            CanEnterStateDelegate = null;
            CanExitStateDelegate = null;
            EnterStateDelegate = enter_state_delegate;
            BeginUpdateDelegate = begin_update_delegate;
            UpdateDelegate = update_delegate;
            EndUpdateDelegate = end_update_delegate;
            ExitStateDelegate = exit_state_delegate;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            CAN_ENTER_STATE_DELEGATE can_enter_state_delegate = null,
            CAN_EXIT_STATE_DELEGATE can_exit_state_delegate = null,
            ENTER_STATE_DELEGATE enter_state_delegate = null,
            UPDATE_DELEGATE update_delegate = null,
            EXIT_STATE_DELEGATE exit_state_delegate = null
            ) :
            base( name )
        {
            CanEnterStateDelegate = can_enter_state_delegate;
            CanExitStateDelegate = can_exit_state_delegate;
            EnterStateDelegate = enter_state_delegate;
            BeginUpdateDelegate = null;
            UpdateDelegate = update_delegate;
            EndUpdateDelegate = null;
            ExitStateDelegate = exit_state_delegate;
        }

        // ~~

        public DYNAMIC_STATE(
            string name,
            CAN_ENTER_STATE_DELEGATE can_enter_state_delegate = null,
            CAN_EXIT_STATE_DELEGATE can_exit_state_delegate = null,
            ENTER_STATE_DELEGATE enter_state_delegate = null,
            UPDATE_DELEGATE begin_update_delegate = null,
            UPDATE_DELEGATE update_delegate = null,
            UPDATE_DELEGATE end_update_delegate = null,
            EXIT_STATE_DELEGATE exit_state_delegate = null
            ) :
            base( name )
        {
            CanEnterStateDelegate = can_enter_state_delegate;
            CanExitStateDelegate = can_exit_state_delegate;
            EnterStateDelegate = enter_state_delegate;
            BeginUpdateDelegate = begin_update_delegate;
            UpdateDelegate = update_delegate;
            EndUpdateDelegate = end_update_delegate;
            ExitStateDelegate = exit_state_delegate;
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
            else if ( CanEnterStateDelegate != null )
            {
                return CanEnterStateDelegate( old_state, new_state );
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
            else if ( CanExitStateDelegate != null )
            {
                return CanExitStateDelegate( old_state, new_state );
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
            if ( EnterStateDelegate != null )
            {
                EnterStateDelegate( old_state, new_state );
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
            else if ( BeginUpdateDelegate != null )
            {
                BeginUpdateDelegate();
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
            else if ( UpdateDelegate != null )
            {
                UpdateDelegate();
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
            else if ( EndUpdateDelegate != null )
            {
                EndUpdateDelegate();
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

            if ( ExitStateDelegate != null )
            {
                ExitStateDelegate( old_state, new_state);
            }
        }
    }
}
