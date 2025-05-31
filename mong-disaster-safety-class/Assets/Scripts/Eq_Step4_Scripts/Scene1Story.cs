using UnityEngine;

public class Scene1Story : MonoBehaviour
{
    public GameObject phone;
    private bool phoneShown = false;

    public void OnNextDialogue()
    {
        if (!phoneShown)
        {
            phone.SetActive(true);
            phoneShown = true;
            Debug.Log("핸드폰 등장!");
        }
    }
}
