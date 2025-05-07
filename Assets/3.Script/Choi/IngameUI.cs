using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [SerializeField] IconDatas iconDatas;
    private CharacterControl player;
    [SerializeField] TextMeshProUGUI text_score;

    [SerializeField] Transform healthbar;

    [SerializeField] GameObject inGamePopup;
    [SerializeField] GameObject interferePanel;

    List<Image> healths = new List<Image>();

    void Awake()
    {
        for (int i=0;i < 3;i++)
        {
            GameObject o = Instantiate(iconDatas.health, healthbar);
            healths.Add(o.GetComponent<Image>());
        }

        player = GetComponentInParent<CharacterControl>();
    }

    void Start()
    {
        inGamePopup.SetActive(false);
        interferePanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            TogglePopup();
        }
    }

    void TogglePopup(){
        if(inGamePopup.activeSelf){
            InGameManager.I.OnPause(false);
            inGamePopup.SetActive(false);
        }else{
            InGameManager.I.OnPause(true);
            inGamePopup.SetActive(true);
        }
    }

    public void OnGetscore(int score)
    {
        Debug.Log("스코어 갱신!");
        text_score.text=$"Point: <size=120%>{score}</size>";
    }

    public void OnChagedHealth(int count){
        Debug.Log($"현재 체력 : {count}");
        for(int i=0;i<healths.Count;i++){
            healths[i].enabled = false;
        }

        for(int i=0; i<count; i++){
            healths[i].enabled = true;
        }
    }

    public void Interfere(){
        StartCoroutine(Interfere_Co(2.5f));
    }

    IEnumerator Interfere_Co(float duration){

        Image img = interferePanel.GetComponentInChildren<Image>();
        interferePanel.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Color originalColor = img.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration - 0.5f);
            img.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        img.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        interferePanel.SetActive(false);
    }

    void OnEnable()
    {
        StartCoroutine(Registers_Co());
    }

    void OnDisable()
    {
        player.state.OnChangedScore -= OnGetscore;
        player.state.OnChangedHealth -= OnChagedHealth;
    }

    IEnumerator Registers_Co(){
        yield return new WaitForSeconds(1);
        player.state.OnChangedScore += OnGetscore;
        player.state.OnChangedHealth += OnChagedHealth;
    }
}
