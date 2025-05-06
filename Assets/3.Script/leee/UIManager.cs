using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("�г�")]
    public GameObject startPanel;
    public GameObject characterPanel;

    [Header("ĳ���� ����")]
    public Image selectedCharacterPreview;
    public Sprite[] characterSprites; // �� ĳ���� �̹���
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
            Debug.LogWarning($"SelectCharacter: �߸��� �ε��� ���� �õ� (index: {index})");
        }
    }

    public void ConfirmCharacter()
    {
        if (selectedCharacterIndex == -1)
        {
            Debug.Log("ĳ���͸� �����ϼ���!");
            return;
        }

        Debug.Log("���õ� ĳ����: " + selectedCharacterIndex);
        ShowStartPanel();
    }

    public void StartGame()
    {
        if (selectedCharacterIndex == -1)
        {
            Debug.Log("ĳ���͸� ���� �����ϼ���!");
            return;
        }

        Debug.Log("���� ����!");
        // ���� �� ��ȯ �Ǵ� ���� ���� ����
    }
}
