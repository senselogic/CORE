// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public struct GRID_CELL
    {
        // -- ATTRIBUTES

        public GRID_REFERENCE
            FirstReference;

        // -- OPERATIONS

        public void AddReference(
            GRID_REFERENCE grid_reference
            )
        {
            Debug.Assert( grid_reference.PriorReference == null );
            Debug.Assert( grid_reference.NextReference == null );

            if ( FirstReference != null )
            {
                FirstReference.PriorReference = grid_reference;
                grid_reference.NextReference = FirstReference;
            }

            FirstReference = grid_reference;
        }

        // ~~

        public void RemoveReference(
            GRID_REFERENCE grid_reference
            )
        {
            if ( FirstReference == grid_reference )
            {
                FirstReference = grid_reference.NextReference;
            }

            if ( grid_reference.PriorReference != null )
            {
                grid_reference.PriorReference.NextReference = grid_reference.NextReference;
            }

            if ( grid_reference.NextReference != null )
            {
                grid_reference.NextReference.PriorReference = grid_reference.PriorReference;
            }

            grid_reference.PriorReference = null;
            grid_reference.NextReference = null;
        }
    }
}
