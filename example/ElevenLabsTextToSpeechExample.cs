using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class ElevenLabsTextToSpeechExample : MonoBehaviour
{
    [SerializeField] private ElevenLabsVoiceSetting elevenLabsVoiceSetting;
    [SerializeField] public string inputText = "Greetings";
    private AudioSource audioSource;
    private IElevenLabsTextToSpeechApi ttsApi;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        IAudioFileLoader audioFileLoader = new Mp3FileLoader();
        ttsApi = new ElevenLabsTextToSpeechApi(audioFileLoader);
        StartCoroutine(ttsApi.GetAudioClip(elevenLabsVoiceSetting, inputText, OnAudioClipLoaded, OnAudioClipLoadError));
    }

    private void OnAudioClipLoaded(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void OnAudioClipLoadError(string errorMessage)
    {
        Debug.LogError($"Error loading AudioClip: {errorMessage}");
    }
}