using System.Collections.Generic;

[System.Serializable]
public class DialogueData
{
    public List<DialogueLine> lines;

    public DialogueLine GetLine(int index)
    {
        if (index < 0 || index >= lines.Count)
        {
            return null;
        }
        return lines[index];
    }
}
