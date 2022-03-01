// -- IMPORTS

using CORE;

// -- TYPES

namespace CORE
{
    public static class BOOL_EXTENSION
    {
        // -- INQUIRIES

        public static string GetText(
            this bool boolean
            )
        {
            if ( boolean )
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }
}
