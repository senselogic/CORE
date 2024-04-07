// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "", "*" ) ]
    #endif

    public class INPUT : ENTITY
    {
        // -- ATTRIBUTES

        public static float
            MoveLaterallyAxis,
            MoveFrontallyAxis,
            AimHorizontallyAxis,
            AimVerticallyAxis;
        public static bool
            ShootButtonIsPressed;
        public static float
            NavigateHorizontallyAxis,
            NavigateVerticallyAxis;
        public static bool
            PauseButtonIsPressed,
            SubmitButtonIsPressed,
            CancelButtonIsPressed;

        // -- INQUIRIES

        public static void Log(
            )
        {
            Debug.Log(
                "Input : "
                + MoveLaterallyAxis
                + " "
                + MoveFrontallyAxis
                + " / "
                + AimHorizontallyAxis
                + " "
                + AimVerticallyAxis
                + " / "
                + ShootButtonIsPressed
                + " "
                + NavigateHorizontallyAxis
                + " / "
                + NavigateVerticallyAxis
                + " / "
                + PauseButtonIsPressed
                + " / "
                + SubmitButtonIsPressed
                + " / "
                + CancelButtonIsPressed
                );
        }

        // -- OPERATIONS

        public void Update(
            )
        {
            MoveLaterallyAxis = Input.GetAxis( "MoveLaterally" );
            MoveFrontallyAxis = Input.GetAxis( "MoveFrontally" );
            AimHorizontallyAxis = Input.GetAxis( "AimHorizontally" );
            AimVerticallyAxis = Input.GetAxis( "AimVertically" );
            ShootButtonIsPressed = ( Input.GetAxis( "Shoot" ) > 0.0f );

            NavigateHorizontallyAxis = Input.GetAxis( "Horizontal" );
            NavigateVerticallyAxis = Input.GetAxis( "Vertical" );
            PauseButtonIsPressed = Input.GetButtonDown( "Pause" );
            SubmitButtonIsPressed = Input.GetButtonDown( "Submit" );
            CancelButtonIsPressed = Input.GetButtonDown( "Cancel" ) || Input.GetKeyDown( KeyCode.Escape );
        }
    }
}
