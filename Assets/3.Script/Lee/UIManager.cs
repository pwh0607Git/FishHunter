using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("패널")]
    public GameObject startPanel;
    public GameObject characterPanel;

    [Header("캐릭터 관련")]
    public Image selectedCharacterPreview;
    public Sprite[] characterSprites; // 각 캐릭터 이미지
    private int selectedCharacterIndex = -1;

    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
        characterPanel.SetActive(false);
    }

    public void ShowCharacterPanel()
    {
        startPanel.SetActive(false);
        characterPanel.SetActive(true);
    }

    public void SelectCharacter(int index)
    {
        if (index >= 0 && index < characterSprites.Length)
        {
            selectedCharacterIndex = index;
            selectedCharacterPreview.sprite = characterSprites[index];
        }
        else
        {
            Debug.LogWarning($"SelectCharacter: 잘못된 인덱스 접근 시도 (index: {index})");
        }
    }

    public void ConfirmCharacter()
    {
        if (selectedCharacterIndex == -1)
        {
            Debug.Log("캐릭터를 선택하세요!");
            return;
        }

        Debug.Log("선택된 캐릭터: " + selectedCharacterIndex);
        ShowStartPanel();
    }

    public void StartGame()
    {
        if (selectedCharacterIndex == -1)
        {
            Debug.Log("캐릭터를 먼저 선택하세요!");
            return;
        }

        Debug.Log("게임 시작!");
        // 게임 씬 전환 또는 게임 시작 로직
    }
}
