using UnityEngine;

public class PlayerEventListener : MonoBehaviour
{
    private CharacterControl player;

    // Prop이 가지고 있는 효과를 적용한다.
    void Start()
    {
        TryGetComponent(out player);
    }

    public void ApplyScoreUp(int amount){
        player.state.AddScore(amount);
    }

    public void ApplyScoreDown(int amount){
        // UI 상의 점수 amount 만큼 감소
    }

    
    public void ApplyHeartUp(){
        // 체력 1개 증가
        player.state.AddHealth();
    }

    
    public void ApplyHeartDown(){
        // 체력 1개 감소
        Debug.Log("hp 감소");
        player.state.MinusHealth();
    }

    public void ApplyInterfere(){
        // 오징어 먹물 => 2초 동안 지속
        player.ui.Interfere();
    }
}