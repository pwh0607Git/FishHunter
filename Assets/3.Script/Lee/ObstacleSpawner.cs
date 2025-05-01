using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;         // 생성할 장애물 프리팹
    public float spawnInterval = 1.5f;        // 생성 간격 (초)

    [SerializeField] private TrackManager trackManager; //TrackManager를 저장할 변수

    void Start()
    {
        trackManager = FindObjectOfType<TrackManager>(); 

        if (trackManager == null)
        {
            Debug.LogError("TrackManager를 찾을 수 없습니다!");
        }

        else
        {
            trackManager.OnDestroyTrack += SpawnObstacleOnTrack;
        }

    }

    void SpawnObstacleOnTrack(Vector3 spawnPos)
    {
        // 트랙 오브젝트 중 해당 위치에 가장 가까운 트랙을 찾음
        Track track = trackManager.lastTrack;

        if (track == null)
        {
            Debug.LogWarning("트랙에 장애물을 배치할 수 없습니다: 위치 일치 실패");
            return;
        }

        if (!track.gameObject.activeSelf) return;
            // 장애물 위치들 중 하나를 랜덤 선택해서 생성
    
        if (track.obstaclePositions != null && track.obstaclePositions.Length > 0)
        {
            int index = Random.Range(0, track.obstaclePositions.Length);
            Transform obstacleSpawn = track.obstaclePositions[index];

            GameObject obj = Instantiate(obstaclePrefab, obstacleSpawn.position, Quaternion.identity, track.transform);
        }
        return; // 찾았으면 종료
    }
}
