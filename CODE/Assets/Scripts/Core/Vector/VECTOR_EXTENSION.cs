// -- IMPORTS

using UnityEngine;

// -- TYPES

public static class VECTOR_EXTENSION
{
    // -- INQUIRIES

    public static Vector3 GetXyVector(
        this Vector3 vector
        )
    {
        return new Vector3( vector.x, vector.y, 0.0f );
    }

    // ~~

    public static Vector3 GetXzVector(
        this Vector3 vector
        )
    {
        return new Vector3( vector.x, 0.0f, vector.z );
    }

    // ~~

    public static Vector3 GetYzVector(
        this Vector3 vector
        )
    {
        return new Vector3( 0.0f, vector.y, vector.z );
    }
}
