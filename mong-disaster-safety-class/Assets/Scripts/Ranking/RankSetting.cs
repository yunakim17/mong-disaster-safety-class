using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankSetting : MonoBehaviour
{
    public Image rankIcon;              // 1~3위
    public TextMeshProUGUI rankText;    // 4위 이상
    public TextMeshProUGUI nicknameText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI scoreText;

    public Sprite goldIcon;
    public Sprite silverIcon;
    public Sprite bronzeIcon;

    public Sprite defaultPanel;
    public Sprite myPanel;

    public string userId;

    public void Setup(int rank, string nickname, int age, int score, string userId)
    {
        nicknameText.text = nickname;
        ageText.text = age.ToString() + "살";
        scoreText.text = score.ToString() + "점";

        this.userId = userId;

        if (rank < 4)
        {
            if (rank == 1) rankIcon.sprite = goldIcon;
            if (rank == 2) rankIcon.sprite = silverIcon;
            if (rank == 3) rankIcon.sprite = bronzeIcon;

            rankIcon.color = new Color(1f, 1f, 1f, 1f);
            rankText.color = new Color(rankText.color.r, rankText.color.g, rankText.color.b, 0f);
        }
        else
        {
            rankText.text = rank.ToString();

            rankIcon.color = new Color(1f, 1f, 1f, 0f);
            rankText.color = new Color(rankText.color.r, rankText.color.g, rankText.color.b, 1f);
        }

        // 내 랭킹 패널 이미지 바꾸기
        Image panelImage = transform.Find("RankPanel").GetComponent<Image>();
        if (userId == PlayerPrefs.GetString("uuid"))
        {
            panelImage.sprite = myPanel;
        }
        else
        {
            panelImage.sprite = defaultPanel;
        }
    }
}
