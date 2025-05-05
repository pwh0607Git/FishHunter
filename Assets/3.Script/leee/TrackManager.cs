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

    public Track lastTrack;            

    private int poolSize = 8;   //

    private void Start()
    {
        OnDestroyTrack += ReuseTrack;

        // Ʈ�� ������Ʈ �̸� ����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(trackPrefab, transform);
            obj.SetActive(false);
            Track track = obj.GetComponent<Track>();
            trackPool.Enqueue(track);
        }

        // ���� Ʈ�� ��ġ
        for (int i = 0; i < 4; i++)
        {
            Vector3 position = spawnPosition.position + Vector3.back * 10f * i;
            ReuseTrack(position);
        }
    }

    void ReuseTrack(Vector3 position) //Track ����
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

        }
    }

    // Ʈ���� ���� �� �ٽ� Ǯ�� �ֱ�
    public void ReturnToPool(Track track)
    {
        track.gameObject.SetActive(false);
        trackPool.Enqueue(track); //ť ���� �����͸� �߰��Ѵ�.
    }
}