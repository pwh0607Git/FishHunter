using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePopUp : MonoBehaviour
{
    public void OnClickResume(){
        // 게임 pause 끝
    }

    public void OnClickRestart(){
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
        
    public void OnClickToLobby(){
        SceneManager.LoadScene("Lobby");
    }
}