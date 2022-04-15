// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class DYNAMIC_ENTITY : TEMPORAL_ENTITY
    {
        // -- ATTRIBUTES

        public Vector3
            PriorPositionVector,
            OldPositionVector,
            PositionVector;
        public Quaternion
            PriorOrientationQuaternion,
            OldOrientationQuaternion,
            OrientationQuaternion;
        public bool
            IsUpdated;

        // -- INQUIRIES

        public override Vector3 GetPositionVector(
            )
        {
            if ( IsUpdated )
            {
                return PositionVector;
            }
            else
            {
                return transform.position;
            }
        }

        // ~~

        public override Quaternion GetOrientationQuaternion(
            )
        {
            if ( IsUpdated )
            {
                return OrientationQuaternion;
            }
            else
            {
                return transform.rotation;
            }
        }

        // -- OPERATIONS

        public void OnEnable(
            )
        {
            PositionVector = transform.position;
            OrientationQuaternion = transform.rotation;

            OldPositionVector = PositionVector;
            OldOrientationQuaternion = OrientationQuaternion;

            PriorPositionVector = PositionVector;
            PriorOrientationQuaternion = OrientationQuaternion;
        }

        // ~~

        public new void BeginUpdate(
            )
        {
            base.BeginUpdate();

            PriorOrientationQuaternion = OldOrientationQuaternion;
            PriorPositionVector = OldPositionVector;

            OldOrientationQuaternion = transform.rotation;
            OldPositionVector = transform.position;

            OrientationQuaternion = OldOrientationQuaternion;
            PositionVector = OldPositionVector;

            IsUpdated = true;
        }
        // ~~

        public override void SetPositionVector(
            Vector3 position_vector
            )
        {
            if ( IsUpdated )
            {
                PositionVector = position_vector;
            }
            else
            {
                transform.position = position_vector;
            }
        }

        // ~~

        public override void SetOrientationQuaternion(
            Quaternion orientation_quaternion
            )
        {
            if ( IsUpdated )
            {
                OrientationQuaternion = orientation_quaternion;
            }
            else
            {
                transform.rotation = orientation_quaternion;
            }
        }

        // ~~

        public virtual void UpdateTransform(
            )
        {
            transform.position = PositionVector;
            transform.rotation = OrientationQuaternion;
        }

        // ~~

        public new void EndUpdate(
            )
        {
            UpdateTransform();

            IsUpdated = false;

            base.EndUpdate();
        }
    }
}
