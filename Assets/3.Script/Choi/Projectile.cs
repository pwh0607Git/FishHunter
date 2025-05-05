using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="")
        {
            collision.transform.SetParent(null);
            collision.collider.enabled=false;
        }
    }
}
