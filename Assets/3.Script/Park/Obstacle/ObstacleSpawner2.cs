using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner2 : MonoBehaviour
{
    [SerializeField] List<ObstacleData> obstacleDatas;
    
    [SerializeField] private Transform spawnPoint;
    private ObstaclePooling pooling;

    void Start()
    {
        TryGetComponent(out pooling);

        foreach(var data in obstacleDatas){
            pooling.InitPool(data, 6);
        }
    
        TrackManager.I.RegisterTrackEvent(SpawnObstacle);
    }

    private void SpawnObstacle(){
        int count = TrackManager.I.lastTrack.obstaclePositions.Length;

        List<ObstacleData> ods = new();

        do{
            ods.Clear();
            for(int i=0;i<count;i++){
                int index = Random.Range(0, obstacleDatas.Count);
                ods.Add(obstacleDatas[index]);
            }
        }while(!CheckValid(ods));

        for(int i=0; i < ods.Count; i++){
            Track selectedTrack = TrackManager.I.lastTrack;
            
            GameObject o = pooling.GetObject(ods[i]);

            Vector3 spawnPosition = new Vector3(selectedTrack.obstaclePositions[i].position.x, selectedTrack.obstaclePositions[i].position.y, spawnPoint.position.z);
            
            o.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            o.transform.SetParent(selectedTrack.transform);
        }
    }

    private bool CheckValid(List<ObstacleData> ods){
        int fullCount = 0;
        
        foreach(var o in ods){
            if(o.type.Equals(ObstacleType.FULL)) fullCount++;
        }

        return fullCount < 3;
    }
}