// -- IMPORTS

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CORE_MODULE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace GAME_MODULE
{
    public class CONFIGURE_GAME_PANEL : PANEL
    {
        // -- ATTRIBUTES

        public TEXT_MESH
            MusicLabelTextMesh,
            SoundLabelTextMesh,
            LanguageLabelTextMesh,
            LanguageNameTextMesh,
            BackButtonTextMesh;
        public Slider
            MusicSlider,
            SoundSlider;
        public STATE
            ExitState;

        // -- OPERATIONS

        public override void UpdateLanguage(
            LANGUAGE language
            )
        {
            MusicLabelTextMesh.text = language.GetText( "MusicLabel" );
            SoundLabelTextMesh.text = language.GetText( "SoundLabel" );
            LanguageLabelTextMesh.text = language.GetText( "LanguageLabel" );
            LanguageNameTextMesh.text = language.GetText( "LanguageName" );
            BackButtonTextMesh.text = language.GetText( "BackButton" );
        }

        // ~~

        public new void OnEnable(
            )
        {
            base.OnEnable();

            GAME.ShowCursor();

            MusicLabelTextMesh = FindSubEntityByName<TEXT_MESH>( "MusicLabelText" );
            SoundLabelTextMesh = FindSubEntityByName<TEXT_MESH>( "SoundLabelText" );
            LanguageLabelTextMesh = FindSubEntityByName<TEXT_MESH>( "LanguageLabelText" );
            LanguageNameTextMesh = FindSubEntityByName<TEXT_MESH>( "LanguageNameText" );
            BackButtonTextMesh = FindSubEntityByName<TEXT_MESH>( "BackButtonText" );
            MusicSlider = FindSubEntityByName<Slider>( "MusicVolumeSlider" );
            SoundSlider = FindSubEntityByName<Slider>( "SoundVolumeSlider" );

            SoundSlider.value = GAME.VolumeScaleArray[ 0 ];
            MusicSlider.value = GAME.VolumeScaleArray[ 1 ];

            UpdateLanguage( GAME.Instance.Language );
        }

        // ~~

        public void SetSoundVolume(
            float sound_volume
            )
        {
            GAME.VolumeScaleArray[ 0 ] = sound_volume;
        }

        // ~~

        public void SetMusicVolume(
            float music_volume
            )
        {
            GAME.VolumeScaleArray[ 1 ] = music_volume;
        }

        // ~~

        public void SetPriorLanguage(
            )
        {
            int
                language_index;

            language_index = GAME.Instance.LanguageIndex - 1;

            if ( language_index < 0 )
            {
                language_index = GAME.Instance.LanguageList.Count - 1;
            }

            GAME.Instance.SetLanguageIndex( language_index );
        }

        // ~~

        public void SetNextLanguage(
            )
        {
            int
                language_index;

            language_index = GAME.Instance.LanguageIndex + 1;

            if ( language_index >= GAME.Instance.LanguageList.Count )
            {
                language_index = 0;
            }

            GAME.Instance.SetLanguageIndex( language_index );
        }

        // ~~

        public void ExitPanel(
            )
        {
            GAME.Instance.State.ChangeState( ExitState );
        }
    }
}
