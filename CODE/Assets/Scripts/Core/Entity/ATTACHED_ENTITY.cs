// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class ATTACHED_ENTITY : TEMPORAL_ENTITY
    {
        // -- ATTRIBUTES

        public Transform
            ReferenceTransform;
        public float
            Time,
            Duration;

        // -- OPERATIONS

        public void OnEnable(
            )
        {
            ReferenceTransform = null;
            Time = 0.0f;
            Duration = 0.0f;
        }

        // ~~

        public void LateUpdate(
            )
        {
            if ( ReferenceTransform != null )
            {
                SetPositionVector( ReferenceTransform.position );
                SetOrientationQuaternion( ReferenceTransform.rotation );
            }
        }
    }
}
