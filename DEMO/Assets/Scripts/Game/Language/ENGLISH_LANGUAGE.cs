// -- IMPORTS

using GAME_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class ENGLISH_LANGUAGE : LANGUAGE
    {
        // -- CONSTRUCTORS

        public ENGLISH_LANGUAGE(
            ) : base()
        {
            Name = "English";
            DotCharacter = '.';
            TranslationDictionary[ "CompleteMissionTitle" ] = new TRANSLATION( "Mission Complete" );
            TranslationDictionary[ "ConfigureGameTitle" ] = new TRANSLATION( "Options" );
            TranslationDictionary[ "FailMissionTitle" ] = new TRANSLATION( "Mission Failed" );
            TranslationDictionary[ "FirstLevelTitle" ] = new TRANSLATION( "Level 1" );
            TranslationDictionary[ "PauseLevelTitle" ] = new TRANSLATION( "Pause" );
            TranslationDictionary[ "StartGameTitle" ] = new TRANSLATION( "Game" );
            TranslationDictionary[ "StartLevelTitle" ] = new TRANSLATION( "Game" );
            TranslationDictionary[ "BackButton" ] = new TRANSLATION( "Back" );
            TranslationDictionary[ "ConfigureButton" ] = new TRANSLATION( "Options" );
            TranslationDictionary[ "ExitButton" ] = new TRANSLATION( "Exit" );
            TranslationDictionary[ "QuitButton" ] = new TRANSLATION( "Quit" );
            TranslationDictionary[ "RestartButton" ] = new TRANSLATION( "Restart" );
            TranslationDictionary[ "ResumeButton" ] = new TRANSLATION( "Resume" );
            TranslationDictionary[ "StartButton" ] = new TRANSLATION( "Start" );
            TranslationDictionary[ "GoLabel" ] = new TRANSLATION( "GO" );
            TranslationDictionary[ "LanguageLabel" ] = new TRANSLATION( "Language" );
            TranslationDictionary[ "MusicLabel" ] = new TRANSLATION( "Music" );
            TranslationDictionary[ "SoundLabel" ] = new TRANSLATION( "Sound" );
            TranslationDictionary[ "LanguageName" ] = new TRANSLATION( "English" );
        }

        // -- INQUIRIES

        public override PLURALITY GetCardinalPlurality(
            TRANSLATION translation
            )
        {
            return translation.GetEnglishCardinalPlurality();
        }

        // ~~

        public override PLURALITY GetOrdinalPlurality(
            TRANSLATION translation
            )
        {
            return translation.GetEnglishOrdinalPlurality();
        }

        // ~~

    }
}
