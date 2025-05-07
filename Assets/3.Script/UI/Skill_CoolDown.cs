using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skill_CoolDown : MonoBehaviour
{
    public Image coolDownImage;
    void Start()
    {
        coolDownImage.fillAmount = 0f;
    }

    public void StartCool(float duration){
        Debug.Log("스킬 쿨중...");
        StartCoroutine(StartCool_Co(duration));
    }

    IEnumerator StartCool_Co(float duration){
        coolDownImage.fillAmount = 1f;

        float elapsedTime = 0f;

        while(elapsedTime < duration){
            Debug.Log("스킬 쿨중...");
            elapsedTime += Time.deltaTime;
            coolDownImage.fillAmount = 1 - (elapsedTime / duration);
            yield return null;
        }

        coolDownImage.fillAmount = 0f;
    }
}