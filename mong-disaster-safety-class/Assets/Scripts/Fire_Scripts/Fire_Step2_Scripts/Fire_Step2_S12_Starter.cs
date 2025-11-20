using UnityEngine;

public class Fire_Step2_S12_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject HowToUse_1, HowToUse_2, HowToUse_3, HowToUse_4;
    public GameObject IMG;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step2/Fire_Step2_S12_dialogues",
            "",
            "Fire_Step2_S13");
    }

    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Fire_Step2_S12")
        {
            int currentIdx = dialogueManager.GetCurrentLineIndex();

            IMG.SetActive(currentIdx == 0);
            HowToUse_1?.SetActive(currentIdx == 1);
            HowToUse_2?.SetActive(currentIdx == 2);
            HowToUse_3?.SetActive(currentIdx == 3);
            HowToUse_4?.SetActive(currentIdx == 4);
        }
    }
}
