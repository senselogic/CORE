// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class CHARACTER_ENTITY : ANIMATED_ENTITY
    {
        // -- ATTRIBUTES

        public CharacterController
            Controller;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            Controller = GetComponent<CharacterController>();
        }

        // ~~

        public override void UpdateTransform(
            )
        {
            transform.rotation = OrientationQuaternion;

            if ( Controller.enabled )
            {
                Controller.Move( PositionVector - OldPositionVector );
            }
        }
    }
}
