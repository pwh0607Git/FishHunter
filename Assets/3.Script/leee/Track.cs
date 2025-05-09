﻿using UnityEngine;

public class Track : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public float destroyZ = -15f;

    [SerializeField] public Transform[] obstaclePositions;

    private TrackManager manager;

    [SerializeField] float headPoint;

    private Collider col;

    private void Start()
    {
        manager = FindAnyObjectByType<TrackManager>();
    
        TryGetComponent(out col);
    }

    void Update()
    {
        if(InGameManager.I.State.Equals(GameState.PAUSE) || InGameManager.I.State.Equals(GameState.OVER)) return;
        transform.Translate(Vector3.back * moveSpeed * Time.fixedDeltaTime);

        if (transform.position.z < destroyZ)
        {
            manager.ReturnToPool(this);
        }
    }

    private void OnDisable()
    {
        if (manager != null)
        {
            manager.OnDestroyTrack?.Invoke(manager.spawnPosition.position, false);
        }
    }
}