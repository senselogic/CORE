// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using CORE_MODULE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace GAME_MODULE
{
    public class START_GAME_PANEL : PANEL
    {
        // -- ATTRIBUTES

        public AudioClip
            MusicAudioClip;
        public TEXT_MESH
            TitleLabelTextMesh,
            StartButtonTextMesh,
            ConfigureButtonTextMesh,
            ExitButtonTextMesh;

        // -- OPERATIONS

        public override void UpdateLanguage(
            LANGUAGE language
            )
        {
            TitleLabelTextMesh.text = language.GetText( "StartGameTitle" );
            StartButtonTextMesh.text = language.GetText( "StartButton" );
            ConfigureButtonTextMesh.text = language.GetText( "ConfigureButton" );
            ExitButtonTextMesh.text = language.GetText( "ExitButton" );
        }

        // ~~

        public new void OnEnable(
            )
        {
            base.OnEnable();

            GAME.ShowCursor();

            TitleLabelTextMesh = FindSubEntityByName<TEXT_MESH>( "TitleLabelText" );
            StartButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "StartButtonText" );
            ConfigureButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "ConfigureButtonText" );
            ExitButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "ExitButtonText" );

            UpdateLanguage( GAME.Instance.Language );

            GAME.Instance.PlayMusicAudioClip( MusicAudioClip, SOUND_MASK.Loop | SOUND_MASK.Exclusive, false );
        }

        // ~~

        public void StartLevel(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.LoadLevelState );
        }

        // ~~

        public void ConfigureGame(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.ConfigureGameState );
        }

        // ~~

        public void ExitGame(
            )
        {
            GAME.Exit();
        }
    }
}
