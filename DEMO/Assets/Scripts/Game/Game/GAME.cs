// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "", "LEVEL *" ) ]
    #endif

    public class GAME : APPLICATION
    {
        // -- ATTRIBUTES

        public List<LANGUAGE>
            LanguageList;
        public int
            LanguageIndex;
        public LANGUAGE
            Language;
        public UPDATE_LANGUAGE_FUNCTION
            UpdateLanguageFunction;
        public GAME_PANEL
            Panel;
        public START_GAME_PANEL
            StartGamePanel;
        public CONFIGURE_GAME_PANEL
            ConfigureGamePanel;
        public LOAD_LEVEL_PANEL
            LoadLevelPanel;
        public START_LEVEL_PANEL
            StartLevelPanel;
        public PAUSE_LEVEL_PANEL
            PauseLevelPanel;
        public COMPLETE_MISSION_PANEL
            CompleteMissionPanel;
        public FAIL_MISSION_PANEL
            FailMissionPanel;
        public GAME_LOAD_GAME_STATE
            LoadGameState;
        public GAME_START_GAME_STATE
            StartGameState;
        public GAME_CONFIGURE_GAME_STATE
            ConfigureGameState;
        public GAME_LOAD_LEVEL_STATE
            LoadLevelState;
        public GAME_START_LEVEL_STATE
            StartLevelState;
        public GAME_PLAY_LEVEL_STATE
            PlayLevelState;
        public GAME_PAUSE_LEVEL_STATE
            PauseLevelState;
        public GAME_COMPLETE_MISSION_STATE
            CompleteMissionState;
        public GAME_FAIL_MISSION_STATE
            FailMissionState;
        public STATE
            State;
        public static GAME
            Instance;

        // -- OPERATIONS

        public void SetLanguageIndex(
            int language_index
            )
        {
            LanguageIndex = language_index;
            Language = LanguageList[ language_index ];

            if ( UpdateLanguageFunction != null )
            {
                UpdateLanguageFunction( Language );
            }
        }

        // ~~

        public new void OnEnable(
            )
        {
            if ( Instance == null )
            {
                Instance = this;
                SetPermanent();

                base.OnEnable();

                IgnoreLayerCollision( ( int )LAYER_INDEX.Trigger );

                SOUND.InitializePool();

                VolumeScaleArray[ 0 ] = 1.0f;
                VolumeScaleArray[ 1 ] = 0.25f;

                LanguageList = new List<LANGUAGE>();
                LanguageList.Add( new ENGLISH_LANGUAGE() );
                LanguageList.Add( new FRENCH_LANGUAGE() );

                SetLanguageIndex( 0 );

                Panel = FindSubEntityByType<GAME_PANEL>();
                StartGamePanel = FindSubEntityByType<START_GAME_PANEL>();
                ConfigureGamePanel = FindSubEntityByType<CONFIGURE_GAME_PANEL>();
                LoadLevelPanel = FindSubEntityByType<LOAD_LEVEL_PANEL>();
                StartLevelPanel = FindSubEntityByType<START_LEVEL_PANEL>();
                PauseLevelPanel = FindSubEntityByType<PAUSE_LEVEL_PANEL>();
                CompleteMissionPanel = FindSubEntityByType<COMPLETE_MISSION_PANEL>();
                FailMissionPanel = FindSubEntityByType<FAIL_MISSION_PANEL>();

                LoadGameState = new GAME_LOAD_GAME_STATE( "LoadGameState", this );
                StartGameState = new GAME_START_GAME_STATE( "StartGameState", this );
                ConfigureGameState = new GAME_CONFIGURE_GAME_STATE( "ConfigureGameState", this );
                LoadLevelState = new GAME_LOAD_LEVEL_STATE( "LoadLevelState", this );
                StartLevelState = new GAME_START_LEVEL_STATE( "StartLevelState", this );
                PlayLevelState = new GAME_PLAY_LEVEL_STATE( "PlayLevelState", this );
                PauseLevelState = new GAME_PAUSE_LEVEL_STATE( "PauseLevelState", this );
                CompleteMissionState = new GAME_COMPLETE_MISSION_STATE( "CompleteMissionState", this );
                FailMissionState = new GAME_FAIL_MISSION_STATE( "FailMissionState", this );

                State = new STATE();
                State.ChangeState( LoadGameState );
            }
            else if ( Instance != this )
            {
                Destroy( gameObject );
            }
        }

        // ~~

        public new void Update(
            )
        {
            base.Update();

            BeginUpdate();
            State.Update();
            EndUpdate();
        }

        // ~~

        public void PlaySoundAudioClip(
            AudioClip audio_clip,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            float volume = 1.0f,
            float pitch = 1.0f
            )
        {
            Panel.SoundMixer.PlayAudioClip( audio_clip, sound_mask, 0, volume, pitch, 0.0f );
        }

        // ~~

        public void PlayMusicAudioClip(
            AudioClip audio_clip,
            SOUND_MASK sound_mask = SOUND_MASK.None,
            bool music_is_restarted = true
            )
        {
            if ( music_is_restarted
                 || !Panel.MusicSoundMixer.IsPlayingAudioClip( audio_clip ) )
            {
                Panel.MusicSoundMixer.PlayAudioClip( audio_clip, sound_mask, 1, 1.0f, 1.0f, 0.5f );
            }
        }

        // -- STATES

        public void EnterLoadGameState(
            STATE old_state,
            STATE new_state
            )
        {
            LoadLevelPanel.LevelName = "StartLevel";
            LoadLevelPanel.ExitState = StartGameState;
            LoadLevelPanel.SetActive( true );
        }

        // ~~

        public void ExitLoadGameState(
            STATE old_state,
            STATE new_state
            )
        {
            LoadLevelPanel.SetActive( false );
        }

        // ~~

        public void EnterStartGameState(
            STATE old_state,
            STATE new_state
            )
        {
            StartGamePanel.SetActive( true );
        }

        // ~~

        public void ExitStartGameState(
            STATE old_state,
            STATE new_state
            )
        {
            StartGamePanel.SetActive( false );
        }

        // ~~

        public void EnterConfigureGameState(
            STATE old_state,
            STATE new_state
            )
        {
            ConfigureGamePanel.ExitState = old_state;
            ConfigureGamePanel.SetActive( true );
        }

        // ~~

        public void ExitConfigureGameState(
            STATE old_state,
            STATE new_state
            )
        {
            ConfigureGamePanel.SetActive( false );
        }

        // ~~

        public void EnterLoadLevelState(
            STATE old_state,
            STATE new_state
            )
        {
            LoadLevelPanel.LevelName = "FirstLevel";
            LoadLevelPanel.ExitState = StartLevelState;
            LoadLevelPanel.SetActive( true );
        }

        // ~~

        public void ExitLoadLevelState(
            STATE old_state,
            STATE new_state
            )
        {
            LoadLevelPanel.SetActive( false );
        }

        // ~~

        public void EnterStartLevelState(
            STATE old_state,
            STATE new_state
            )
        {
            StartLevelPanel.SetActive( true );
        }

        // ~~

        public void ExitStartLevelState(
            STATE old_state,
            STATE new_state
            )
        {
            StartLevelPanel.SetActive( false );
            LEVEL.Instance.Activate();
        }

        // ~~

        public void EnterPlayLevelState(
            STATE old_state,
            STATE new_state
            )
        {
        }

        // ~~

        public void ExitPlayLevelState(
            STATE old_state,
            STATE new_state
            )
        {
        }

        // ~~

        public void EnterPauseLevelState(
            STATE old_state,
            STATE new_state
            )
        {
            PauseLevelPanel.SetActive( true );
        }

        // ~~

        public void ExitPauseLevelState(
            STATE old_state,
            STATE new_state
            )
        {
            PauseLevelPanel.SetActive( false );
        }

        // ~~

        public void EnterCompleteMissionState(
            STATE old_state,
            STATE new_state
            )
        {
            CompleteMissionPanel.SetActive( true );
        }

        // ~~

        public void ExitCompleteMissionState(
            STATE old_state,
            STATE new_state
            )
        {
            CompleteMissionPanel.SetActive( false );
        }

        // ~~

        public void EnterFailMissionState(
            STATE old_state,
            STATE new_state
            )
        {
            FailMissionPanel.SetActive( true );
        }

        // ~~

        public void ExitFailMissionState(
            STATE old_state,
            STATE new_state
            )
        {
            FailMissionPanel.SetActive( false );
        }
    }
}
