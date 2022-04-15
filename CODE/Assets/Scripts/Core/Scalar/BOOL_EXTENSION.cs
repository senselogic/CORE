// -- TYPES

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
