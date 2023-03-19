using System;
using System.Collections;
using UnityEngine;

public interface IElevenLabsTextToSpeechApi
{
    IEnumerator GetAudioClip(ElevenLabsVoiceSetting ElevenLabsSettings, string inputText,
        Action<AudioClip> onSuccess, Action<string> onError);
}