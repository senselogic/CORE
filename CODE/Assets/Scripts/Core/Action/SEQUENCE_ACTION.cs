// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class SEQUENCE_ACTION : ACTION
    {
        // -- ATTRIBUTES

        public static List<SEQUENCE_ACTION>
            FreeSequenceActionList = new List<SEQUENCE_ACTION>();

        // -- OPERATIONS

        public static SEQUENCE_ACTION Create(
            )
        {
            SEQUENCE_ACTION
                sequence_action;

            if ( FreeSequenceActionList.Count == 0 )
            {
                sequence_action = new SEQUENCE_ACTION();
            }
            else
            {
                sequence_action = FreeSequenceActionList[ FreeSequenceActionList.Count - 1 ];
                FreeSequenceActionList.RemoveAt( FreeSequenceActionList.Count - 1 );
            }

            sequence_action.Initialize();

            return sequence_action;
        }

        // ~~

        public override void Release(
            )
        {
            FreeSequenceActionList.Add( this );
        }

        // ~~

        public override void UpdateProperty(
            )
        {
            if ( FirstSubAction != null )
            {
                FirstSubAction.Update( TimeStep );
            }
            else
            {
                Finish();
            }
        }
    }
}
