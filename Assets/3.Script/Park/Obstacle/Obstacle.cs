using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleData data;

    public ObstacleData Data{get => data; set => data = value;}

    public UnityAction<GameObject> OnDisableEvent;
    
    [SerializeField] float minScaleY;
    [SerializeField] float maxScaleY;

    void Update()
    {
        CheckReturnPosition();
    }

    void OnEnable()
    {
        if(Data == null) return;

        if(Data.type.Equals(ObstacleType.SINGLE)){
            foreach(Transform child in transform){
                SetScale(child);
            }
        }
    }

    void SetScale(Transform mesh){  
        float rnd = Random.Range(minScaleY, maxScaleY);
        mesh.localScale = new Vector3(1,rnd,1);
    }

    void CheckReturnPosition(){
        if(transform.position.z <= 0.5f){
            OnDisableEvent?.Invoke(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("MISSILE")){
            OnDisableEvent?.Invoke(this.gameObject);
        }
    }
}