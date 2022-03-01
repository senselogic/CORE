#if UNITY_EDITOR
// -- IMPORTS

using System;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class UPDATE : Attribute
    {
        // -- ATTRIBUTES

        public int
            ExecutionOrderSign;
        public string[]
            PriorTypeNameArray,
            NextTypeNameArray;

        // -- CONSTRUCTORS

        public UPDATE(
            string predecessor_text = "",
            string successor_text = ""
            )
        {
            PriorTypeNameArray = GetTypeNameArray( predecessor_text );
            NextTypeNameArray = GetTypeNameArray( successor_text );

            if ( Array.IndexOf( NextTypeNameArray, "*" ) >= 0 )
            {
                ExecutionOrderSign = -1;
            }
            else if ( Array.IndexOf( PriorTypeNameArray, "*" ) >= 0 )
            {
                ExecutionOrderSign = 1;
            }
            else
            {
                ExecutionOrderSign = 0;
            }

            Debug.Assert( PriorTypeNameArray != null );
            Debug.Assert( NextTypeNameArray != null );
        }

        // -- INQUIRIES

        public string[] GetTypeNameArray(
            string text
            )
        {
            return text.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
        }
    }
}
#endif
