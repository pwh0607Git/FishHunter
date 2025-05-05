using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;         // ������ ��ֹ� ������
    public float spawnInterval = 1.5f;        // ���� ���� (��)

    [SerializeField] private TrackManager trackManager; //TrackManager�� ������ ����

    void Start()
    {
        trackManager = FindObjectOfType<TrackManager>();

        if (trackManager == null)
        {
            Debug.LogError("TrackManager�� ã�� �� �����ϴ�!");
        }

        else
        {
            trackManager.OnDestroyTrack += SpawnObstacleOnTrack;
        }

    }

    void SpawnObstacleOnTrack(Vector3 spawnPos)
    {
        // Ʈ�� ������Ʈ �� �ش� ��ġ�� ���� ����� Ʈ���� ã��
        Track track = trackManager.lastTrack;

        if (track == null || !track.gameObject.activeSelf || 
            track.obstaclePositions == null || track.obstaclePositions.Length < 9)
        {
            Debug.LogWarning("올바르지 않은 트랙 또는 장애물 위치 부족");
            return;
        }

        int lane = Random.Range(0, 3);
        int startIndex = lane * 3; //0, 3, 6 중 하나
        int chosenIndex = Random.Range(startIndex, startIndex + 3);

        Transform obstacleSpawn = track.obstaclePositions[chosenIndex];
        Instantiate(obstaclePrefab, obstacleSpawn.position, Quaternion.identity, track.transform);

        //if (!track.gameObject.activeSelf) return;

        //if (track.obstaclePositions != null && track.obstaclePositions.Length > 0)
        //{
        //    int index = Random.Range(0, track.obstaclePositions.Length);
        //    Transform obstacleSpawn = track.obstaclePositions[index];

        //    GameObject obj = Instantiate(obstaclePrefab, obstacleSpawn.position, Quaternion.identity, track.transform);
        //}
        //return; 
    }
}