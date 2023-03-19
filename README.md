# Unofficial Eleven Labs Text-to-Speech API for Unity

This project includes a set of scripts that make it easy to use the Eleven Labs text-to-speech API in Unity. The scripts allow you to convert text to speech and play the requested audio clip in your Unity project in real time (after request processes).

## Getting Started

To get started, you will need to obtain an API key from the Eleven Labs website. Once you have your API key, create a new `ElevenLabs Settings.asset` file in your project and enter your API key in the appropriate field.

You will also have to obtain a Voice ID

## Usage

Here is an example of how you can use the Eleven Labs text-to-speech API in your Unity project:

```csharp

private ElevenLabsVoiceSetting elevenLabsVoiceSetting;
private string inputText = "Greetings";
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

```
## License
- This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- This project is based on the Eleven Labs text-to-speech API.
- The `Mp3FileLoader.cs` script is based on the [Mp3StreamingDecoder](https://github.com/ZhimingDing/mp3-streaming-decoder) project by Zhiming Ding.
