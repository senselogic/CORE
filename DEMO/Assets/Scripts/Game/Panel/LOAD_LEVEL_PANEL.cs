// -- IMPORTS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CORE_MODULE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace GAME_MODULE
{
    public class LOAD_LEVEL_PANEL : PANEL
    {
        // -- ATTRIBUTES

        public TEXT_MESH
            TitleLabelTextMesh;
        public string
            LevelName;
        public Transform
            ProgressBarLevelTransform;
        public AsyncOperation
            AsyncOperation_;
        public int
            StateIndex;
        public float
            Time;
        public STATE
            ExitState;

        // -- OPERATIONS

        public override void UpdateLanguage(
            LANGUAGE language
            )
        {
            TitleLabelTextMesh.text = language.GetText( LevelName + "Title" );
        }

        // ~~

        public new void OnEnable(
            )
        {
            base.OnEnable();

            GAME.HideCursor();

            TitleLabelTextMesh = FindSubEntityByName<TEXT_MESH>( "TitleLabelText" );
            ProgressBarLevelTransform = FindSubTransformByName( "ProgressBarLevel" );

            UpdateLanguage( GAME.Instance.Language );

            POOL.ReleaseAllObjects();

            AsyncOperation_ = SceneManager.LoadSceneAsync( LevelName );
            AsyncOperation_.allowSceneActivation = false;

            StateIndex = 0;
            Time = 0.0f;
        }

        // ~~

        public void UpdateProgressBar(
            float progress
            )
        {
            ProgressBarLevelTransform.localScale = new Vector3( progress, 1.0f, 1.0f );
        }

        // ~~

        public void Update(
            )
        {
            base.BeginUpdate();

            if ( AsyncOperation_ != null )
            {
                Time += TimeStep;

                if ( StateIndex == 0 )
                {
                    UpdateProgressBar( AsyncOperation_.progress );

                    if ( AsyncOperation_.progress >= 0.9f )
                    {
                        UpdateProgressBar( 1.0f );
                        StateIndex = 1;
                    }
                }
                else if ( StateIndex == 1 )
                {
                    if ( Time >= 1.0f )
                    {
                        AsyncOperation_.allowSceneActivation = true;
                        StateIndex = 2;
                    }
                }
                else if ( StateIndex == 2 )
                {
                    if ( AsyncOperation_.isDone )
                    {
                        StateIndex = 3;
                    }
                }
                else
                {
                    AsyncOperation_ = null;

                    GAME.Instance.State.ChangeState( ExitState );
                }
            }

            base.EndUpdate();
        }
    }
}
