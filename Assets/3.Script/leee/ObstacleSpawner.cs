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

        if (track == null)
        {
            Debug.LogWarning("Ʈ���� ��ֹ��� ��ġ�� �� �����ϴ�: ��ġ ��ġ ����");
            return;
        }

        if (!track.gameObject.activeSelf) return;
        // ��ֹ� ��ġ�� �� �ϳ��� ���� �����ؼ� ����

        if (track.obstaclePositions != null && track.obstaclePositions.Length > 0)
        {
            int index = Random.Range(0, track.obstaclePositions.Length);
            Transform obstacleSpawn = track.obstaclePositions[index];

            GameObject obj = Instantiate(obstaclePrefab, obstacleSpawn.position, Quaternion.identity, track.transform);
        }
        return; // ã������ ����
    }
}