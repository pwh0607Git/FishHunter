using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TrackManager : BehaviourSingleton<TrackManager>
{
    protected override bool IsDontDestroy() => false;

    public float scrollSpeed = 5f;

    [SerializeField] GameObject trackPrefab;
    [SerializeField] public Transform spawnPosition;
    public List<Transform> lanes;

    public UnityAction<Vector3, bool> OnDestroyTrack;
    private UnityAction OnCreateTrack;               // 트랙이 생성되었을 때.

    private Queue<Track> trackPool = new ();
    public Track lastTrack;            

    [SerializeField] int startCount = 3;

    private int poolSize = 8;
    

    private void Start()
    {
        OnDestroyTrack += SpawnTrack;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(trackPrefab, transform);
            obj.SetActive(false);
            Track track = obj.GetComponent<Track>();
            trackPool.Enqueue(track);
        }
        
        StartGame();
    }

    void StartGame(){
      for (int i = 0; i < startCount; i++)
        {
            Vector3 position = spawnPosition.position + Vector3.back * 20f * i;
            SpawnTrack(position, true);
        }
    }

    void SpawnTrack(Vector3 position, bool isStart)
    {
        if (trackPool.Count > 0)
        {
            Track track = trackPool.Dequeue();
            track.gameObject.SetActive(true);
            track.transform.position = position;
            track.moveSpeed = scrollSpeed;

            lastTrack = track;

            if(!isStart){
                OnCreateTrack?.Invoke();
            }
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