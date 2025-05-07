using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CharacterControl : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] public CharacterProfile characterProfile;

    [Header("Lane Settings")]
    [SerializeField] private int laneCount = 3;
    [SerializeField] LayerMask groundLayer;

    public IngameUI ui;
    public PlayerEventListener eventListener; 
    public State state;
    
    private ParticleSystem bubbleParticle;
    public Rigidbody rb;
    private List<Transform> lanes;
    
    private int currentLane;
    private bool isJumping = false;

    public Animator animator;
    private Sequence sequence;
    public Camera mainCam;

    private float jumpForce => characterProfile != null ? characterProfile.JumpForce : 5f;
    private float moveDuration => characterProfile != null ? characterProfile.LaneMoveDuration : 0.2f;

    private void Awake()
    {
        TryGetComponent(out rb);
        TryGetComponent(out animator);
        TryGetComponent(out ui);
        TryGetComponent(out eventListener);
        TryGetComponent(out state);

        lanes = TrackManager.I.lanes;
        
        mainCam=Camera.main;
        sequence=DOTween.Sequence();
        laneCount = Mathf.Max(1, lanes.Count);

        currentLane = laneCount / 2;
        Vector3 startPos = transform.position;
        startPos.x = lanes[currentLane].position.x;
        transform.position = startPos;
        bubbleParticle = GetComponentInChildren<ParticleSystem>();
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    void Start()
    {
        rb.isKinematic = true;
        InGameManager.I.OnStart += GameStart;
    }

    private void Update()
    {
        if (lanes == null)
        {
            return;
        }
        HandleLaneInput();
        HandleJumpInput();
    }

    void GameStart(){
        rb.isKinematic = false;
    }

    private void HandleLaneInput()
    {
        if (DOTween.IsTweening(transform))
        {
            sequence.Kill();
        };

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0)
            {
                currentLane--;
                MoveToLane(currentLane);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < laneCount - 1)
            {
                currentLane++;
                MoveToLane(currentLane);
            }
        }
    }

    private void MoveToLane(int laneIndex)
    {
        Vector3 targetPos = new Vector3(lanes[laneIndex].position.x, transform.position.y, transform.position.z);
        sequence.Append(transform.DOMoveX(targetPos.x, moveDuration).SetEase(Ease.OutQuad));
    }

    private void HandleJumpInput()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJumping)
        {
            DoJump();
        }
        Physics.Raycast(transform.position, -transform.up, out var hit, 0.2f, groundLayer);

        if(hit.point!=null)
        {
            isJumping=false;
        }
    }

    private void DoJump()
    {
        if (rb == null) return;
        bubbleParticle.Play();
        isJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Y 초기화
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool canDamage = true;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("OBSTACLE")){
            if(!canDamage) return;

            state.MinusHealth();
            StartCoroutine(PlayerDamage());
        }

        if(other.tag.Equals("DEADZONE")){
            InGameManager.I.OnGameOver?.Invoke(state.score);
        }
    }

    public SkinnedMeshRenderer[] renderers;

    IEnumerator PlayerDamage(){
        canDamage = false;

        float blinkDuration = 0.1f;
        float totalDuration = 0.5f;
        float elapsed = 0f;

        while (elapsed < totalDuration)
        {
            SetRenderersVisible(false);
            yield return new WaitForSeconds(blinkDuration);
            SetRenderersVisible(true);
            yield return new WaitForSeconds(blinkDuration);

            elapsed += blinkDuration * 2;
        }

        yield return new WaitForSeconds(0.5f);
        canDamage = true;
    }

    void SetRenderersVisible(bool visible)
    {
        foreach (var r in renderers)
        {
            r.enabled = visible;
        }
    }
}