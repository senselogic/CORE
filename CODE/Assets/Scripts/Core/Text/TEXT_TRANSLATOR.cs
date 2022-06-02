// -- IMPORTS

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CORE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace CORE
{
    public class TEXT_TRANSLATOR : MonoBehaviour
    {
        // -- ATTRIBUTES

        public Text
            Text_;
        public TEXT_MESH
            TextMesh;
        public string
            Slug,
            TranslatedText;

        // -- OPERATIONS

        public void Start(
            )
        {
            Text_ = GetComponent<Text>();
            TextMesh = GetComponent<TEXT_MESH>();
        }

        // ~~

        public void SetText(
            string text
            )
        {
            if ( Text_ != null )
            {
                Text_.text = text;
            }

            if ( TextMesh != null )
            {
                TextMesh.text = text;
            }
        }

        // ~~

        public void UpdateText(
            )
        {
            string
                text;

            if ( Slug != "" )
            {
                if ( APPLICATION.FindTranslatedTextBySlug( out text, Slug ) )
                {
                    SetText( text );
                }
            }
            else
            {
                SetText( APPLICATION.GetTranslatedText( TranslatedText ) );
            }
        }

        // ~~

        public static void UpdateTexts(
            )
        {
            foreach ( TEXT_TRANSLATOR text_translator in FindObjectsOfType<TEXT_TRANSLATOR>() )
            {
                text_translator.UpdateText();
            }
        }

        // ~~

        public void OnEnable(
            )
        {
            UpdateText();
        }
    }
}
