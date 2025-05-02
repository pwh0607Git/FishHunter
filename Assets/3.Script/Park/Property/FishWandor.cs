using System.Linq;
using UnityEngine;

public class FishWandor : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private Vector3 targetPosition;             //다음으로 이동할 위치

    private float minX;
    private float maxX;

    public float minY;
    public float maxY;

    TrackManager trackManager;          //추후에 싱글톤으로 변경예정

    void Start()
    {
        trackManager = FindAnyObjectByType<TrackManager>();
        SetMinMaxXY();
        SetTargetPosition();
    }

    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    
        if(Vector3.Distance(transform.localPosition, targetPosition) < 0.1f){
            // 목적지 도착.
            SetTargetPosition();
        }
    }

    private void SetMinMaxXY(){
        Track currentTrack = trackManager.lastTrack;

        maxX = currentTrack.obstaclePositions.Max(p => p.localPosition.x);
        minX = currentTrack.obstaclePositions.Min(p => p.localPosition.x);

        SetTargetPosition();
    }

    private void SetTargetPosition(){
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetPosition = new Vector3(randomX, randomY, transform.localPosition.z);
    }
}