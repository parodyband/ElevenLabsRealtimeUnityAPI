using UnityEngine;

[CreateAssetMenu(fileName = "ElevenLabs Settings", menuName = "ElevenLabs/ElevenLabs Settings", order = 1)]
public class ElevenLabsVoiceSetting : ScriptableObject
{
    [SerializeField]
    private string _key;
    [SerializeField]
    private string _voiceID;
    [SerializeField]
    private int _stability;
    [SerializeField]
    private int _similarity;

    public string Key => _key;
    public string VoiceID => _voiceID;
    public int Stability => _stability;
    public int Similarity => _similarity;
}