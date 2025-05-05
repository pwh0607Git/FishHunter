using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="")//물고기 태그
        {
            collision.transform.SetParent(null);
            collision.collider.enabled=false;
        }
    }
}
