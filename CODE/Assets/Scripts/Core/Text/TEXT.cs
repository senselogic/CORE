// -- IMPORTS

using System.Text;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class TEXT
    {
        // -- ATTRIBUTES

        public StringBuilder
            StringBuilder_;

        // -- CONSTRUCTORS

        public TEXT(
            )
        {
            StringBuilder_ = new StringBuilder();
        }

        // -- INQUIRIES

        public string GetString(
            )
        {
            return StringBuilder_.ToString();
        }

        // -- OPERATIONS

        public void Clear(
            )
        {
            StringBuilder_.Length = 0;
        }

        // ~~

        public void AddCharacter(
            char character
            )
        {
            StringBuilder_.Append( character );
        }

        // ~~

        public void AddText(
            string text
            )
        {
            StringBuilder_.Append( text );
        }

        // ~~

        public void AddText<_OBJECT_>(
            _OBJECT_ object_
            )
        {
            StringBuilder_.Append( object_.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_, _FOURTH_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object,
            _FOURTH_OBJECT_ fourth_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
            StringBuilder_.Append( fourth_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_, _FOURTH_OBJECT_, _FIFTH_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object,
            _FOURTH_OBJECT_ fourth_object,
            _FIFTH_OBJECT_ fifth_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
            StringBuilder_.Append( fourth_object.ToString() );
            StringBuilder_.Append( fifth_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_, _FOURTH_OBJECT_, _FIFTH_OBJECT_, _SIXTH_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object,
            _FOURTH_OBJECT_ fourth_object,
            _FIFTH_OBJECT_ fifth_object,
            _SIXTH_OBJECT_ sixth_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
            StringBuilder_.Append( fourth_object.ToString() );
            StringBuilder_.Append( fifth_object.ToString() );
            StringBuilder_.Append( sixth_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_, _FOURTH_OBJECT_, _FIFTH_OBJECT_, _SIXTH_OBJECT_, _SEVENTH_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object,
            _FOURTH_OBJECT_ fourth_object,
            _FIFTH_OBJECT_ fifth_object,
            _SIXTH_OBJECT_ sixth_object,
            _SEVENTH_OBJECT_ seventh_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
            StringBuilder_.Append( fourth_object.ToString() );
            StringBuilder_.Append( fifth_object.ToString() );
            StringBuilder_.Append( sixth_object.ToString() );
            StringBuilder_.Append( seventh_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_, _FOURTH_OBJECT_, _FIFTH_OBJECT_, _SIXTH_OBJECT_, _SEVENTH_OBJECT_, _EIGHTH_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object,
            _FOURTH_OBJECT_ fourth_object,
            _FIFTH_OBJECT_ fifth_object,
            _SIXTH_OBJECT_ sixth_object,
            _SEVENTH_OBJECT_ seventh_object,
            _EIGHTH_OBJECT_ eighth_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
            StringBuilder_.Append( fourth_object.ToString() );
            StringBuilder_.Append( fifth_object.ToString() );
            StringBuilder_.Append( sixth_object.ToString() );
            StringBuilder_.Append( seventh_object.ToString() );
            StringBuilder_.Append( eighth_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_, _FOURTH_OBJECT_, _FIFTH_OBJECT_, _SIXTH_OBJECT_, _SEVENTH_OBJECT_, _EIGHTH_OBJECT_, _NINTH_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object,
            _FOURTH_OBJECT_ fourth_object,
            _FIFTH_OBJECT_ fifth_object,
            _SIXTH_OBJECT_ sixth_object,
            _SEVENTH_OBJECT_ seventh_object,
            _EIGHTH_OBJECT_ eighth_object,
            _NINTH_OBJECT_ ninth_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
            StringBuilder_.Append( fourth_object.ToString() );
            StringBuilder_.Append( fifth_object.ToString() );
            StringBuilder_.Append( sixth_object.ToString() );
            StringBuilder_.Append( seventh_object.ToString() );
            StringBuilder_.Append( eighth_object.ToString() );
            StringBuilder_.Append( ninth_object.ToString() );
        }

        // ~~

        public void AddText<_FIRST_OBJECT_, _SECOND_OBJECT_, _THIRD_OBJECT_, _FOURTH_OBJECT_, _FIFTH_OBJECT_, _SIXTH_OBJECT_, _SEVENTH_OBJECT_, _EIGHTH_OBJECT_, _NINTH_OBJECT_, _TENTH_OBJECT_>(
            _FIRST_OBJECT_ first_object,
            _SECOND_OBJECT_ second_object,
            _THIRD_OBJECT_ third_object,
            _FOURTH_OBJECT_ fourth_object,
            _FIFTH_OBJECT_ fifth_object,
            _SIXTH_OBJECT_ sixth_object,
            _SEVENTH_OBJECT_ seventh_object,
            _EIGHTH_OBJECT_ eighth_object,
            _NINTH_OBJECT_ ninth_object,
            _TENTH_OBJECT_ tenth_object
            )
        {
            StringBuilder_.Append( first_object.ToString() );
            StringBuilder_.Append( second_object.ToString() );
            StringBuilder_.Append( third_object.ToString() );
            StringBuilder_.Append( fourth_object.ToString() );
            StringBuilder_.Append( fifth_object.ToString() );
            StringBuilder_.Append( sixth_object.ToString() );
            StringBuilder_.Append( seventh_object.ToString() );
            StringBuilder_.Append( eighth_object.ToString() );
            StringBuilder_.Append( ninth_object.ToString() );
            StringBuilder_.Append( tenth_object.ToString() );
        }

        // ~~

        public void AddRepeatedText<_OBJECT_>(
            _OBJECT_ object_,
            int repetition_count
            )
        {
            int
                repetition_index;

            for ( repetition_index = 0;
                  repetition_index < repetition_count;
                  ++repetition_index )
            {
                StringBuilder_.Append( object_.ToString() );
            }
        }

        // ~~

        public void AddFormattedInteger(
            int integer,
            int minimum_digit_count = 1,
            int fractional_digit_count = 0,
            char dot_character = '.',
            int base_integer = 10
            )
        {
            bool
                integer_is_negative;
            int
                digit,
                digit_count,
                padding_digit_count,
                power_integer;

            Debug.Assert( base_integer > 0 && base_integer <= 16 );

            integer_is_negative = ( integer < 0 );

            if ( integer_is_negative )
            {
                StringBuilder_.Append( '-' );
                integer = -integer;
            }

            digit_count = 1;

            for ( power_integer = 1;
                  integer >= power_integer * base_integer;
                  power_integer *= base_integer )
            {
                ++digit_count;
            }

            for ( padding_digit_count = minimum_digit_count - digit_count;
                  padding_digit_count >= 1;
                  --padding_digit_count )
            {
                StringBuilder_.Append( '0' );
            }

            while ( digit_count > 0 )
            {
                digit = integer / power_integer;
                integer -= digit * power_integer;
                power_integer /= base_integer;

                if ( digit_count == fractional_digit_count )
                {
                    StringBuilder_.Append( dot_character );
                }

                StringBuilder_.Append( "0123456789ABCDEF"[ digit ] );

                --digit_count;
            }
        }
    }
}
