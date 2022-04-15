// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class POOL_OBJECT : MonoBehaviour
    {
        // -- ATTRIBUTES

        public bool
            IsPermanent;
        public POOL_OBJECT
            PriorObject,
            NextObject;
    }
}
