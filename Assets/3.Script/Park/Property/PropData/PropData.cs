using System.Collections.Generic;
using UnityEngine;

public enum PropsType{
    NORMAL, OBSTACLE, ITEM
}

public enum EffectType{
    SCOREUP, SCOREDOWN, HEARTUP, HEARTDOWN, INTERFERE  
}

[CreateAssetMenu(menuName = "Property/Props")]
public class PropData : ScriptableObject
{
    public PropsType type;
    public float moveSpeed;
    public int score;
    public PropsController model;
    public List<EffectType> effects;
}