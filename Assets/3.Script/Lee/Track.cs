using UnityEngine;

public class Track : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float destroyZ = -10f;

    [SerializeField] public Transform[] obstaclePositions;

    private TrackManager manager;

    [SerializeField] float headPoint;

    private Collider col;

    private void Start()
    {
        manager = FindAnyObjectByType<TrackManager>();
    
        TryGetComponent(out col);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.fixedDeltaTime);

        headPoint = col.bounds.min.z;

        if (headPoint <= destroyZ)
        {
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