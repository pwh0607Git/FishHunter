using System.Collections.Generic;
using UnityEngine;

public enum EffectType{
    SCOREUP, SCOREDOWN, HEARTUP, HEARTDOWN, INTERFERE  
}

[CreateAssetMenu(menuName = "Property/Props")]
public class PropData : ScriptableObject
{
    public float moveSpeed;
    public int amount;
    public GameObject prefab;                   // 모델
    public List<EffectType> effects;
}