// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CORE;

// -- TYPES

namespace CORE
{
    public class POOL
    {
        // -- ATTRIBUTES

        public GameObject
            PrefabGameObject;
        public POOL_OBJECT
            FirstInactiveObject,
            FirstActiveObject;
        public static List<POOL>
            PoolList;

        // -- CONSTRUCTORS

        public POOL(
            string prefab_path,
            int object_count
            )
        {
            int
                object_index;
            POOL_OBJECT
                pool_object;

            PrefabGameObject = ( GameObject )Resources.Load( prefab_path );
            PrefabGameObject.SetActive( false );

            FirstInactiveObject = null;
            FirstActiveObject = null;

            for ( object_index = 0;
                  object_index < object_count;
                  ++object_index )
            {
                pool_object = InstantiateObject( Vector3.zero, Quaternion.identity, false, false );
                pool_object.NextObject = FirstInactiveObject;

                if ( FirstInactiveObject != null )
                {
                    FirstInactiveObject.PriorObject = pool_object;
                }

                FirstInactiveObject = pool_object;
            }

            if ( PoolList == null )
            {
                PoolList = new List<POOL>();
            }

            PoolList.Add( this );
        }

        // -- OPERATIONS

        public POOL_OBJECT InstantiateObject(
            Vector3 position_vector,
            Quaternion orientation_quaternion,
            bool object_is_permanent,
            bool object_is_active
            )
        {
            GameObject
                game_object;
            POOL_OBJECT
                pool_object;

            game_object = ( GameObject )Object.Instantiate( PrefabGameObject, position_vector, orientation_quaternion );
            game_object.SetActive( object_is_active );

            Object.DontDestroyOnLoad( game_object );

            pool_object = game_object.GetComponent<POOL_OBJECT>();
            pool_object.IsPermanent = object_is_permanent;

            return pool_object;
        }

        // ~~

        public POOL_OBJECT CreateObject(
            Vector3 position_vector,
            Quaternion orientation_quaternion,
            bool object_is_permanent
            )
        {
            GameObject
                game_object;
            POOL_OBJECT
                pool_object;

            if ( FirstInactiveObject == null )
            {
                pool_object = InstantiateObject( position_vector, orientation_quaternion, object_is_permanent, true );
            }
            else
            {
                pool_object = FirstInactiveObject;
                FirstInactiveObject = pool_object.NextObject;

                if ( FirstInactiveObject != null )
                {
                    FirstInactiveObject.PriorObject = null;
                }

                pool_object.IsPermanent = object_is_permanent;

                game_object = pool_object.gameObject;
                game_object.transform.position = position_vector;
                game_object.transform.rotation = orientation_quaternion;
                game_object.SetActive( true );
            }

            pool_object.NextObject = FirstActiveObject;

            if ( FirstActiveObject != null )
            {
                FirstActiveObject.PriorObject = pool_object;
            }

            FirstActiveObject = pool_object;

            return pool_object;
        }

        // ~~

        public void ReleaseObject(
            POOL_OBJECT pool_object
            )
        {
            pool_object.gameObject.SetActive( false );

            if ( FirstActiveObject == pool_object )
            {
                FirstActiveObject = pool_object.NextObject;
            }

            if ( pool_object.PriorObject != null )
            {
                pool_object.PriorObject.NextObject = pool_object.NextObject;
            }

            if ( pool_object.NextObject != null )
            {
                pool_object.NextObject.PriorObject = pool_object.PriorObject;
            }

            pool_object.PriorObject = null;
            pool_object.NextObject = FirstInactiveObject;
            FirstInactiveObject = pool_object;
        }

        // ~~

        public void ReleaseObjects(
            )
        {
            POOL_OBJECT
                next_pool_object,
                pool_object;

            for ( pool_object = FirstActiveObject;
                  pool_object != null;
                  pool_object = next_pool_object )
            {
                next_pool_object = pool_object.NextObject;

                if ( !pool_object.IsPermanent )
                {
                    ReleaseObject( pool_object );
                }
            }
        }

        // ~~

        public static void ReleaseAllObjects(
            )
        {
            foreach ( POOL pool in PoolList )
            {
                pool.ReleaseObjects();
            }
        }
    }
}
