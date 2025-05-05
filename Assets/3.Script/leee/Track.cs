using System.Collections;
using UnityEngine;

public class Track : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float destroyZ = -5f;

    [SerializeField] public Transform[] obstaclePositions;

    private TrackManager manager;

    private void Start()
    {
        manager = FindAnyObjectByType<TrackManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (transform.position.z < destroyZ)
        {
            // ��Ȱ��ȭ �� Ǯ�� ��ȯ
            manager.ReturnToPool(this);
        }
    }

    private void OnDisable()
    {
        if (manager != null)
        {
            manager.OnDestroyTrack?.Invoke(manager.spawnPosition.position);
        }
    }
}