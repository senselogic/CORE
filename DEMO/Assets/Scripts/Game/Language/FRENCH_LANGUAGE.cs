// -- IMPORTS

using GAME_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class FRENCH_LANGUAGE : LANGUAGE
    {
        // -- CONSTRUCTORS

        public FRENCH_LANGUAGE(
            ) : base()
        {
            Name = "French";
            DotCharacter = ',';
            TranslationDictionary[ "CompleteMissionTitle" ] = new TRANSLATION( "Mission accomplie" );
            TranslationDictionary[ "ConfigureGameTitle" ] = new TRANSLATION( "Options" );
            TranslationDictionary[ "FailMissionTitle" ] = new TRANSLATION( "Echec de la mission" );
            TranslationDictionary[ "FirstLevelTitle" ] = new TRANSLATION( "Niveau 1" );
            TranslationDictionary[ "PauseLevelTitle" ] = new TRANSLATION( "Pause" );
            TranslationDictionary[ "StartGameTitle" ] = new TRANSLATION( "Jeu" );
            TranslationDictionary[ "StartLevelTitle" ] = new TRANSLATION( "Jeu" );
            TranslationDictionary[ "BackButton" ] = new TRANSLATION( "Retour" );
            TranslationDictionary[ "ConfigureButton" ] = new TRANSLATION( "Options" );
            TranslationDictionary[ "ExitButton" ] = new TRANSLATION( "Sortir" );
            TranslationDictionary[ "QuitButton" ] = new TRANSLATION( "Quitter" );
            TranslationDictionary[ "RestartButton" ] = new TRANSLATION( "Redémarrer" );
            TranslationDictionary[ "ResumeButton" ] = new TRANSLATION( "Reprendre" );
            TranslationDictionary[ "StartButton" ] = new TRANSLATION( "Démarrer" );
            TranslationDictionary[ "GoLabel" ] = new TRANSLATION( "GO" );
            TranslationDictionary[ "LanguageLabel" ] = new TRANSLATION( "Langue" );
            TranslationDictionary[ "MusicLabel" ] = new TRANSLATION( "Musique" );
            TranslationDictionary[ "SoundLabel" ] = new TRANSLATION( "Son" );
            TranslationDictionary[ "LanguageName" ] = new TRANSLATION( "Français" );
        }

        // -- INQUIRIES

        public override PLURALITY GetCardinalPlurality(
            TRANSLATION translation
            )
        {
            return translation.GetFrenchCardinalPlurality();
        }

        // ~~

        public override PLURALITY GetOrdinalPlurality(
            TRANSLATION translation
            )
        {
            return translation.GetFrenchOrdinalPlurality();
        }

        // ~~

    }
}
