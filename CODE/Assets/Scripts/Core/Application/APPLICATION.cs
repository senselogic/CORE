// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using CORE;

// -- TYPES

namespace CORE
{
    public class APPLICATION : TEMPORAL_ENTITY
    {
        // -- CONSTANTS

        public const int
            RaycastHitCount = 128;
        public const int
            ColliderCount = 128;

        // -- ATTRIBUTES

        public static float
            ScaledTimeStep,
            UnscaledTimeStep;
        public static float[]
            TimeScaleArray = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
        public static float[]
            VolumeScaleArray = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
        public static Collider[]
            ColliderArray;
        public static RaycastHit[]
            RaycastHitArray;
        public static string
            LanguageCode = "en";
        public static Dictionary<string, string>
            TextBySlugDictionary = new Dictionary<string, string>();

        // -- INQUIRIES

        public static float GetUnscaledTime(
            )
        {
            return Time.unscaledTime;
        }

        // ~~

        public static float GetTimeStep(
            int time_scale_index
            )
        {
            if ( time_scale_index == 0 )
            {
                return ScaledTimeStep * TimeScaleArray[ time_scale_index ];
            }
            else
            {
                return UnscaledTimeStep * TimeScaleArray[ time_scale_index ];
            }
        }

        // ~~

        public static bool IsPaused(
            int time_scale_index = 0
            )
        {
            return GetTimeStep( time_scale_index ) == 0.0f;
        }

        // ~~

        public static string GetTranslatedText(
            string text
            )
        {
            return text.GetTranslatedText( LanguageCode );
        }

        // ~~

        public static string GetTranslatedTextBySlug(
            string slug
            )
        {
            return GetTranslatedText( TextBySlugDictionary[ slug ] );
        }

        // ~~

        public static bool FindTranslatedTextBySlug(
            out string translated_text,
            string slug
            )
        {
            if ( TextBySlugDictionary.ContainsKey( slug ) )
            {
                translated_text = GetTranslatedTextBySlug( slug );

                return true;
            }
            else
            {
                Debug.LogWarning( "Translated text not found : " + slug );
                translated_text = "";

                return false;
            }
        }

        // -- OPERATIONS

        public static void UpdateTexts(
            )
        {
            TEXT_TRANSLATOR.UpdateTexts();
        }

        // ~~

        public static void SetLanguageCode(
            string language_code
            )
        {
            LanguageCode = language_code;

            UpdateTexts();
        }

        // ~~

        public void OnEnable(
            )
        {
            ColliderArray = new Collider[ ColliderCount ];
            RaycastHitArray = new RaycastHit[ RaycastHitCount ];
        }

        // ~~

        public void Update(
            )
        {
            ScaledTimeStep = Time.deltaTime;
            UnscaledTimeStep = Time.unscaledDeltaTime;

            if ( UnscaledTimeStep > 0.1f )
            {
                UnscaledTimeStep = 0.1f;
            }
        }

        // ~~

        public static void SetFrameRate(
            int frame_rate
            )
        {
             QualitySettings.vSyncCount = 0;
             Application.targetFrameRate = frame_rate;
        }

        // ~~

        public static void Pause(
            int time_scale_index = 0
            )
        {
            TimeScaleArray[ time_scale_index ] = 0.0f;

            if ( time_scale_index == 0 )
            {
                Time.timeScale = 0.0f;
            }
        }

        // ~~

        public static void Resume(
            int time_scale_index = 0,
            float time_scale = 1.0f
            )
        {
            TimeScaleArray[ time_scale_index ] = time_scale;

            if ( time_scale_index == 0 )
            {
                Time.timeScale = time_scale;
            }
        }

        // ~~

        public static void Exit(
            )
        {
            Application.Quit();
        }

        // ~~

        public static void HideCursor(
            )
        {
            #if !UNITY_EDITOR
                Cursor.visible = false;
            #endif
        }

        // ~~

        public static void ShowCursor(
            )
        {
            Cursor.visible = true;
        }
    }
}
