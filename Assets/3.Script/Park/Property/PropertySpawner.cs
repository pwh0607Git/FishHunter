using System.Collections.Generic;
using UnityEngine;

public class PropertySpawner : MonoBehaviour
{
    [SerializeField] List<PropData> propsDatas;
    private PropsPooling pooling;
    private TrackManager trackManager;      //추후에 싱글톤으로 변경예정

    //test
    float interval = 3f;

    float elapsedTime = 0f;

    void Start()
    {
        trackManager = FindAnyObjectByType<TrackManager>();   
        TryGetComponent(out pooling);

        for(int i=0; i<propsDatas.Count; i++){
            pooling.InitPool(propsDatas[i], 5);
        }
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
        int i = Random.Range(0, propsDatas.Count);

        PropData data = propsDatas[i];
        GameObject prop = pooling.GetObject(data);

        Vector3 spawnPosition = currentTrack.obstaclePositions[Random.Range(0, currentTrack.obstaclePositions.Length)].position;
        
        prop.transform.position = spawnPosition;
        prop.transform.SetParent(currentTrack.transform);    
    }
}
