// -- IMPORTS

using CORE;

// -- TYPES

namespace CORE
{
    public static class INT_EXTENSION
    {
        // -- INQUIRIES

        public static float GetReal(
            this int integer
            )
        {
            return ( int )integer;
        }

        // ~~

        public static string GetText(
            this int integer,
            int minimum_digit_count = 1
            )
        {
            int
                digit_count;
            string
                text;

            text = integer.ToString();

            digit_count = text.Length;

            if ( integer < 0 )
            {
                --digit_count;

                if ( digit_count < minimum_digit_count )
                {
                    text = "-" + "0000000000000000000000000000000000000000".Substring( 0, minimum_digit_count - digit_count ) + text.Substring( 1 );
                }
            }
            else
            {
                if ( digit_count < minimum_digit_count )
                {
                    text = "0000000000000000000000000000000000000000".Substring( 0, minimum_digit_count - digit_count ) + text;
                }
            }

            return text;
        }
    }
}
