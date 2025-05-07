using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    Sequence sequence;
    Image panel;

    void Awake()
    {
        sequence = DOTween.Sequence();
        panel = GetComponentInChildren<Image>();
        panel.rectTransform.localScale = Vector3.zero;
    }
    public void OnUIClick()
    {
        if(Time.timeScale==0)
        {
            return;
        }
        StartCoroutine(GamePause());
    }

    public void Onresume()
    {
        StartCoroutine(GameResume());
    }

    public void OnLobby()
    {
        //변수초기화코드
        SceneManager.LoadScene("Lobby");//로비씬이름
        Time.timeScale=1f;
    }

    public void OnRestart()
    {
        //변수초기화코드
        SceneManager.LoadScene("2.PlayerScene");//플레이 씬 이름
        Time.timeScale=1f;
    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();// 에디터
#else
        Application.Quit();// 빌드
#endif
    }

    public IEnumerator GamePause()
    {
        panel.rectTransform.DOScale(Vector3.one,0.3f);
        yield return new WaitForSeconds(0.3f);
        Time.timeScale=0f;
    }

    public IEnumerator GameResume()
    {
        Time.timeScale=1f;
        panel.rectTransform.DOScale(Vector3.zero, 0.3f);
        yield return null;
    }
}