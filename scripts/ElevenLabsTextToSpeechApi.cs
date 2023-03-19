using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// 
// Copyright (c) 2023 Austin Crane, Strange Ape Games
// 

public class ElevenLabsTextToSpeechApi : IElevenLabsTextToSpeechApi
{
    private readonly IAudioFileLoader _audioFileLoader;
    private const string url = "https://api.elevenlabs.io/v1/text-to-speech/";

    public ElevenLabsTextToSpeechApi(IAudioFileLoader audioFileLoader)
    {
        _audioFileLoader = audioFileLoader;
    }
    public IEnumerator GetAudioClip(ElevenLabsVoiceSetting ElevenLabsSettings, string inputText,
        Action<AudioClip> onSuccess, Action<string> onError)
    {
        var requestBody = new SpeechGenerationOptions
        {
            text = inputText,
            voice_settings = new SpeechGenerationOptions.VoiceSettings
            {
                stability = ElevenLabsSettings.Stability,
                similarity_boost = ElevenLabsSettings.Similarity
            }
        };

        var jsonBody = JsonUtility.ToJson(requestBody);
        var request = new UnityWebRequest(string.Concat(url,ElevenLabsSettings.VoiceID), "POST");
        
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonBody));
        request.downloadHandler = new DownloadHandlerBuffer();
        
        request.SetRequestHeader("accept", "audio/mpeg");
        request.SetRequestHeader("xi-api-key", ElevenLabsSettings.Key);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
        {
            onError?.Invoke(request.error);
        }
        else if (request.responseCode == 200)
        {
            var audioClip = _audioFileLoader.LoadAudioClip(request.downloadHandler.data);
            onSuccess?.Invoke(audioClip);
        }
        else
        {
            onError?.Invoke($"Error: {request.responseCode} {request.downloadHandler.text}");
        }
    }
}