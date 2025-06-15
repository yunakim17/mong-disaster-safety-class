using UnityEngine;
using UnityEngine.UI;

public class TouchSoundPlayer : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() =>
            {
                TouchSoundManager.Instance?.PlayClickSound();
            });
        }
    }
}
