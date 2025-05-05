using UnityEngine;

public class ObstaclePooling : ObjectPooling<ObstacleData>
{
    protected override void MakeClone(ObstacleData data){
        Obstacle clone = Instantiate(data.model, transform).GetComponent<Obstacle>();
        clone.Data = data;
        pool[data].Enqueue(clone.gameObject);
        clone.gameObject.SetActive(false);

        //리턴 이벤트.
        clone.OnDisableEvent += ReturnProps;
    }

    public override void ReturnProps(GameObject prop){
        Obstacle data = prop.GetComponent<Obstacle>();

        if(!pool.ContainsKey(data.Data)) return;

        data.OnDisableEvent -= ReturnProps;
        
        pool[data.Data].Enqueue(prop);
        prop.transform.SetParent(transform);
        prop.SetActive(false);
    }
}