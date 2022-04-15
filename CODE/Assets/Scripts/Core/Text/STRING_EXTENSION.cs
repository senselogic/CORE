// -- IMPORTS

using System.Globalization;

// -- TYPES

public static class STRING_EXTENSION
{
    // -- INQUIRIES

    public static string GetLowerCaseText(
        this string text
        )
    {
        char[]
            lower_case_character_array;
        int
            character_index;

        lower_case_character_array = text.ToCharArray();

        for ( character_index = 0;
              character_index < text.Length;
              ++character_index )
        {
            lower_case_character_array[ character_index ] = text[ character_index ].GetLowerCaseCharacter();
        }

        return new string( lower_case_character_array );
    }

    // ~~

    public static string GetUpperCaseText(
        this string text
        )
    {
        char[]
            upper_case_character_array;
        int
            character_index;

        upper_case_character_array = text.ToCharArray();

        for ( character_index = 0;
              character_index < text.Length;
              ++character_index )
        {
            upper_case_character_array[ character_index ] = text[ character_index ].GetUpperCaseCharacter();
        }

        return new string( upper_case_character_array );
    }

    // ~~

    public static string GetTitleCaseText(
        this string text
        )
    {
        char
            character,
            prior_character;
        char[]
            title_case_character_array;
        int
            character_index;

        prior_character = ' ';
        title_case_character_array = text.ToCharArray();

        for ( character_index = 0;
              character_index < text.Length;
              ++character_index )
        {
            character = text[ character_index ];

            if ( prior_character == ' '
                 && character.IsLowerCaseCharacter() )
            {
                title_case_character_array[ character_index ] = character.GetUpperCaseCharacter();
            }

            prior_character = character;
        }

        return new string( title_case_character_array );
    }

    // ~~

    public static string GetSentenceCaseText(
        this string text
        )
    {
        char[]
            capital_case_character_array;

        if ( text.Length > 0
             && !text[ 0 ].IsUpperCaseCharacter( ) )
        {
            capital_case_character_array = text.ToCharArray();
            capital_case_character_array[ 0 ] = text[ 0 ].GetUpperCaseCharacter();

            return new string( capital_case_character_array );
        }
        else
        {
            return text;
        }
    }

    // ~~

    public static bool HasFirstCharacter(
        this string text,
        char first_character
        )
    {
        return
            text.Length > 0
            && text[ 0 ] == first_character;
    }

    // ~~

    public static bool HasFirstCharacter(
        this string text,
        string first_characters
        )
    {
        return
            text.Length > 0
            && first_characters.IndexOf( text[ 0 ] ) >= 0;
    }

    // ~~

    public static bool HasPrefix(
        this string text,
        string prefix
        )
    {
        return text.StartsWith( prefix );
    }

    // ~~

    public static bool HasSuffix(
        this string text,
        string suffix
        )
    {
        return text.EndsWith( suffix );
    }

    // ~~

    public static bool GetBoolean(
        this string text
        )
    {
        return text == "true";
    }

    // ~~

    public static int GetInteger(
        this string text
        )
    {
        return int.Parse( text );
    }

    // ~~

    public static float GetReal(
        this string text
        )
    {
        return float.Parse( text.Replace( ',', '.' ), CultureInfo.InvariantCulture );
    }

    // ~~

    public static string GetTranslatedText(
        this string text,
        string language_code,
        string default_language_code = "en",
        char translation_separator = '¨'
        )
    {
        int
            translated_text_index;
        string
            translated_text;
        string[]
            translated_text_array;

        if ( text == "" )
        {
            return "";
        }
        else
        {
            translated_text_array = text.Split( translation_separator );

            if ( language_code != default_language_code )
            {
                for ( translated_text_index = 1;
                      translated_text_index < translated_text_array.Length;
                      ++translated_text_index )
                {
                    translated_text = translated_text_array[ translated_text_index ];

                    if ( translated_text[ 0 ] == language_code[ 0 ]
                         && translated_text[ 1 ] == language_code[ 1 ] )
                    {
                        return translated_text.Substring( 3 );
                    }
                }
            }

            return translated_text_array[ 0 ];
        }
    }
}
