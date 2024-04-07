// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using CORE_MODULE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace GAME_MODULE
{
    public class PAUSE_LEVEL_PANEL : PANEL
    {
        // -- ATTRIBUTES

        public TEXT_MESH
            TitleLabelTextMesh,
            ResumeButtonTextMesh,
            RestartButtonTextMesh,
            ConfigureButtonTextMesh,
            QuitButtonTextMesh;

        // -- OPERATIONS

        public override void UpdateLanguage(
            LANGUAGE language
            )
        {
            TitleLabelTextMesh.text = language.GetText( "PauseLevelTitle" );
            ResumeButtonTextMesh.text = language.GetText( "ResumeButton" );
            RestartButtonTextMesh.text = language.GetText( "RestartButton" );
            ConfigureButtonTextMesh.text = language.GetText( "ConfigureButton" );
            QuitButtonTextMesh.text = language.GetText( "QuitButton" );
        }

        // ~~

        public new void OnEnable(
            )
        {
            base.OnEnable();

            GAME.ShowCursor();

            TitleLabelTextMesh = FindSubEntityByName<TEXT_MESH>( "TitleLabelText" );
            ResumeButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "ResumeButtonText" );
            RestartButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "RestartButtonText" );
            ConfigureButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "ConfigureButtonText" );
            QuitButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "QuitButtonText" );

            UpdateLanguage( GAME.Instance.Language );

            GAME.Pause();
        }

        // ~~

        public void Update(
            )
        {
            if ( INPUT.PauseButtonIsPressed
                 || INPUT.CancelButtonIsPressed )
            {
                ResumeLevel();
            }
        }

        // ~~

        public void ResumeLevel(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.PlayLevelState );
            GAME.HideCursor();
            GAME.Resume();
        }

        // ~~

        public void RestartLevel(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.LoadLevelState );
            GAME.Resume();
        }

        // ~~

        public void ConfigureGame(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.ConfigureGameState );
        }

        // ~~

        public void QuitLevel(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.LoadGameState );
            GAME.Resume();
        }
    }
}
