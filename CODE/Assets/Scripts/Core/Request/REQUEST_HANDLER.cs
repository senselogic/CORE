// -- IMPORTS

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using CORE;

// -- TYPES

namespace CORE
{
    public class REQUEST_HANDLER : MonoBehaviour
    {
        // -- OPERATIONS

        public IEnumerator<UnityWebRequestAsyncOperation> DoSendGetByteArrayRequest(
            string url,
            HANDLE_BYTE_ARRAY_RESPONSE_DELEGATE handle_byte_array_response_delegate,
            HANDLE_ERROR_RESPONSE_DELEGATE handle_error_response_delegate = null
            )
        {
            UnityWebRequest
                unity_web_request;

            unity_web_request = UnityWebRequest.Get( url );
            yield return unity_web_request.SendWebRequest();

            if ( unity_web_request.result == UnityWebRequest.Result.ConnectionError
                 || unity_web_request.result == UnityWebRequest.Result.ProtocolError )
            {
                if ( handle_error_response_delegate == null )
                {
                    Debug.Log( unity_web_request.error );
                }
                else
                {
                    handle_error_response_delegate( unity_web_request.error );
                }
            }
            else
            {
                handle_byte_array_response_delegate( unity_web_request.downloadHandler.data );
            }
        }

        // ~~

        public void SendGetByteArrayRequest(
            string url,
            HANDLE_BYTE_ARRAY_RESPONSE_DELEGATE handle_byte_array_response_delegate,
            HANDLE_ERROR_RESPONSE_DELEGATE handle_error_response_delegate = null
            )
        {
            StartCoroutine(
                DoSendGetByteArrayRequest(
                    url,
                    handle_byte_array_response_delegate,
                    handle_error_response_delegate
                    )
                );
        }

        // ~~

        public IEnumerator<UnityWebRequestAsyncOperation> DoSendGetTextRequest(
            string url,
            HANDLE_TEXT_RESPONSE_DELEGATE handle_text_response_delegate,
            HANDLE_ERROR_RESPONSE_DELEGATE handle_error_response_delegate
            )
        {
            UnityWebRequest
                unity_web_request;

            unity_web_request = UnityWebRequest.Get( url );
            yield return unity_web_request.SendWebRequest();

            if ( unity_web_request.result == UnityWebRequest.Result.ConnectionError
                 || unity_web_request.result == UnityWebRequest.Result.ProtocolError )
            {
                if ( handle_error_response_delegate == null )
                {
                    Debug.Log( unity_web_request.error );
                }
                else
                {
                    handle_error_response_delegate( unity_web_request.error );
                }
            }
            else
            {
                handle_text_response_delegate( unity_web_request.downloadHandler.text );
            }
        }

        // ~~

        public void SendGetTextRequest(
            string url,
            HANDLE_TEXT_RESPONSE_DELEGATE handle_text_response_delegate,
            HANDLE_ERROR_RESPONSE_DELEGATE handle_error_response_delegate
            )
        {
            StartCoroutine(
                DoSendGetTextRequest(
                    url,
                    handle_text_response_delegate,
                    handle_error_response_delegate
                    )
                );
        }

        // ~~

        public IEnumerator<UnityWebRequestAsyncOperation> DoSendPostJsonTextRequest(
            string url,
            string json_text,
            HANDLE_TEXT_RESPONSE_DELEGATE handle_text_response_delegate,
            HANDLE_ERROR_RESPONSE_DELEGATE handle_error_response_delegate
            )
        {
            byte[]
                json_byte_array;
            UnityWebRequest
                unity_web_request;

            json_byte_array = Encoding.UTF8.GetBytes( json_text );

            unity_web_request = new UnityWebRequest( url, "POST" );
            unity_web_request.uploadHandler = ( UploadHandler ) new UploadHandlerRaw( json_byte_array );
            unity_web_request.downloadHandler = ( DownloadHandler ) new DownloadHandlerBuffer();
            unity_web_request.SetRequestHeader( "Content-Type", "application/json" );
            yield return unity_web_request.SendWebRequest();

            if ( unity_web_request.result == UnityWebRequest.Result.ConnectionError
                 || unity_web_request.result == UnityWebRequest.Result.ProtocolError )
            {
                if ( handle_error_response_delegate == null )
                {
                    Debug.Log( unity_web_request.error );
                }
                else
                {
                    handle_error_response_delegate( unity_web_request.error );
                }
            }
            else
            {
                handle_text_response_delegate( unity_web_request.downloadHandler.text );
            }
        }

        // ~~

        public void SendPostJsonTextRequest(
            string url,
            string json_text,
            HANDLE_TEXT_RESPONSE_DELEGATE handle_text_response_delegate,
            HANDLE_ERROR_RESPONSE_DELEGATE handle_error_response_delegate
            )
        {
            StartCoroutine(
                DoSendPostJsonTextRequest(
                    url,
                    json_text,
                    handle_text_response_delegate,
                    handle_error_response_delegate
                    )
                );
        }
    }
}
