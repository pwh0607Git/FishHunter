using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField nicknameInput;

    public void OnStartButtonClicked()
    {
        string nickname = nicknameInput.text;

        if (!string.IsNullOrEmpty(nickname))
        {
            PlayerData.Nickname = nickname;
            SceneManager.LoadScene("Lobby");  // 나중에 로비 씬 생성할 예정
        }
        else
        {
            Debug.LogWarning("닉네임을 입력해주세요.");
        }
    }
}
