// -- IMPORTS

using TMPro;
using UnityEngine;
using CORE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace CORE
{
    public class TEXT_SCALER : ENTITY
    {
        // -- OPERATIONS

        void ScaleText(
            )
        {
            float
                text_scale,
                x_scale,
                y_scale;
            Rect
                text_rect;
            TEXT_MESH
                text_mesh;
            Vector2
                text_size_vector;

            text_mesh = GetComponent<TEXT_MESH>();

            text_rect = text_mesh.rectTransform.rect;
            text_size_vector = text_mesh.GetPreferredValues( text_mesh.text );
            text_scale = 1.0f;

            if ( text_size_vector.x > text_rect.width )
            {
                x_scale = text_rect.width / text_size_vector.x;

                if ( x_scale < text_scale )
                {
                    text_scale = x_scale;
                }
            }

            if ( text_size_vector.y > text_rect.height )
            {
                y_scale = text_rect.height / text_size_vector.y;

                if ( y_scale < text_scale )
                {
                    text_scale = y_scale;
                }
            }

            if ( text_scale != 1.0f )
            {
                transform.localScale = new Vector3( text_scale, text_scale, 1.0f );
            }
        }

        // ~~

        void OnEnable(
            )
        {
            ScaleText();
        }

        // ~~

        void OnRectTransformDimensionsChange(
            )
        {
            ScaleText();
        }
    }
}
