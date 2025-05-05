using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LobbyUI : MonoBehaviour
{
    public TextMeshProUGUI nicknameText;
    public GameObject[] characterPrefabs;
    public Transform previewSpot;
    private GameObject currentPreview;
    private int selectedIndex = 0;

    void Start()
    {
        nicknameText.text = "Nickname: " + PlayerData.Nickname;
        ShowCharacter(0);  // 기본 첫 캐릭터 표시
    }

    public void SelectCharacter(int index)
    {
        selectedIndex = index;
        ShowCharacter(index);
    }

    void ShowCharacter(int index)
{
    if (currentPreview != null)
    {
        Destroy(currentPreview);
    }

    

    currentPreview = Instantiate(characterPrefabs[index], previewSpot.position, Quaternion.Euler(0, 180, 0));
    currentPreview.transform.SetParent(previewSpot);

    
}

    public void OnStartGameClicked()
    {
        PlayerData.SelectedCharacterIndex = selectedIndex;
        SceneManager.LoadScene("InGame");
    }
}
