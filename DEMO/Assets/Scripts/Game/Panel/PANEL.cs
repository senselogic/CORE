// -- IMPORTS


using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class PANEL : TEMPORAL_ENTITY
    {
        // -- ATTRIBUTES

        public UPDATE_LANGUAGE_FUNCTION
            UpdateLanguageFunction;

        // -- OPERATIONS

        public virtual void UpdateLanguage(
            LANGUAGE language
            )
        {
        }

        // ~~

        public void OnEnable(
            )
        {
            TimeScaleIndex = 1;

            if ( UpdateLanguageFunction == null )
            {
                UpdateLanguageFunction = new UPDATE_LANGUAGE_FUNCTION( UpdateLanguage );
                GAME.Instance.UpdateLanguageFunction += UpdateLanguageFunction;
            }
        }
    }
}
