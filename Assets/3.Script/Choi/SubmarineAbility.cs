using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SubmarineAbility : MonoBehaviour
{
    private Transform w_point { get; set; }
    private Transform m_point { get; set; }
    private CharacterControl cc;
    private Ray ray;
    private RaycastHit hit;
    private LineRenderer lineRenderer;

    [Header("Projectile Settings")]
    public Projectile projectile;

    public Projectile missile;           // Inspector에서 할당
    public float projectileSpeed = 0.1f;
    public float stayTimeAtTarget = 0.5f;

    private bool isAttacking = false;

    private bool isFiring = false;

    public float missileSpeed = 0.05f;             // 미사일은 더 빠름
    public float missileReloadDelay = 0.5f;        // 미사일 소멸 후 재장전 시간


    void Awake()
    {
        TryGetComponent(out cc);
        TryGetComponent(out lineRenderer);
        w_point = transform.FindSlot("_WeaponPoint_");
        m_point = transform.FindSlot("_MissilePoint_");

        if (w_point == null || m_point == null)
        {
            Debug.LogError("point not found");
        }

        if (projectile == null || missile == null)
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

        if (Input.GetMouseButtonDown(1) && isFiring == false)
        {
            ray = cc.mainCam.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out hit, cc.characterProfile.AttackRange);

            Vector3 targetPosition = isHit ? hit.point : ray.origin + ray.direction * cc.characterProfile.AttackRange;

            StartCoroutine(FireMissile(targetPosition));
        }
    }

    IEnumerator FireAndReturnProjectile(Vector3 targetPosition, bool hitSomething)
    {
        isAttacking = true;

        // Projectile 초기 위치 세팅 및 방향
        projectile.transform.position = w_point.position;
        projectile.transform.LookAt(targetPosition);

        // 발사 애니메이션
        yield return projectile.transform.DOMove(targetPosition, projectileSpeed)
            .SetEase(Ease.Linear)
            .WaitForCompletion();

        // 도착 후 대기
        yield return new WaitForSeconds(stayTimeAtTarget);

        // 회수 애니메이션
        yield return projectile.transform.DOMove(w_point.position, projectileSpeed)
            .SetEase(Ease.InOutSine)
            .WaitForCompletion();

        isAttacking = false;
    }

    IEnumerator FireMissile(Vector3 targetPosition)
    {
        isFiring = true;

        // 미사일 초기화
        missile.transform.position = m_point.position;
        missile.transform.LookAt(targetPosition);

        // 목표 지점으로 Tween 이동
        yield return missile.transform.DOMove(targetPosition, missileSpeed)
            .SetEase(Ease.Linear)
            .WaitForCompletion();

        // 도달 후 바로 미사일 회수
        yield return missile.transform.DOMove(m_point.position, missileSpeed)
            .SetEase(Ease.InOutSine)
            .WaitForCompletion();

        // 재장전 딜레이
        yield return new WaitForSeconds(missileReloadDelay);

        isFiring = false;
    }
}
