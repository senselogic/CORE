// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    public class BLOOD_SPRAY : PARTICLE_ENTITY
    {
        // -- ATTRIBUTES

        public static POOL_<BLOOD_SPRAY>
            Pool;

        // -- OPERATIONS

        public override void Release(
            )
        {
            Pool.ReleaseEntity( this );
        }

        // ~~

        public static void InitializePool(
            )
        {
            if ( Pool == null )
            {
                Pool = new POOL_<BLOOD_SPRAY>( "Prefabs/BloodSpray", 4 );
            }
        }

        // ~~

        public static BLOOD_SPRAY Create(
            Vector3 position_vector,
            Quaternion orientation_quaternion
            )
        {
            return Pool.CreateEntity( position_vector, orientation_quaternion );
        }
    }
}
