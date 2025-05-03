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

        if (trackManager == null)
        {
            Debug.LogError("TrackManager is null");
        }
        else
        {
            trackManager.OnDestroyTrack += SpawnObstacleOnTrack;
        }

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
        }while(CheckValid(ods));

        // 랜덤 위치에 설치.
        foreach(var data in ods){
            // 랜덤 위치 가져오기 => 트랙에서 가져오기
            Track selectedTrack = trackManager.lastTrack;
            int index = Random.Range(0,selectedTrack.obstaclePositions.Length);
            
            GameObject o = pooling.GetObject(data);

            Vector3 spawnPosition = new Vector3(selectedTrack.obstaclePositions[index].position.x, selectedTrack.obstaclePositions[index].position.y, spawnPoint.position.z);
            
            o.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            o.transform.SetParent(selectedTrack.transform);
        }
    }

    private bool CheckValid(List<ObstacleData> ods){
        int fullCount = 0;
        
        foreach(var o in ods){
            if(o.type == ObstacleType.FULL) fullCount++;
        }

        return fullCount < 3;
    }

    void SpawnObstacleOnTrack(Vector3 spawnPos)
    {
        // Track track = trackManager.lastTrack;

        // if (track == null || !track.gameObject.activeSelf || 
        //     track.obstaclePositions == null || track.obstaclePositions.Length < 9)
        // {
        //     Debug.LogWarning("올바르지 않은 트랙 또는 장애물 위치 부족");
        //     return;
        // }

        // int lane = Random.Range(0, 3);
        // int startIndex = lane * 3; //0, 3, 6 중 하나
        // int chosenIndex = Random.Range(startIndex, startIndex + 3);

        // Transform obstacleSpawn = track.obstaclePositions[chosenIndex];
        // Instantiate(obstaclePrefab, obstacleSpawn.position, Quaternion.identity, track.transform);
    }
}