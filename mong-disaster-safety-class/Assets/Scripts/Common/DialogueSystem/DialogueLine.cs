using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public int sequence;
    public string speaker;
    public string dialogue_text;
    public string voice_path;

    public AudioClip GetVoice()
    {
        if (!string.IsNullOrEmpty(voice_path))
        {
            return Resources.Load<AudioClip>("Audio/" + voice_path);
        }
        return null;
    }
}
