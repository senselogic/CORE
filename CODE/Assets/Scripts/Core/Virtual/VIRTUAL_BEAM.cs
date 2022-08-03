// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CORE;

// -- TYPES

namespace CORE
{
    public class VIRTUAL_BEAM : MonoBehaviour
    {
        // -- ATTRIBUTES

        public int
            PlayerLayerMask = 1 << 8;
        public float
            RayLength = 120.0f,
            Length;
        public Ray
            Ray_;
        public RaycastHit[]
            RaycastHitArray;
        public int
            RaycastHitCount;
        public bool
            HasContact;
        public List<GameObject>
            OldContactGameObjectList,
            ContactGameObjectList;
        public GameObject
            ContactGameObject;
        public Vector3
            ContactPositionVector;
        public LineRenderer
            LineRenderer;
        public VIRTUAL_HAND_DEVICE
            HandVirtualDevice;

        // -- OPERATIONS

        public void OnEnable(
            )
        {
            RaycastHitArray = new RaycastHit[ 20 ];
            OldContactGameObjectList = new List<GameObject>();
            ContactGameObjectList = new List<GameObject>();
            ContactGameObject = null;
        }

        // ~~

        public void FindContacts(
            )
        {
            int
                closest_raycast_hit_index,
                raycast_hit_index;

            Ray_.origin = transform.position;
            Ray_.direction = transform.forward;
            RaycastHitCount = Physics.RaycastNonAlloc( Ray_, RaycastHitArray, RayLength, PlayerLayerMask );

            if ( RaycastHitCount > 0 )
            {
                closest_raycast_hit_index = 0;

                for ( raycast_hit_index = 1;
                      raycast_hit_index < RaycastHitCount;
                      ++raycast_hit_index )
                {
                    if ( RaycastHitArray[ raycast_hit_index ].distance
                         < RaycastHitArray[ closest_raycast_hit_index ].distance )
                    {
                        closest_raycast_hit_index = raycast_hit_index;
                    }
                }

                HasContact = true;
                ContactGameObject = RaycastHitArray[ closest_raycast_hit_index ].collider.gameObject;
                Length = RaycastHitArray[ closest_raycast_hit_index ].distance;
            }
            else
            {
                HasContact = false;
                ContactGameObject = null;
                Length = RayLength;
            }

            ContactPositionVector = transform.position + transform.forward * Length;
        }

        // ~~

        public void SendPointerEnterEvent(
            GameObject game_object
            )
        {
            ExecuteEvents.Execute(
                game_object,
                new PointerEventData( EventSystem.current ),
                ExecuteEvents.pointerEnterHandler
                );
        }

        // ~~

        public void SendPointerExitEvent(
            GameObject game_object
            )
        {
            ExecuteEvents.Execute(
                game_object,
                new PointerEventData( EventSystem.current ),
                ExecuteEvents.pointerExitHandler
                );
        }

        // ~~

        public void SendPointerClickEvent(
            GameObject game_object
            )
        {
            ExecuteEvents.Execute(
                game_object,
                new PointerEventData( EventSystem.current ),
                ExecuteEvents.pointerClickHandler
                );
        }

        // ~~

        public void SendPointerUpEvent(
            GameObject game_object
            )
        {
            ExecuteEvents.Execute(
                game_object,
                new PointerEventData( EventSystem.current ),
                ExecuteEvents.pointerUpHandler
                );
        }

        // ~~

        public void SendEvents(
            )
        {
            int
                contact_game_object_index,
                old_contact_game_object_index,
                raycast_hit_index;
            GameObject
                contact_game_object,
                old_contact_game_object;
            List<GameObject>
                old_contact_game_object_list;

            for ( raycast_hit_index = 0;
                  raycast_hit_index < RaycastHitCount;
                  ++raycast_hit_index )
            {
                contact_game_object = RaycastHitArray[ raycast_hit_index ].collider.gameObject;

                if ( RaycastHitArray[ raycast_hit_index ].distance < Length + 0.1f )
                {
                    ContactGameObjectList.Add( contact_game_object );
                }
            }

            for ( old_contact_game_object_index = 0;
                  old_contact_game_object_index < OldContactGameObjectList.Count;
                  ++old_contact_game_object_index )
            {
                old_contact_game_object = OldContactGameObjectList[ old_contact_game_object_index ];

                if ( !ContactGameObjectList.Contains( old_contact_game_object ) )
                {
                    SendPointerExitEvent( old_contact_game_object );
                }
            }

            for ( contact_game_object_index = 0;
                  contact_game_object_index < ContactGameObjectList.Count;
                  ++contact_game_object_index )
            {
                contact_game_object = ContactGameObjectList[ contact_game_object_index ];

                if ( !OldContactGameObjectList.Contains( contact_game_object ) )
                {
                    SendPointerEnterEvent( contact_game_object );
                }

                if ( HandVirtualDevice.TriggerButton.IsJustPressed )
                {
                    SendPointerClickEvent( contact_game_object );
                }

                if ( HandVirtualDevice.TriggerButton.IsJustReleased )
                {
                    SendPointerUpEvent( contact_game_object );
                }
            }

            old_contact_game_object_list = OldContactGameObjectList;
            OldContactGameObjectList = ContactGameObjectList;
            ContactGameObjectList = old_contact_game_object_list;
            ContactGameObjectList.Clear();
        }

        // ~~

        public virtual void DrawLine(
            )
        {
            if ( LineRenderer != null )
            {
                LineRenderer.SetPosition( 0, transform.position );
                LineRenderer.SetPosition( 1, ContactPositionVector );
            }
        }

        // ~~

        public void Update(
            )
        {
            FindContacts();
            SendEvents();
            DrawLine();
        }
    }
}
