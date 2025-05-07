using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SubmarineAbility : MonoBehaviour
{
    private Transform point { get; set; }
    private CharacterControl cc;
    private Ray ray;
    private RaycastHit hit;
    private LineRenderer lineRenderer;

    [SerializeField] GameObject harpoon;        //작살
    [SerializeField] Transform harpoonOrigin;

    public float projectileSpeed = 0.1f;
    public float stayTimeAtTarget = 0.5f;

    private bool isAttacking = false;

    private bool isFiring = false;

    [SerializeField] Transform missileOrigin;
    public GameObject missile;           // Inspector에서 할당

    public float missileSpeed = 0.05f;             // 미사일은 더 빠름
    public float missileReloadDelay = 10f;        // 미사일 소멸 후 재장전 시간


    private Vector3 harpoonOriginPosition;
    private Quaternion harpoonOriginQuaternion;

    void Awake()
    {
        TryGetComponent(out cc);
        TryGetComponent(out lineRenderer);
            
        missile.SetActive(false);
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

        if (Input.GetMouseButtonDown(1) && isFiring == false)
        {
            ray = cc.mainCam.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out hit, cc.characterProfile.AttackRange);

            StartCoroutine(FireMissile());
            GetComponentInChildren<Skill_CoolDown>().StartCool(stayTimeAtTarget);
        }

        UpdateLine();
    }
    
    IEnumerator FireAndReturnProjectile(Vector3 targetPosition) 
    {
        isAttacking = true;

        harpoon.transform.position = point.position;
        harpoon.transform.LookAt(targetPosition);
        lineRenderer.enabled = true;
        
        yield return harpoon.transform.DOMove(targetPosition, projectileSpeed)
            .SetEase(Ease.Linear)
            .WaitForCompletion();

        // 도착 후 대기
        yield return new WaitForSeconds(stayTimeAtTarget);
        
        harpoon.transform.localPosition = Vector3.zero;
        harpoon.transform.localRotation = harpoonOriginQuaternion;
        
        lineRenderer.enabled = false;
        isAttacking = false;
    }

    IEnumerator FireMissile()
    {
        isFiring = true;
        
        missile.SetActive(true);
        missile.transform.position = missileOrigin.position;

        yield return missile.transform.DOMove(missileOrigin.position + Vector3.forward * cc.characterProfile.AttackRange, missileSpeed)
            .SetEase(Ease.Linear)
            .WaitForCompletion();

        missile.transform.position = missileOrigin.position;
        missile.SetActive(false);

        yield return new WaitForSeconds(missileReloadDelay);
        isFiring = false;
    }

    void UpdateLine(){
        if (harpoon == null) return;

        lineRenderer.SetPosition(0, harpoonOrigin.position);
        lineRenderer.SetPosition(1, harpoon.transform.position);
    }
}
