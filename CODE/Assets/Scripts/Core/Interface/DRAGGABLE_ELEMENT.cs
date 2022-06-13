// -- IMPORTS

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CORE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace CORE
{
    public class DRAG_MOVABLE : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // -- ATTRIBUTES

        // -- INQUIRIES

        public Vector3 GetLocalTranslationVector(
            PointerEventData pointer_event_data
            )
        {
            return new Vector3( pointer_event_data.delta.x, pointer_event_data.delta.y, 0.0f ) / transform.lossyScale.x;
        }

        // -- OPERATIONS

        public void OnBeginDrag(
            PointerEventData pointer_event_data
            )
        {
            Vector3
                local_translation_vector;

            local_translation_vector = GetLocalTranslationVector( pointer_event_data );

            Debug.Log( "*** OnBeginDrag" );
            Debug.Log( "pointer_event_data.delta : " + pointer_event_data.delta );
            Debug.Log( "local_translation_vector : " + local_translation_vector );
        }

        // ~~

        public void OnDrag(
            PointerEventData pointer_event_data
            )
        {
            Vector3
                local_translation_vector;

            local_translation_vector = GetLocalTranslationVector( pointer_event_data );

            Debug.Log( "*** OnBeginDrag" );
            Debug.Log( "pointer_event_data.delta : " + pointer_event_data.delta );
            Debug.Log( "local_translation_vector : " + local_translation_vector );
        }

        // ~~

        public void OnEndDrag(
            PointerEventData pointer_event_data
            )
        {
            Vector3
                local_translation_vector;

            local_translation_vector = GetLocalTranslationVector( pointer_event_data );

            Debug.Log( "*** OnBeginDrag" );
            Debug.Log( "pointer_event_data.delta : " + pointer_event_data.delta );
            Debug.Log( "local_translation_vector : " + local_translation_vector );
        }
    }
}
