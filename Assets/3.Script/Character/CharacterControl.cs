using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class CharacterControl : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] public CharacterProfile characterProfile;

    [Header("Lane Settings")]
    [SerializeField] private int laneCount = 3;
    [SerializeField] LayerMask groundLayer;

    public Rigidbody rb;
    public List<Transform> lanes;
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
        mainCam=Camera.main;
        laneCount = Mathf.Max(1, lanes.Count); // 최소 1 보장

        currentLane = laneCount / 2; ;
        Vector3 startPos = transform.position;
        startPos.x = lanes[currentLane].position.x;
        transform.position = startPos;
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

    private void HandleLaneInput()
    {
        if (DOTween.IsTweening(transform))
        {
            sequence.Kill();
        }
        ;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0)
            {
                currentLane--;
                MoveToLane(currentLane);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
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

        isJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Y 초기화
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}
