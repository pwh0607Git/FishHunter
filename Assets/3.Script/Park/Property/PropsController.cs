using System.Collections.Generic;
using UnityEngine;

public class PropsController : MonoBehaviour
{
    [SerializeField] PropData data;

    //Player가 잡화를 잡았을 때 이벤트
    List<IEffect> effects = new();
    
    void Start()
    {
        effects = EffectFactory.CreateEffects(data);
    }

    void OnEnable()
    {
        // player.OnCatchProps += OnCatch;
    }

    void OnDisable()
    {
        // player.OnCatchProps -= OnCatch;
    }

    // 플레이어에게 잡혔을 때.
    private void OnCatch(){
        //player.OnCatchProps?.Invoke(effects);       //effect를 보내어 Apply실횅!
    }
}
