using UnityEngine;

public enum ObstacleType{
    NONE, SINGLE, FULL
}

[CreateAssetMenu(menuName = "Obstacle")]
public class ObstacleData : ScriptableObject
{
    public ObstacleType type;
    public GameObject model;
}