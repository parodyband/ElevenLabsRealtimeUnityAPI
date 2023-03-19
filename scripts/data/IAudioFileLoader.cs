using UnityEngine;

public interface IAudioFileLoader
{
    AudioClip LoadAudioClip(byte[] audioData);
}