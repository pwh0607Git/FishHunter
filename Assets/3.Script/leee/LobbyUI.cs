using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class LobbyUI : MonoBehaviour
{
    public TextMeshProUGUI nicknameText;
    public CharacterProfile[] profiles;
    
    public Transform previewSpot;
    private GameObject currentPreview;
    private int selectedIndex = 0;

    void Start()
    {
        nicknameText.text = "Nickname: " + PlayerData.Nickname;
        CharacterSession.I.playerNickname = PlayerData.Nickname;
        ShowCharacter(0);
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
        currentPreview = Instantiate(profiles[index].lobbyModel, previewSpot.position, Quaternion.Euler(0, 180, 0));
        
        CharacterSession.I.currentProfile = profiles[index];

        currentPreview.transform.SetParent(previewSpot);
    }

    public void OnStartGameClicked()
    {
        PlayerData.SelectedCharacterIndex = selectedIndex;
        SceneManager.LoadScene("InGame");
    }
    [SerializeField] GameObject rankPanel;
    public Transform rankingContent;
    public GameObject rankEntryPrefab; 


    public void OnClickRankBtn(){
        if(!rankPanel.activeSelf){
            rankPanel.SetActive(true);
        }else{
            rankPanel.SetActive(false);
            return;
        }

        foreach (Transform child in rankingContent)
        {
            Destroy(child.gameObject);
        }

        List<ScoreEntry> topScores = LocalScoreManager.GetSortedScoresDescending();
        
        Debug.Log(topScores.Count);

        for(int i=0;i<topScores.Count; i++){
            GameObject obj = Instantiate(rankEntryPrefab, rankingContent);
            RankEntryUI entryUI = obj.GetComponent<RankEntryUI>();
            entryUI.Setup(i+1, topScores[i].nickname, topScores[i].score);
        }

    }
}