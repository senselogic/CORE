// -- IMPORTS

using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class ENTITY : MonoBehaviour
    {
        // -- CONSTANTS

        public const int
            WaterLayerIndex = 4,
            TriggerLayerIndex = 8,
            OwnerFeatureIndex = 0,
            HealthFeatureIndex = 1,
            AliveFeatureIndex = 2;

        // -- ATTRIBUTES

        public ulong
            FeatureMask;

        // -- INQUIRIES

        public virtual ENTITY GetEntity(
            )
        {
            return this;
        }

        // ~~

        public virtual ENTITY GetOwnerEntity(
            )
        {
            return this;
        }

        // ~~

        public bool IsActive(
            )
        {
            return gameObject.activeSelf;
        }

        // ~~

        public virtual Vector3 GetPositionVector(
            )
        {
            return transform.position;
        }

        // ~~

        public virtual Quaternion GetOrientationQuaternion(
            )
        {
            return transform.rotation;
        }

        // ~~

        public virtual float GetHealth(
            )
        {
            return 0;
        }

        // ~~

        public bool HasFeatureMask(
            ulong required_feature_mask
            )
        {
            return ( FeatureMask & required_feature_mask ) == required_feature_mask;
        }

        // ~~

        public bool HasFeatureMask(
            ulong required_feature_mask,
            ulong allowed_feature_mask
            )
        {
            return
                ( FeatureMask & required_feature_mask ) == required_feature_mask
                && ( ( FeatureMask & allowed_feature_mask ) != 0 || allowed_feature_mask == 0 );
        }

        // ~~

        public bool HasFeatureMask(
            ulong required_feature_mask,
            ulong allowed_feature_mask,
            ulong forbidden_feature_mask
            )
        {
            return
                ( FeatureMask & required_feature_mask ) == required_feature_mask
                && ( ( FeatureMask & allowed_feature_mask ) != 0 || allowed_feature_mask == 0 )
                && ( FeatureMask & forbidden_feature_mask ) == 0;
        }

        // ~~

        public Transform FindSubTransformByName(
            string name
            )
        {
            return FindSubTransformByName( transform, name );
        }

        // ~~

        public GameObject FindSubObjectByName(
            string name
            )
        {
            Transform
                found_transform;

            found_transform = FindSubTransformByName( transform, name );

            if ( found_transform != null )
            {
                return found_transform.gameObject;
            }
            else
            {
                return null;
            }
        }

        // ~~

        public GameObject FindObjectByName(
            string name
            )
        {
            return GameObject.Find( name );
        }

        // ~~

        public _ENTITY_ FindSubEntityByName<_ENTITY_>(
            string name
            )
        {
            return FindSubObjectByName( name ).GetComponent<_ENTITY_>();
        }

        // ~~

        public _ENTITY_ FindSubEntityByType<_ENTITY_>(
            ) where _ENTITY_ : Component
        {
            return FindSubEntityByType<_ENTITY_>( transform );
        }

        // ~~

        public Transform FindSubTransformByTag(
            string tag
            )
        {
            return FindSubTransformByTag( transform, tag );
        }

        // ~~

        public GameObject FindSubObjectByTag(
            string tag
            )
        {
            Transform
                found_transform;

            found_transform = FindSubTransformByTag( transform, tag );

            if ( found_transform != null )
            {
                return found_transform.gameObject;
            }
            else
            {
                return null;
            }
        }

        // ~~

        public GameObject FindObjectByTag(
            string tag
            )
        {
            return GameObject.FindGameObjectWithTag( tag );
        }

        // ~~

        public _ENTITY_ FindSubEntityByTag<_ENTITY_>(
            string tag
            )
        {
            return FindSubObjectByTag( tag ).GetComponent<_ENTITY_>();
        }

        // ~~

        public bool IsNearEntity(
            ENTITY entity,
            float maximum_distance
            )
        {
            return ( entity.GetPositionVector() - GetPositionVector() ).sqrMagnitude <= maximum_distance * maximum_distance;
        }

        // ~~

        public bool LooksEntity(
            ENTITY entity,
            float maximum_distance,
            float maximum_angle,
            float head_height = 1.8f
            )
        {
            Vector3
                entity_axis_vector,
                entity_offset_vector,
                entity_position_vector,
                forward_axis_vector,
                position_vector;

            position_vector = GetPositionVector();
            position_vector.y += head_height;

            entity_position_vector = entity.GetPositionVector();
            entity_position_vector.y += head_height;

            entity_offset_vector = entity_position_vector - position_vector;

            if ( entity_offset_vector.sqrMagnitude <= maximum_distance * maximum_distance )
            {
                forward_axis_vector = transform.TransformDirection( Vector3.forward );
                entity_axis_vector = entity_offset_vector.normalized;

                return Vector3.Dot( forward_axis_vector, entity_axis_vector ) > Mathf.Cos( maximum_angle * Mathf.Deg2Rad );
            }
            else
            {
                return false;
            }
        }

        // ~~

        public static _ENTITY_[] FindEntitiesByType<_ENTITY_>(
            ) where _ENTITY_ : Component
        {
            return Object.FindObjectsOfType<_ENTITY_>();
        }

        // ~~

        public static _ENTITY_ FindEntityByType<_ENTITY_>(
            ) where _ENTITY_ : Component
        {
            _ENTITY_[]
                entity_array;

            entity_array = Object.FindObjectsOfType<_ENTITY_>();

            if ( entity_array.Length > 0 )
            {
                return entity_array[ 0 ];
            }
            else
            {
                return null;
            }
        }

        // ~~

        public static _ENTITY_ FindSubEntityByType<_ENTITY_>(
            Transform transform
            ) where _ENTITY_ : Component
        {
            int
                child_count,
                child_index;
            Transform
                child_transform;
            _ENTITY_
                found_entity;

            child_count = transform.childCount;

            for ( child_index = 0;
                  child_index < child_count;
                  ++child_index )
            {
                child_transform = transform.GetChild( child_index );

                found_entity = child_transform.GetComponent<_ENTITY_>();

                if ( found_entity != null )
                {
                    return found_entity;
                }
                else
                {
                    found_entity = FindSubEntityByType<_ENTITY_>( child_transform );

                    if ( found_entity != null )
                    {
                        return found_entity;
                    }
                }
            }

            return null;
        }

        // ~~

        public static Transform FindTransformByName(
            string name
            )
        {
            return GameObject.Find( name ).transform;
        }

        // ~~

        public static Transform FindSubTransformByName(
            Transform transform,
            string name
            )
        {
            int
                child_count,
                child_index;
            Transform
                child_transform,
                found_transform;

            child_count = transform.childCount;

            for ( child_index = 0;
                  child_index < child_count;
                  ++child_index )
            {
                child_transform = transform.GetChild( child_index );

                if ( child_transform.name == name )
                {
                    return child_transform;
                }
                else
                {
                    found_transform = FindSubTransformByName( child_transform, name );

                    if ( found_transform != null )
                    {
                        return found_transform;
                    }
                }
            }

            return null;
        }

        // ~~

        public static _ENTITY_ FindEntityByName<_ENTITY_>(
            string name
            )
        {
            return GameObject.Find( name ).GetComponent<_ENTITY_>();
        }

        // ~~

        public static Transform FindSubTransformByTag(
            Transform transform,
            string tag
            )
        {
            int
                child_count,
                child_index;
            Transform
                child_transform,
                found_transform;

            child_count = transform.childCount;

            for ( child_index = 0;
                  child_index < child_count;
                  ++child_index )
            {
                child_transform = transform.GetChild( child_index );

                if ( child_transform.tag == tag )
                {
                    return child_transform;
                }
                else
                {
                    found_transform = FindSubTransformByTag( child_transform, tag );

                    if ( found_transform != null )
                    {
                        return found_transform;
                    }
                }
            }

            return null;
        }

        // ~~

        public static _ENTITY_ FindEntityByTag<_ENTITY_>(
            string tag
            )
        {
            return GameObject.FindGameObjectWithTag( tag ).GetComponent<_ENTITY_>();
        }

        // ~~

        public static _ENTITY_ FindSuperEntity<_ENTITY_>(
            Transform transform
            ) where _ENTITY_ : Component
        {
            _ENTITY_
                entity;

            while ( transform != null )
            {
                entity = transform.gameObject.GetComponent<_ENTITY_>();

                if ( entity != null )
                {
                    return entity;
                }
                else
                {
                    transform = transform.parent;
                }
            }

            return null;
        }

        // ~~

        public static _ENTITY_ FindSuperEntityByFeatureMask<_ENTITY_>(
            Transform transform,
            ulong required_feature_mask = 0,
            ulong allowed_feature_mask = 0,
            ulong forbidden_feature_mask = 0
            ) where _ENTITY_ : ENTITY
        {
            _ENTITY_
                entity;

            while ( transform != null )
            {
                entity = transform.gameObject.GetComponent<_ENTITY_>();

                if ( entity != null
                     && entity.HasFeatureMask( required_feature_mask, allowed_feature_mask, forbidden_feature_mask ) )
                {
                    return entity;
                }
                else
                {
                    transform = transform.parent;
                }
            }

            return null;
        }

        // ~~

        public static bool HasContact(
            Vector3 position_vector,
            float radius,
            IS_VALID_COLLIDER_DELEGATE is_valid_collider_delegate,
            uint layer_mask = ~( uint )0,
            Collider[] collider_array = null
            )
        {
            int
                collider_count,
                collider_index;

            if ( collider_array == null )
            {
                collider_array = APPLICATION.ColliderArray;
            }

            collider_count
                = Physics.OverlapSphereNonAlloc(
                      position_vector,
                      radius,
                      collider_array,
                      ( int )layer_mask,
                      QueryTriggerInteraction.Ignore
                      );

            if ( collider_count > 0 )
            {
                for ( collider_index = 0;
                      collider_index < collider_count;
                      ++collider_index )
                {
                    if ( is_valid_collider_delegate == null
                         || is_valid_collider_delegate( collider_array[ collider_index ] ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // ~~

        public static bool FindContact(
            out RaycastHit raycast_hit,
            Vector3 position_vector,
            float radius,
            Vector3 axis_vector,
            float maximum_distance,
            IS_VALID_RAYCAST_HIT_DELEGATE is_valid_raycast_hit_delegate,
            uint layer_mask = ~( uint )0,
            RaycastHit[] raycast_hit_array = null
            )
        {
            float
                closest_raycast_hit_distance,
                raycast_hit_distance;
            int
                closest_raycast_hit_index,
                raycast_hit_count,
                raycast_hit_index;

            if ( raycast_hit_array == null )
            {
                raycast_hit_array = APPLICATION.RaycastHitArray;
            }

            if ( radius > 0.0f )
            {
                raycast_hit_count
                    = Physics.SphereCastNonAlloc(
                          position_vector,
                          radius,
                          axis_vector,
                          raycast_hit_array,
                          maximum_distance,
                          ( int )layer_mask,
                          QueryTriggerInteraction.Ignore
                          );
            }
            else
            {
                raycast_hit_count
                    = Physics.RaycastNonAlloc(
                          new Ray( position_vector, axis_vector ),
                          raycast_hit_array,
                          maximum_distance,
                          ( int )layer_mask,
                          QueryTriggerInteraction.Ignore
                          );
            }

            closest_raycast_hit_index = -1;
            closest_raycast_hit_distance = 0.0f;

            if ( raycast_hit_count > 0 )
            {
                for ( raycast_hit_index = 0;
                      raycast_hit_index < raycast_hit_count;
                      ++raycast_hit_index )
                {
                    raycast_hit_distance = raycast_hit_array[ raycast_hit_index ].distance;

                    if ( ( closest_raycast_hit_index < 0
                           || raycast_hit_distance < closest_raycast_hit_distance )
                         && ( is_valid_raycast_hit_delegate == null
                              || is_valid_raycast_hit_delegate( ref raycast_hit_array[ raycast_hit_index ] ) ) )
                    {
                        closest_raycast_hit_index = raycast_hit_index;
                        closest_raycast_hit_distance = raycast_hit_distance;
                    }
                }
            }

            if ( closest_raycast_hit_index >= 0 )
            {
                raycast_hit = raycast_hit_array[ closest_raycast_hit_index ];

                return true;
            }
            else
            {
                raycast_hit = default( RaycastHit );

                return false;
            }
        }

        // ~~

        public static float GetTerrainHeight(
            Vector3 position_vector
            )
        {
            return Terrain.activeTerrain.SampleHeight( position_vector );
        }

        // -- OPERATIONS

        public void SetActive(
            bool active
            )
        {
            gameObject.SetActive( active );
        }

        // ~~

        public void SetPermanent(
            )
        {
            DontDestroyOnLoad( gameObject );
        }

        // ~~

        public void AddFeatureMask(
            ulong feature_mask
            )
        {
            FeatureMask |= feature_mask;
        }

        // ~~

        public void RemoveFeatureMask(
            ulong feature_mask
            )
        {
            FeatureMask &= ~feature_mask;
        }

        // ~~

        public virtual void SetPositionVector(
            Vector3 position_vector
            )
        {
            transform.position = position_vector;
        }

        // ~~

        public virtual void SetOrientationQuaternion(
            Quaternion orientation_quaternion
            )
        {
            transform.rotation = orientation_quaternion;
        }

        // ~~

        public virtual bool AddDamage(
            int damage
            )
        {
            return false;
        }

        // ~~

        public static void IgnoreLayerCollision(
            uint layer_index
            )
        {
            int
                other_layer_index;

            for ( other_layer_index = 0;
                  other_layer_index < 32;
                  ++other_layer_index )
            {
                Physics.IgnoreLayerCollision( ( int )layer_index, other_layer_index );
            }
        }
    }
}
