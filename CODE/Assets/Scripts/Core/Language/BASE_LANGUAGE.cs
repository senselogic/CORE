// -- IMPORTS

using System;
using System.Collections.Generic;
using CORE;

// -- TYPES

namespace CORE
{
    public class BASE_LANGUAGE
    {
        // -- ATTRIBUTES

        public string
            Name,
            Code;
        public char
            DotCharacter;
        public Dictionary<string, string>
            TextDictionary;

        // -- CONSTRUCTORS

        public BASE_LANGUAGE(
            string name,
            string code,
            char dot_character
            )
        {
            Name = name;
            Code = code;
            DotCharacter = dot_character;
            TextDictionary = new Dictionary<string, string>();
        }

        // -- INQUIRIES

        public bool HasText(
            string slug
            )
        {
            return TextDictionary.ContainsKey( slug );
        }

        // ~~

        public string GetText(
            string slug
            )
        {
            return TextDictionary[ slug ];
        }

        // ~~

        public string GetTranslatedText(
            string text
            )
        {
            return text.GetTranslatedText( Code );
        }

        // ~~

        public string GetRealText(
            float real,
            int minimum_fractional_digit_count = 1,
            int maximum_fractional_digit_count = 6
            )
        {
            return real.GetText( minimum_fractional_digit_count, maximum_fractional_digit_count, DotCharacter );
        }
    }
}
