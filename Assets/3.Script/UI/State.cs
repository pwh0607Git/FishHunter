using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour
{
    public int score;
    public int currentHealth = 3;

    public UnityAction<int> OnChangedScore;
    public UnityAction<int> OnChangedHealth;
    
    public void AddScore(int score){
        this.score += score;
        OnChangedScore?.Invoke(this.score);
    }

    public void AddHealth(){
        currentHealth++;
        OnChangedHealth?.Invoke(this.currentHealth);
    }

    public void MinusHealth(){
        currentHealth--;
        OnChangedHealth?.Invoke(this.currentHealth);
        
        if(currentHealth <= 0){
            InGameManager.I.OnGameOver(this.score);
        }
    }
}
