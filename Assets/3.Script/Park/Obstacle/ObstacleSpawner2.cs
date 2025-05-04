using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner2 : MonoBehaviour
{
    [SerializeField] List<ObstacleData> obstacleDatas;
    
    [SerializeField] private TrackManager trackManager;
    [SerializeField] private Transform spawnPoint;
    private ObstaclePooling pooling;

    [SerializeField] private float spawnInterval = 3f;
    void Start()
    {
        trackManager = FindObjectOfType<TrackManager>();

        TryGetComponent(out pooling);

        foreach(var data in obstacleDatas){
            pooling.InitPool(data, 6);
        }
    }

    float elapsed = 0f;

    void Update()
    {
        elapsed += Time.deltaTime;

        if(elapsed >= spawnInterval){
            elapsed = 0f;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle(){
        int count = trackManager.lastTrack.obstaclePositions.Length;

        List<ObstacleData> ods = new();

        do{
            ods.Clear();
            for(int i=0;i<count;i++){
                int index = Random.Range(0, obstacleDatas.Count);
                ods.Add(obstacleDatas[index]);
            }
        }while(!CheckValid(ods));

        for(int i=0; i < ods.Count; i++){
            // 랜덤 위치 가져오기 => 트랙에서 가져오기
            Track selectedTrack = trackManager.lastTrack;
            
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