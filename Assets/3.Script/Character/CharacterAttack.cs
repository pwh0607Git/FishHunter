using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Transform point { get; set; }
    private CharacterControl cc;
    Ray ray;
    RaycastHit hit;
    LineRenderer lineRenderer;
    void Awake()
    {
        TryGetComponent(out cc);
        TryGetComponent(out lineRenderer);
        point = transform.FindSlot("_WeaponPoint_");

        if (point == null)
        {
            Debug.LogError("point not found");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cc.mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, cc.characterProfile.AttackRange))
            {
                Debug.Log("hit");
                Attack();
            }
            else
            {
                lineRenderer.SetPosition(0,point.position);
                lineRenderer.SetPosition(1,ray.direction*cc.characterProfile.AttackRange);
            }
        }
    }

    void Attack()
    {
        Debug.Log(hit.transform.gameObject.name);
        lineRenderer.SetPosition(0,point.position);
        lineRenderer.SetPosition(1,hit.point);
    }
}
