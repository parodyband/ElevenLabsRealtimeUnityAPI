[System.Serializable]
public class SpeechGenerationOptions
{
    public string text;
    public VoiceSettings voice_settings;

    [System.Serializable]
    public class VoiceSettings
    {
        public int stability;
        public int similarity_boost;
    }
}