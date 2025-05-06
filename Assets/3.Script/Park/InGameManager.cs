using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState{
    PLAYING, PAUSE, OVER
}

public class InGameManager : BehaviourSingleton<InGameManager>
{
    protected override bool IsDontDestroy() => false;

    public GameState State{get; set;}

    public UnityAction<int> OnGameOver;              // 게임 오버 이벤트.
    public UnityAction<bool> OnPause;                 // 게임 중지 이벤트
    public UnityAction OnStart;                 // 게임 중지 이벤트

    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] GameObject OverPanel;

    void OnEnable()
    {
        Debug.Log("Enable");
        OverPanel.SetActive(false);
        countDown.enabled = false;

        State = GameState.PAUSE;
        StartCoroutine(StartCount_Co());

        SceneManager.sceneLoaded += OnSceneLoaded;
        OnGameOver += GameOver;
        OnPause += GamePause;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        OnGameOver -= GameOver;
        OnPause -= GamePause;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    IEnumerator StartCount_Co(){
        yield return new WaitForEndOfFrame();

        countDown.enabled = true;

        for(int i=3; i>0; i--){
            countDown.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }

        countDown.text = $"GO!";
        countDown.enabled = false;
        OnStart?.Invoke();
        State = GameState.PLAYING;
    }

    void GameOver(int score){
        //게임 오버 로직 수행[시퀀스]
        StartCoroutine(GameOver_Co(score));
    }
    
    [SerializeField] TextMeshProUGUI finalScore;

    IEnumerator GameOver_Co(int score){
        State = GameState.OVER;
        
        LocalScoreManager.SaveScore(PlayerData.Nickname, score);

        yield return new WaitForSeconds(1f);

        OverPanel.SetActive(true);
        finalScore.text = $"{score}";

        yield return new WaitForSeconds(2f);
        
        SceneManager.LoadScene("Lobby");
    }

    void GamePause(bool on){
        if(on){
            State = GameState.PAUSE;
        }else{
            State = GameState.PLAYING;
        }
    }
}