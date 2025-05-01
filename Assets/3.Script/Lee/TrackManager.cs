using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public Transform cameraTransform;

    [SerializeField] GameObject trackPrefab;
    [SerializeField] public Transform spawnPosition;
    public float spawnInterval = 1.5f;

    public UnityAction<Vector3> OnDestroyTrack;

    private Queue<Track> trackPool = new Queue<Track>();

    public Track lastTrack;             //현재 가장 마지막에 생성된 트랙
    
    private int poolSize = 8;   //

    private void Start()
    {
        OnDestroyTrack += ReuseTrack;

        // 트랙 오브젝트 미리 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(trackPrefab, transform);
            obj.SetActive(false);
            Track track = obj.GetComponent<Track>();
            trackPool.Enqueue(track);
        }

        // 시작 트랙 배치
        for (int i = 0; i < 4; i++)
        {
            Vector3 position = spawnPosition.position + Vector3.back * 10f * i;
            ReuseTrack(position);
        }
    }

    void ReuseTrack(Vector3 position) //Track 재사용
    {
        if (trackPool.Count > 0)
        {
            Track track = trackPool.Dequeue();
            track.gameObject.SetActive(true);
            track.transform.position = position;
            track.moveSpeed = scrollSpeed;
            lastTrack = track;
        }
        else
        {
            Debug.LogWarning("트랙 풀이 부족합니다!");
        }
    }

    // 트랙이 꺼질 때 다시 풀에 넣기
    public void ReturnToPool(Track track)
    {
        track.gameObject.SetActive(false);
        trackPool.Enqueue(track); //큐 끝에 데이터를 추가한다.
    }
}
