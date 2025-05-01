using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float moveSpeed = 5f;

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
