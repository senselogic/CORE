// -- IMPORTS

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using CORE;

// -- TYPES

namespace CORE
{
    public class VIRTUAL_BEAM_BUTTON : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        // -- OPERATIONS

        public virtual void OnPointerEnter(
            PointerEventData pointer_event_data
            )
        {
        }

        // ~~

        public virtual void OnPointerExit(
            PointerEventData pointer_event_data
            )
        {
        }

        // ~~

        public virtual void OnPointerClick(
            PointerEventData pointer_event_data
            )
        {
        }
    }
}
