// -- TYPES

public static class FLOAT_EXTENSION
{
    // -- INQUIRIES

    public static int GetInteger(
        this float real
        )
    {
        return ( int )real;
    }

    // ~~

    public static string GetText(
        this float real,
        int minimum_fractional_digit_count = 1,
        int maximum_fractional_digit_count = 6,
        char dot_character = '.'
        )
    {
        int
            dot_character_index,
            fractional_digit_count;
        string
            text;

        text = real.ToString( "F" + maximum_fractional_digit_count.ToString() );

        dot_character_index = text.IndexOf( '.' );

        if ( dot_character_index < 0 )
        {
            dot_character_index = text.IndexOf( ',' );
        }

        if ( dot_character_index < 0 )
        {
            dot_character_index = text.Length;

            text += dot_character;
            text += '0';
        }

        fractional_digit_count = text.Length - dot_character_index - 1;

        if ( fractional_digit_count < minimum_fractional_digit_count )
        {
            text += "0000000000000000000000000000000000000000".Substring( 0, minimum_fractional_digit_count - fractional_digit_count );

            fractional_digit_count = minimum_fractional_digit_count;
        }
        else if ( fractional_digit_count > maximum_fractional_digit_count )
        {
            text = text.Substring( 0, dot_character_index + 1 + maximum_fractional_digit_count );

            fractional_digit_count = maximum_fractional_digit_count;
        }

        if ( minimum_fractional_digit_count < maximum_fractional_digit_count )
        {
            while ( fractional_digit_count > minimum_fractional_digit_count
                    && text[ dot_character_index + fractional_digit_count ] == '0' )
            {
                --fractional_digit_count;
            }

            text = text.Substring( 0, dot_character_index + fractional_digit_count + 1 );
        }

        if ( fractional_digit_count == 0 )
        {
            return text.Substring( 0, dot_character_index );
        }
        else if ( text[ dot_character_index ] != dot_character )
        {
            return text.Substring( 0, dot_character_index ) + dot_character + text.Substring( dot_character_index + 1 );
        }
        else
        {
            return text;
        }
    }
}
