using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] propsPrefabs;

    TrackManager trackManager;      //추후에 싱글톤으로 변경예정

    //test
    float interval = 1f;

    float elapsedTime = 0f;

    void Start()
    {
        trackManager = FindAnyObjectByType<TrackManager>();    
    }
    
    void Update()
    {
        elapsedTime+=Time.deltaTime;

        if(elapsedTime > interval){
            SpawnProps();
            elapsedTime = 0f;
        }
    }

    public void SpawnProps(){
        Track currentTrack = trackManager.lastTrack;
        int i = Random.Range(0, propsPrefabs.Length);
        GameObject prefab = propsPrefabs[i];
        Vector3 spawnPoint = currentTrack.obstaclePositions[Random.Range(0, currentTrack.obstaclePositions.Length)].position;
        GameObject clone = Instantiate(prefab, spawnPoint, Quaternion.identity, currentTrack.transform);
    }
}
