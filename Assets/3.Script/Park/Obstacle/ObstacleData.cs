using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType{
    NONE, SINGE, FULL
}

[CreateAssetMenu(menuName = "Obstacle")]
public class ObstacleData : ScriptableObject
{
    public ObstacleType type;
    public GameObject model;
}