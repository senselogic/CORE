// -- IMPORTS

using UnityEngine;
using UnityEngine.SceneManagement;
using CORE;

// -- TYPES

namespace CORE
{
    public class POOL_<_ENTITY_> : POOL where _ENTITY_ : Component
    {
        // -- CONSTRUCTORS

        public POOL_(
            string prefab_path,
            int game_object_count
            ) :
            base( prefab_path, game_object_count )
        {
        }

        // -- OPERATIONS

        public _ENTITY_ CreateEntity(
            Vector3 position_vector,
            Quaternion orientation_quaternion,
            bool object_is_permanent = false
            )
        {
            _ENTITY_
                entity;
            POOL_OBJECT
                pool_object;

            pool_object = CreateObject( position_vector, orientation_quaternion, object_is_permanent );

            entity = pool_object.gameObject.GetComponent<_ENTITY_>();
            Debug.Assert( entity != null );

            return entity;
        }

        // ~~

        public void ReleaseEntity(
            _ENTITY_ entity
            )
        {
            ReleaseObject( entity.GetComponent<POOL_OBJECT>() );
        }
    }
}
