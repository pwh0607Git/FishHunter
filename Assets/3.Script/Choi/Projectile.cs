using UnityEngine;

public enum ProjectileType {Proejctile, Missile}

public class Projectile : MonoBehaviour
{
    public ProjectileType projectileType;
    void OnCollisionEnter(Collision collision)
    {
        if(projectileType==ProjectileType.Proejctile)
        {
            if(collision.collider.tag=="")//물고기 태그 넣기, 팩토리 스폰 방식에 맞춰서 개조
            {
                collision.transform.SetParent(null);
                collision.collider.enabled=false;
            }
        }

        if(projectileType==ProjectileType.Missile)//맞으면 일단 파괴라 조건문 이 이상 없음, 팩토리 스폰 방식에 맞춰서 개조
        {
            collision.transform.SetParent(null);
            Destroy(collision.gameObject);
        }
    }
}
