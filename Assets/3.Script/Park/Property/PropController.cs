using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PropController : MonoBehaviour
{
    public PropData Data{get; set;}

    public event UnityAction<GameObject> OnDisenableEvent;

    [Header("자체 프로퍼티")]
    private Animator animator;
    List<IEffect> effects = new();    

    private Vector3 targetPosition;             //다음으로 이동할 위치

    private float minX;
    private float maxX;

    public float minY;
    public float maxY;

    TrackManager trackManager;          //추후에 싱글톤으로 변경예정

    void Start()
    {
        trackManager = FindAnyObjectByType<TrackManager>();
        
        TryGetComponent(out animator);
        
        effects = EffectFactory.CreateEffects(Data);
        SetMinMaxXY();
        SetTargetPosition();
        
    }

    void Update()
    {
        Rotate();
        Wandor();
        CheckReturnPosition();
    }

    private void Death(){
        animator.PlayInFixedTime("DEATH");
        // 풀링으로 반환
    }

    private void Wandor(){
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Data.moveSpeed * Time.deltaTime);
    
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

    private void Rotate(){
        if(targetPosition == null) return;

        Vector3 direction = (targetPosition - transform.localPosition).normalized;
        direction.y = 0f;
        Quaternion lookrot = new();

        if(direction != Vector3.zero) lookrot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookrot, 1080f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.transform.tag.Equals("SHOOT")) return;
        
        Death();
    }

    void OnEnable()
    {
        // player.OnCatchProps += OnCatch;
    }

    void OnDisable()
    {
        // player.OnCatchProps += OnCatch;
    }

    // 플레이어에게 잡혔을 때.
    private void OnCatch(){
        //player.OnCatchProps?.Invoke(effects);       //effect를 보내어 Apply실횅!
    }
    
    void CheckReturnPosition(){
        if(transform.position.z < 0){
            OnDisenableEvent?.Invoke(this.gameObject);
        }
    }
}