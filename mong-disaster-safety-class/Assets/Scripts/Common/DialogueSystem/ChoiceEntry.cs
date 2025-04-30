using UnityEngine;

[System.Serializable]
public class ChoiceEntry
{
    public int sequence;
    public string speaker;
    public string question;
    public string option1;
    public string option2;
    public int correct_option;
    public string feedback;
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