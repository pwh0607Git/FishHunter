using TMPro;
using UnityEngine;

public class RankEntryUI : MonoBehaviour
{
    public TextMeshProUGUI rankNum;
    public TextMeshProUGUI nicknameText;
    public TextMeshProUGUI scoreText;

    public void Setup(int i, string nickname, int score)
    {
        rankNum.text = i.ToString();
        nicknameText.text = nickname;
        scoreText.text = score.ToString();
    }
}