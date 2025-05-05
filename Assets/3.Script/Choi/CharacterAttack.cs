using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CharacterAttack : MonoBehaviour
{
    private Transform point { get; set; }
    private CharacterControl cc;
    private Ray ray;
    private RaycastHit hit;
    private LineRenderer lineRenderer;

    [Header("Projectile Settings")]
    public Projectile projectileObject;           // Inspector에서 할당
    public float projectileSpeed = 0.1f;
    public float stayTimeAtTarget = 0.5f;

    private bool isAttacking = false;

    void Awake()
    {
        TryGetComponent(out cc);
        TryGetComponent(out lineRenderer);
        point = transform.FindSlot("_WeaponPoint_");

        if (point == null)
        {
            Debug.LogError("point not found");
        }

        if (projectileObject == null)
        {
            Debug.LogError("Projectile object is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            ray = cc.mainCam.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out hit, cc.characterProfile.AttackRange);

            Vector3 targetPosition = isHit ? hit.point : ray.origin + ray.direction * cc.characterProfile.AttackRange;

            StartCoroutine(FireAndReturnProjectile(targetPosition, isHit));
        }
    }

    IEnumerator FireAndReturnProjectile(Vector3 targetPosition, bool hitSomething)
    {
        isAttacking = true;

        // Projectile 초기 위치 세팅 및 방향
        projectileObject.transform.position = point.position;
        projectileObject.transform.LookAt(targetPosition);

        // 발사 애니메이션
        yield return projectileObject.transform.DOMove(targetPosition, projectileSpeed)
            .SetEase(Ease.Linear)
            .WaitForCompletion();

        // 도착 후 대기
        yield return new WaitForSeconds(stayTimeAtTarget);

        // 회수 애니메이션
        yield return projectileObject.transform.DOMove(point.position, projectileSpeed)
            .SetEase(Ease.InOutSine)
            .WaitForCompletion();

        isAttacking = false;
    }
}
