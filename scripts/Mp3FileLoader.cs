using System;
using System.IO;
using UnityEngine;
using NAudio.Wave;

public class Mp3FileLoader : IAudioFileLoader
{
    public AudioClip LoadAudioClip(byte[] audioData)
    {
        AudioClip audioClip = null;

        using var mp3Stream = new MemoryStream(audioData);
        using var mp3Reader = new Mp3FileReader(mp3Stream);
        using var waveStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader);
        
        var sampleRate = waveStream.WaveFormat.SampleRate;
        var channels = waveStream.WaveFormat.Channels;
        var bitsPerSample = waveStream.WaveFormat.BitsPerSample;
        var bytesPerSample = bitsPerSample / 8;
        var sampleLength = (int)(waveStream.Length / bytesPerSample);

        var samples = new float[sampleLength];
        var buffer = new byte[sampleLength * bytesPerSample];
        waveStream.Read(buffer, 0, buffer.Length);

        for (var i = 0; i < sampleLength; ++i)
        {
            var pcmValue = BitConverter.ToInt16(buffer, i * bytesPerSample);
            samples[i] = (float)pcmValue / short.MaxValue;
        }

        audioClip = AudioClip.Create("GeneratedAudio", sampleLength, channels, sampleRate, false);
        audioClip.SetData(samples, 0);

        return audioClip;
    }
}