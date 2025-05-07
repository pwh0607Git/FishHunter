using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DiverAbility : MonoBehaviour
{
    private Transform point { get; set; }
    private CharacterControl cc;
    private Ray ray;
    private RaycastHit hit;
    private LineRenderer lineRenderer;

    [Header("Projectile Settings")]
    public float projectileSpeed = 0.1f;
    public float stayTimeAtTarget = 0.5f;

    [SerializeField] GameObject harpoon;        //작살
    [SerializeField] Transform harpoonOrigin;

    private Vector3 harpoonOriginPosition;
    private Quaternion harpoonOriginQuaternion;

    private bool isAttacking = false;

    void Awake()
    {
        TryGetComponent(out cc);
        TryGetComponent(out lineRenderer);
        
        point = harpoonOrigin;
        harpoonOriginPosition = harpoon.transform.localPosition;
        harpoonOriginQuaternion = harpoon.transform.localRotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            ray = cc.mainCam.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out hit, cc.characterProfile.AttackRange);

            Vector3 targetPosition = isHit ? hit.point : ray.origin + ray.direction * cc.characterProfile.AttackRange;

            StartCoroutine(FireAndReturnProjectile(targetPosition));
        }

        UpdateLine();
    }

    IEnumerator FireAndReturnProjectile(Vector3 targetPosition)
    {
        isAttacking = true;

        // Projectile 초기 위치 세팅 및 방향
        harpoon.transform.position = point.position;
        harpoon.transform.LookAt(targetPosition);

        // 발사 애니메이션
        yield return harpoon.transform.DOMove(targetPosition, projectileSpeed)
            .SetEase(Ease.Linear)
            .WaitForCompletion();

        // 도착 후 대기
        yield return new WaitForSeconds(stayTimeAtTarget);
        
        harpoon.transform.localPosition = Vector3.zero;
        harpoon.transform.localRotation = harpoonOriginQuaternion;

        isAttacking = false;
    }

    void UpdateLine(){
        Debug.Log("line Update");
        if (harpoon == null) return;

        lineRenderer.SetPosition(0, harpoonOrigin.position);
        lineRenderer.SetPosition(1, harpoon.transform.position);
    }
}