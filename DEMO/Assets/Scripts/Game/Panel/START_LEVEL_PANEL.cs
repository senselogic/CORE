// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using CORE_MODULE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace GAME_MODULE
{
    public class START_LEVEL_PANEL : PANEL
    {
        // -- ATTRIBUTES

        public AudioClip
            CounterAudioClip,
            MusicAudioClip;
        public Transform
            CounterTransform;
        public TEXT_MESH
            CounterTextMesh;
        public float
            CounterTime;
        public int
            Counter;
        public TEXT
            CounterText;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            GAME.HideCursor();

            CounterTransform = FindSubTransformByName( "Counter" );
            CounterTextMesh = FindSubEntityByName<TEXT_MESH>( "CounterText" );

            CounterTime = 5.999f;
            Counter = -1;
            CounterText = new TEXT();

            GAME.Instance.PlayMusicAudioClip( MusicAudioClip, SOUND_MASK.Loop | SOUND_MASK.Exclusive );
        }

        // ~~

        public void Update(
            )
        {
            float
                scaling;
            int
                counter;

            base.BeginUpdate();

            CounterTime -= TimeStep;

            if ( CounterTime < 0.0f )
            {
                CounterTime = 0.0f;
            }

            counter = Mathf.FloorToInt( CounterTime );

            if ( Counter != counter )
            {
                Counter = counter;

                if ( Counter >= 1 )
                {
                    CounterText.Clear();
                    CounterText.AddFormattedInteger( Counter );
                    CounterTextMesh.text = CounterText.GetString();

                    GAME.Instance.PlaySoundAudioClip( CounterAudioClip, SOUND_MASK.Exclusive, 0.5f );
                }
                else
                {
                    CounterTextMesh.text = GAME.Instance.Language.GetText( "GoLabel" );

                    GAME.Instance.PlaySoundAudioClip( CounterAudioClip, SOUND_MASK.Exclusive, 1.0f, 1.5f );
                }
            }

            if ( Counter >= 1 )
            {
                scaling = CounterTime - Mathf.Floor( CounterTime );
                scaling *= scaling * scaling * scaling;
            }
            else
            {
                scaling = 1.0f;
            }

            CounterTransform.localScale = new Vector3( scaling, scaling, 1.0f );

            CounterTextMesh.color = new Color32( 255, 255, 255, ( byte )( scaling * 255.0f ) );

            if ( CounterTime == 0.0 )
            {
                GAME.Instance.State.ChangeState( GAME.Instance.PlayLevelState );
            }

            base.EndUpdate();
        }
    }
}
