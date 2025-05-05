using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] characterPrefabs;

    void Start()
    {
        int index = PlayerData.SelectedCharacterIndex;
        Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);
        Debug.Log("Nickname: " + PlayerData.Nickname);
    }
}
