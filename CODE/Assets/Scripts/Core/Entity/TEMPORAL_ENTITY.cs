// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class TEMPORAL_ENTITY : ENTITY
    {
        // -- ATTRIBUTES

        public int
            TimeScaleIndex;
        public float
            TimeStep;

        // -- OPERATIONS

        public void BeginUpdate(
            )
        {
            TimeStep = APPLICATION.GetTimeStep( TimeScaleIndex );
        }

        // ~~

        public void EndUpdate(
            )
        {
        }
    }
}
