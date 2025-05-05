using System.Collections.Generic;
using UnityEngine;

public class PropertySpawner : MonoBehaviour
{
    [SerializeField] List<PropData> propsDatas;
    private PropsPooling pooling;

    void Start()
    {
        TryGetComponent(out pooling);

        for(int i=0; i<propsDatas.Count; i++){
            pooling.InitPool(propsDatas[i], 5);
        }
        TrackManager.I.RegisterTrackEvent(SpawnProps);
    }

    public void SpawnProps(){
        Track currentTrack = TrackManager.I.lastTrack;
        int i = Random.Range(0, propsDatas.Count);

        PropData data = propsDatas[i];
        GameObject prop = pooling.GetObject(data);

        Vector3 spawnPosition = currentTrack.obstaclePositions[Random.Range(0, currentTrack.obstaclePositions.Length)].position - Vector3.forward * 2;
        
        prop.transform.position = spawnPosition;
        prop.transform.SetParent(currentTrack.transform);    
    }
}
