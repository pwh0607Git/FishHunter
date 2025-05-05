using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TrackManager : BehaviourSingleton<TrackManager>
{
    protected override bool IsDontDestroy() => false;

    public float scrollSpeed = 5f;
    public Transform cameraTransform;

    [SerializeField] GameObject trackPrefab;
    [SerializeField] public Transform spawnPosition;
    
    public float spawnInterval = 1.5f;

    public UnityAction<Vector3> OnDestroyTrack;
    private UnityAction OnCreateTrack;               // 트랙이 생성되었을 때.

    private Queue<Track> trackPool = new Queue<Track>();

    public Track lastTrack;            

    private int poolSize = 8;   //
    [SerializeField] int startCount = 3;

    private void Start()
    {
        OnDestroyTrack += ReuseTrack;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(trackPrefab, transform);
            obj.SetActive(false);
            Track track = obj.GetComponent<Track>();
            trackPool.Enqueue(track);
        }

        for (int i = 0; i < startCount; i++)
        {
            Vector3 position = spawnPosition.position + Vector3.back * 20f * i;
            ReuseTrack(position);
        }
    }

    void ReuseTrack(Vector3 position)
    {
        if (trackPool.Count > 0)
        {
            Track track = trackPool.Dequeue();
            track.gameObject.SetActive(true);
            track.transform.position = position;
            track.moveSpeed = scrollSpeed;
            lastTrack = track;

            OnCreateTrack?.Invoke();
        }
    }

    public void ReturnToPool(Track track)
    {
        track.gameObject.SetActive(false);
        trackPool.Enqueue(track);
    }

    public void RegisterTrackEvent(UnityAction action){
        OnCreateTrack += action;
    }

    public void UnregisterTrackEvent(UnityAction action){
        OnCreateTrack -= action;
    }
}