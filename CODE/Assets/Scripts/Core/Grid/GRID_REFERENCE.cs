// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class GRID_REFERENCE
    {
        // -- ATTRIBUTES

        public GRID
            Grid;
        public ENTITY
            Entity;
        public int
            XCellIndex,
            YCellIndex;
        public GRID_REFERENCE
            PriorReference,
            NextReference;

        // -- CONSTRUCTORS

        public GRID_REFERENCE(
            GRID grid,
            ENTITY entity
            )
        {
            Grid = grid;
            Entity = entity;
        }

        // -- OPERATIONS

        public void Update(
            )
        {
            Grid.UpdateReference( this );
        }

        // ~~

        public void Remove(
            )
        {
            Grid.RemoveReference( this );
        }
    }
}
