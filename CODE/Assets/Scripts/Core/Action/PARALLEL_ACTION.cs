// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class PARALLEL_ACTION : ACTION
    {
        // -- ATTRIBUTES

        static List<PARALLEL_ACTION>
            FreeParallelActionList = new List<PARALLEL_ACTION>();

        // -- OPERATIONS

        public static PARALLEL_ACTION Create(
            )
        {
            PARALLEL_ACTION
                parallel_action;

            if ( FreeParallelActionList.Count == 0 )
            {
                parallel_action = new PARALLEL_ACTION();
            }
            else
            {
                parallel_action = FreeParallelActionList[ FreeParallelActionList.Count - 1 ];
                FreeParallelActionList.RemoveAt( FreeParallelActionList.Count - 1 );
            }

            parallel_action.Initialize();

            return parallel_action;
        }

        // ~~

        public override void Release(
            )
        {
            FreeParallelActionList.Add( this );
        }

        // ~~

        public override void UpdateProperty(
            )
        {
            ACTION
                sub_action;

            for ( sub_action = FirstSubAction;
                  sub_action != null;
                  sub_action = sub_action.NextAction )
            {
                sub_action.Update( TimeStep );
            }

            if ( FirstSubAction == null )
            {
                Finish();
            }
        }
    }
}
