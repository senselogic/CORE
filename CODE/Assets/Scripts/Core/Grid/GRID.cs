// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class GRID
    {
        // -- TYPES

        public delegate bool IS_MATCHING_ENTITY_DELEGATE(
            GRID_REFERENCE grid_reference
            );

        // -- ATTRIBUTES

        public Vector3
            PositionVector,
            SizeVector;
        public int
            XCellCount,
            YCellCount;
        public float
            XCellSize,
            YCellSize,
            OneOverXCellSize,
            OneOverYCellSize;
        public GRID_CELL[,]
            CellArray;

        // -- CONSTRUCTORS

        public GRID(
            Vector3 minimum_position_vector,
            Vector3 maximum_position_vector,
            int x_cell_count,
            int y_cell_count
            )
        {
            PositionVector = minimum_position_vector;
            SizeVector = ( maximum_position_vector - minimum_position_vector );
            XCellCount = x_cell_count;
            YCellCount = y_cell_count;
            XCellSize = SizeVector.x / x_cell_count;
            YCellSize = SizeVector.y / y_cell_count;
            OneOverXCellSize = 1.0f / XCellSize;
            OneOverYCellSize = 1.0f / YCellSize;

            CellArray = new GRID_CELL[ YCellCount, XCellCount ];
        }

        // -- INQUIRIES

        public int GetXCellIndex(
            float x_position
            )
        {
            return Mathf.Clamp( ( int )( ( x_position - PositionVector.x ) * OneOverXCellSize ), 0, XCellCount - 1 );
        }

        // ~~

        public int GetYCellIndex(
            float y_position
            )
        {
            return Mathf.Clamp( ( int )( ( y_position - PositionVector.y ) * OneOverYCellSize ), 0, YCellCount - 1 );
        }

        // ~~

        public GRID_REFERENCE FindNearEntity(
            ENTITY entity,
            float maximum_distance,
            ulong required_feature_mask = 0,
            ulong allowed_feature_mask = 0,
            ulong forbidden_feature_mask = 0
            )
        {
            int
                first_x_cell_index,
                first_y_cell_index,
                last_x_cell_index,
                last_y_cell_index,
                x_cell_index,
                y_cell_index;
            Vector3
                position_vector;
            GRID_REFERENCE
                grid_reference;

            position_vector = entity.GetPositionVector();

            first_x_cell_index = GetXCellIndex( position_vector.x - maximum_distance );
            first_y_cell_index = GetYCellIndex( position_vector.y - maximum_distance);
            last_x_cell_index = GetXCellIndex( position_vector.x + maximum_distance );
            last_y_cell_index = GetYCellIndex( position_vector.y + maximum_distance );

            for ( y_cell_index = first_y_cell_index;
                  y_cell_index <= last_y_cell_index;
                  ++y_cell_index )
            {
                for ( x_cell_index = first_x_cell_index;
                      x_cell_index <= last_x_cell_index;
                      ++x_cell_index )
                {
                    for ( grid_reference = CellArray[ y_cell_index, x_cell_index ].FirstReference;
                          grid_reference != null;
                          grid_reference = grid_reference.NextReference )
                    {
                        if ( grid_reference.Entity != entity
                             && grid_reference.Entity.HasFeatureMask( required_feature_mask, allowed_feature_mask, forbidden_feature_mask )
                             && entity.IsNearEntity( grid_reference.Entity, maximum_distance ) )
                        {
                            return grid_reference;
                        }
                    }
                }
            }

            return null;
        }

        // ~~

        public List<GRID_REFERENCE> FindNearEntities(
            ENTITY entity,
            float maximum_distance,
            ulong required_feature_mask = 0,
            ulong allowed_feature_mask = 0,
            ulong forbidden_feature_mask = 0
            )
        {
            int
                first_x_cell_index,
                first_y_cell_index,
                last_x_cell_index,
                last_y_cell_index,
                x_cell_index,
                y_cell_index;
            List<GRID_REFERENCE>
                grid_reference_list;
            Vector3
                position_vector;
            GRID_REFERENCE
                grid_reference;

            grid_reference_list = null;

            position_vector = entity.GetPositionVector();

            first_x_cell_index = GetXCellIndex( position_vector.x - maximum_distance );
            first_y_cell_index = GetYCellIndex( position_vector.y - maximum_distance);
            last_x_cell_index = GetXCellIndex( position_vector.x + maximum_distance );
            last_y_cell_index = GetYCellIndex( position_vector.y + maximum_distance );

            for ( y_cell_index = first_y_cell_index;
                  y_cell_index <= last_y_cell_index;
                  ++y_cell_index )
            {
                for ( x_cell_index = first_x_cell_index;
                      x_cell_index <= last_x_cell_index;
                      ++x_cell_index )
                {
                    for ( grid_reference = CellArray[ y_cell_index, x_cell_index ].FirstReference;
                          grid_reference != null;
                          grid_reference = grid_reference.NextReference )
                    {
                        if ( grid_reference.Entity != entity
                             && grid_reference.Entity.HasFeatureMask( required_feature_mask, allowed_feature_mask, forbidden_feature_mask )
                             && entity.IsNearEntity( grid_reference.Entity, maximum_distance ) )
                        {
                            if ( grid_reference_list == null )
                            {
                                grid_reference_list = new List<GRID_REFERENCE>();
                            }

                            grid_reference_list.Add( grid_reference );
                        }
                    }
                }
            }

            return grid_reference_list;
        }

        // ~~

        public GRID_REFERENCE FindVisibleEntity(
            ENTITY entity,
            float maximum_distance,
            float maximum_angle,
            ulong required_feature_mask = 0,
            ulong allowed_feature_mask = 0,
            ulong forbidden_feature_mask = 0
            )
        {
            int
                first_x_cell_index,
                first_y_cell_index,
                last_x_cell_index,
                last_y_cell_index,
                x_cell_index,
                y_cell_index;
            Vector3
                position_vector;
            GRID_REFERENCE
                grid_reference;

            position_vector = entity.GetPositionVector();

            first_x_cell_index = GetXCellIndex( position_vector.x - maximum_distance );
            first_y_cell_index = GetYCellIndex( position_vector.y - maximum_distance);
            last_x_cell_index = GetXCellIndex( position_vector.x + maximum_distance );
            last_y_cell_index = GetYCellIndex( position_vector.y + maximum_distance );

            for ( y_cell_index = first_y_cell_index;
                  y_cell_index <= last_y_cell_index;
                  ++y_cell_index )
            {
                for ( x_cell_index = first_x_cell_index;
                      x_cell_index <= last_x_cell_index;
                      ++x_cell_index )
                {
                    for ( grid_reference = CellArray[ y_cell_index, x_cell_index ].FirstReference;
                          grid_reference != null;
                          grid_reference = grid_reference.NextReference )
                    {
                        if ( grid_reference.Entity != entity
                             && grid_reference.Entity.HasFeatureMask( required_feature_mask, allowed_feature_mask, forbidden_feature_mask )
                             && entity.LooksEntity( grid_reference.Entity, maximum_distance, maximum_angle ) )
                        {
                            return grid_reference;
                        }
                    }
                }
            }

            return null;
        }


        // ~~

        public List<GRID_REFERENCE> FindVisibleEntities(
            ENTITY entity,
            float maximum_distance,
            float maximum_angle,
            ulong required_feature_mask = 0,
            ulong allowed_feature_mask = 0,
            ulong forbidden_feature_mask = 0
            )
        {
            int
                first_x_cell_index,
                first_y_cell_index,
                last_x_cell_index,
                last_y_cell_index,
                x_cell_index,
                y_cell_index;
            List<GRID_REFERENCE>
                grid_reference_list;
            Vector3
                position_vector;
            GRID_REFERENCE
                grid_reference;

            grid_reference_list = null;

            position_vector = entity.GetPositionVector();

            first_x_cell_index = GetXCellIndex( position_vector.x - maximum_distance );
            first_y_cell_index = GetYCellIndex( position_vector.y - maximum_distance);
            last_x_cell_index = GetXCellIndex( position_vector.x + maximum_distance );
            last_y_cell_index = GetYCellIndex( position_vector.y + maximum_distance );

            for ( y_cell_index = first_y_cell_index;
                  y_cell_index <= last_y_cell_index;
                  ++y_cell_index )
            {
                for ( x_cell_index = first_x_cell_index;
                      x_cell_index <= last_x_cell_index;
                      ++x_cell_index )
                {
                    for ( grid_reference = CellArray[ y_cell_index, x_cell_index ].FirstReference;
                          grid_reference != null;
                          grid_reference = grid_reference.NextReference )
                    {
                        if ( grid_reference.Entity != entity
                             && grid_reference.Entity.HasFeatureMask( required_feature_mask, allowed_feature_mask, forbidden_feature_mask )
                             && entity.LooksEntity( grid_reference.Entity, maximum_distance, maximum_angle ) )
                        {
                            if ( grid_reference_list == null )
                            {
                                grid_reference_list = new List<GRID_REFERENCE>();
                            }

                            grid_reference_list.Add( grid_reference );
                        }
                    }
                }
            }

            return grid_reference_list;
        }

        // ~~

        public GRID_REFERENCE FindMatchingEntity(
            float minimum_x_position,
            float minimum_y_position,
            float maximum_x_position,
            float maximum_y_position,
            ulong required_feature_mask = 0,
            ulong allowed_feature_mask = 0,
            ulong forbidden_feature_mask = 0,
            IS_MATCHING_ENTITY_DELEGATE is_matching_entity_delegate = null
            )
        {
            int
                first_x_cell_index,
                first_y_cell_index,
                last_x_cell_index,
                last_y_cell_index,
                x_cell_index,
                y_cell_index;
            GRID_REFERENCE
                grid_reference;

            first_x_cell_index = GetXCellIndex( minimum_x_position );
            first_y_cell_index = GetYCellIndex( minimum_y_position );
            last_x_cell_index = GetXCellIndex( maximum_x_position );
            last_y_cell_index = GetYCellIndex( maximum_y_position );

            for ( y_cell_index = first_y_cell_index;
                  y_cell_index <= last_y_cell_index;
                  ++y_cell_index )
            {
                for ( x_cell_index = first_x_cell_index;
                      x_cell_index <= last_x_cell_index;
                      ++x_cell_index )
                {
                    for ( grid_reference = CellArray[ y_cell_index, x_cell_index ].FirstReference;
                          grid_reference != null;
                          grid_reference = grid_reference.NextReference )
                    {
                        if ( grid_reference.Entity.HasFeatureMask( required_feature_mask, allowed_feature_mask, forbidden_feature_mask )
                             && ( is_matching_entity_delegate == null
                                  || is_matching_entity_delegate( grid_reference ) ) )
                        {
                            return grid_reference;
                        }
                    }
                }
            }

            return null;
        }

        // ~~

        public List<GRID_REFERENCE> FindMatchingEntities(
            float minimum_x_position,
            float minimum_y_position,
            float maximum_x_position,
            float maximum_y_position,
            ulong required_feature_mask = 0,
            ulong allowed_feature_mask = 0,
            ulong forbidden_feature_mask = 0,
            IS_MATCHING_ENTITY_DELEGATE is_matching_entity_delegate = null
            )
        {
            int
                first_x_cell_index,
                first_y_cell_index,
                last_x_cell_index,
                last_y_cell_index,
                x_cell_index,
                y_cell_index;
            List<GRID_REFERENCE>
                grid_reference_list;
            GRID_REFERENCE
                grid_reference;

            grid_reference_list = null;

            first_x_cell_index = GetXCellIndex( minimum_x_position );
            first_y_cell_index = GetYCellIndex( minimum_y_position );
            last_x_cell_index = GetXCellIndex( maximum_x_position );
            last_y_cell_index = GetYCellIndex( maximum_y_position );

            for ( y_cell_index = first_y_cell_index;
                  y_cell_index <= last_y_cell_index;
                  ++y_cell_index )
            {
                for ( x_cell_index = first_x_cell_index;
                      x_cell_index <= last_x_cell_index;
                      ++x_cell_index )
                {
                    for ( grid_reference = CellArray[ y_cell_index, x_cell_index ].FirstReference;
                          grid_reference != null;
                          grid_reference = grid_reference.NextReference )
                    {
                        if ( grid_reference.Entity.HasFeatureMask( required_feature_mask, allowed_feature_mask, forbidden_feature_mask )
                             && ( is_matching_entity_delegate == null
                                  || is_matching_entity_delegate( grid_reference ) ) )
                        {
                            if ( grid_reference_list == null )
                            {
                                grid_reference_list = new List<GRID_REFERENCE>();
                            }

                            grid_reference_list.Add( grid_reference );
                        }
                    }
                }
            }

            return grid_reference_list;
        }

        // -- OPERATIONS

        public void AddReference(
            GRID_REFERENCE grid_reference
            )
        {
            int
                x_cell_index,
                y_cell_index;
            Vector3
                position_vector;

            position_vector = grid_reference.Entity.GetPositionVector();
            x_cell_index = GetXCellIndex( position_vector.x );
            y_cell_index = GetYCellIndex( position_vector.y );

            CellArray[ y_cell_index, x_cell_index ].AddReference( grid_reference );

            grid_reference.XCellIndex = x_cell_index;
            grid_reference.YCellIndex = y_cell_index;
        }

        // ~~

        public GRID_REFERENCE CreateReference(
            ENTITY entity
            )
        {
            GRID_REFERENCE
                grid_reference;

            grid_reference = new GRID_REFERENCE( this, entity );

            AddReference( grid_reference );

            return grid_reference;
        }

        // ~~

        public void RemoveReference(
            GRID_REFERENCE grid_reference
            )
        {
            int
                x_cell_index,
                y_cell_index;

            x_cell_index = grid_reference.XCellIndex;
            y_cell_index = grid_reference.YCellIndex;

            CellArray[ y_cell_index, x_cell_index ].RemoveReference( grid_reference );

            grid_reference.XCellIndex = -1;
            grid_reference.YCellIndex = -1;
        }

        // ~~

        public void UpdateReference(
            GRID_REFERENCE grid_reference
            )
        {
            int
                new_x_cell_index,
                new_y_cell_index,
                old_x_cell_index,
                old_y_cell_index;
            Vector3
                position_vector;

            position_vector = grid_reference.Entity.GetPositionVector();

            old_x_cell_index = grid_reference.XCellIndex;
            old_y_cell_index = grid_reference.YCellIndex;
            new_x_cell_index = GetXCellIndex( position_vector.x );
            new_y_cell_index = GetYCellIndex( position_vector.y );

            if ( new_x_cell_index != old_x_cell_index
                 || new_y_cell_index != old_y_cell_index )
            {
                CellArray[ old_y_cell_index, old_x_cell_index ].RemoveReference( grid_reference );
                CellArray[ new_y_cell_index, new_x_cell_index ].AddReference( grid_reference );

                grid_reference.XCellIndex = new_x_cell_index;
                grid_reference.YCellIndex = new_y_cell_index;
            }
        }
    }
}
