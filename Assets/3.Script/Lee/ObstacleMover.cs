using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveSpeed = 5f;                // 이동 속도
    public float destroyZ = -5f;                // 카메라 뒤쪽에 도달하면 제거

    void Update()
    {
        // z- 방향으로 이동
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // 일정 위치 지나면 제거
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
