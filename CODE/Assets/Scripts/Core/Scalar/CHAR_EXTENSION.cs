// -- IMPORTS

using CORE;

// -- TYPES

namespace CORE
{
    public static class CHAR_EXTENSION
    {
        // -- INQUIRIES

        public static bool IsLowerCaseCharacter(
            this char character
            )
        {
            return
                ( character >= 'a' && character <= 'z' )
                || character == 'à'
                || character == 'â'
                || character == 'é'
                || character == 'è'
                || character == 'ê'
                || character == 'ë'
                || character == 'î'
                || character == 'ï'
                || character == 'ô'
                || character == 'ö'
                || character == 'û'
                || character == 'ü'
                || character == 'ç'
                || character == 'ñ';
        }

        // ~~

        public static bool IsUpperCaseCharacter(
            this char character
            )
        {
            return
                ( character >= 'A' && character <= 'Z' )
                || character == 'À'
                || character == 'Â'
                || character == 'É'
                || character == 'È'
                || character == 'Ê'
                || character == 'Ë'
                || character == 'Î'
                || character == 'Ï'
                || character == 'Ô'
                || character == 'Ö'
                || character == 'Û'
                || character == 'Ü'
                || character == 'Ñ';
        }

        // ~~

        public static char GetLowerCaseCharacter(
            this char character
            )
        {
            if ( character >= 'A' && character <= 'Z' )
            {
                return ( char )( ( int )character + 32 );
            }
            else
            {
                switch ( character )
                {
                    case 'À' : return 'à';
                    case 'Â' : return 'â';
                    case 'É' : return 'é';
                    case 'È' : return 'è';
                    case 'Ê' : return 'ê';
                    case 'Ë' : return 'ë';
                    case 'Î' : return 'î';
                    case 'Ï' : return 'ï';
                    case 'Ô' : return 'ô';
                    case 'Ö' : return 'ö';
                    case 'Û' : return 'û';
                    case 'Ü' : return 'ü';
                    case 'Ñ' : return 'ñ';

                    default : return character;
                }
            }
        }

        // ~~

        public static char GetUpperCaseCharacter(
            this char character
            )
        {
            if ( character >= 'a' && character <= 'z' )
            {
                return ( char )( ( int )character - 32 );
            }
            else
            {
                switch ( character )
                {
                    case 'à' : return 'À';
                    case 'â' : return 'Â';
                    case 'é' : return 'É';
                    case 'è' : return 'È';
                    case 'ê' : return 'Ê';
                    case 'ë' : return 'Ë';
                    case 'î' : return 'Î';
                    case 'ï' : return 'Ï';
                    case 'ô' : return 'Ô';
                    case 'ö' : return 'Ö';
                    case 'û' : return 'Û';
                    case 'ü' : return 'Ü';
                    case 'ç' : return 'C';
                    case 'ñ' : return 'Ñ';

                    default : return character;
                }
            }
        }
    }
}
