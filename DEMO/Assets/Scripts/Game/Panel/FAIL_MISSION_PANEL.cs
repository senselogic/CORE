// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using CORE_MODULE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace GAME_MODULE
{
    public class FAIL_MISSION_PANEL : PANEL
    {
        // -- ATTRIBUTES

        public AudioClip
            MusicAudioClip;
        public TEXT_MESH
            TitleLabelTextMesh,
            RestartButtonTextMesh,
            ExitButtonTextMesh;

        // -- OPERATIONS

        public override void UpdateLanguage(
            LANGUAGE language
            )
        {
            TitleLabelTextMesh.text = language.GetText( "FailMissionTitle" );
            RestartButtonTextMesh.text = language.GetText( "RestartButton" );
            ExitButtonTextMesh.text = language.GetText( "ExitButton" );
        }

        // ~~

        public new void OnEnable(
            )
        {
            base.OnEnable();

            GAME.ShowCursor();

            TitleLabelTextMesh = FindSubEntityByName<TEXT_MESH>( "TitleLabelText" );
            RestartButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "RestartButtonText" );
            ExitButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "ExitButtonText" );

            UpdateLanguage( GAME.Instance.Language );

            GAME.Instance.PlayMusicAudioClip( MusicAudioClip, SOUND_MASK.Exclusive );
        }

        // ~~

        public void RestartLevel(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.LoadLevelState );
        }

        // ~~

        public void ExitLevel(
            )
        {
            GAME.Instance.State.ChangeState( GAME.Instance.LoadGameState );
        }
    }
}
