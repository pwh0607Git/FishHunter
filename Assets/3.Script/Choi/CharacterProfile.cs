using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterProfile", menuName = "Character/Character Profile")]
public class CharacterProfile : ScriptableObject
{
    [Header("Character Info")]
    [SerializeField] private string characterName = "New Character";

    [Header("Movement Stats")]
    [SerializeField] private float moveSpeed = 5f;          // 앞으로의 속도 (Z축 이동 등 사용 가능)
    [SerializeField] private float jumpForce = 7.5f;         // 점프 힘 (Impulse Force)
    [SerializeField] private float laneMoveDuration = 0.2f;  // DOTween으로 좌우 레인 이동하는 데 걸리는 시간
    [SerializeField] private float attackRange=10f;

    public string CharacterName => characterName;
    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public float LaneMoveDuration => laneMoveDuration;
    public float AttackRange => attackRange;
}