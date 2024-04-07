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
    public class GAME_PANEL : PANEL
    {
        // -- ATTRIBUTES

        public SOUND_MIXER
            SoundMixer,
            MusicSoundMixer;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            SoundMixer = new SOUND_MIXER( gameObject, 0, 2 );
            MusicSoundMixer = new SOUND_MIXER( gameObject, 2, 2 );
        }

        // ~~

        public void Update(
            )
        {
            BeginUpdate();

            SoundMixer.Update( TimeStep );
            MusicSoundMixer.Update( TimeStep );

            EndUpdate();
        }
    }
}
