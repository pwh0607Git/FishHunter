using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_score;
    [SerializeField] TextMeshProUGUI text_time;
    
    [SerializeField] Transform healthbar;
    [SerializeField] Transform missilebar;
    
    [SerializeField] GameObject health;
    [SerializeField] GameObject missile;
    [SerializeField] Button menubutton;

    [SerializeField] int currentHealth = 3;
    [SerializeField] int currentMissile = 3;

    List<Image> healths=new List<Image>();
    List<Image> missiles=new List<Image>();

    void Awake()
    {
        for (int i=0;i<currentHealth;i++)
        {
            GameObject o = Instantiate(health, healthbar);
            healths.Add(o.GetComponent<Image>());
        }
        for (int i=0;i<currentMissile;i++)
        {
            GameObject o = Instantiate(missile, missilebar);
            missiles.Add(o.GetComponent<Image>());
            Color col=missiles[i].color;
            col.a=0;
            missiles[i].color=col;
        }
        currentMissile=0;
    }

    void OnEnable()
    {

    }

    void Update()
    {
        text_time.text=Time.time.ToString();
    }

    public void Ongetscore(int score)
    {
        text_score.text=$"Point: <size=120%>{score}</size>";
    }


    public void Onhealthchange(bool isDamage)
    {
        if(currentHealth>3||currentHealth<0) return;

        if(isDamage==true)
        {
            currentHealth--;
            Color col=healths[currentHealth].color;
            col.a=0;
            healths[currentHealth].color=col;
        }
        else
        {
            currentHealth++;
            Color col=healths[currentHealth].color;
            col.a=255;
            healths[currentHealth].color=col;
        }
    }

    public void Onmissilelaunch(bool isLaunch)
    {
         if(currentMissile>3||currentMissile<0) return;

        if(isLaunch==true)
        {
            currentMissile--;
            Color col=missiles[currentMissile].color;
            col.a=0;
            missiles[currentMissile].color=col;
        }
        else
        {
            currentMissile++;
            Color col=missiles[currentMissile].color;
            col.a=255;
            missiles[currentMissile].color=col;
        }
    }

    void OnDisable()
    {
        //이벤트 해제
    }
}
