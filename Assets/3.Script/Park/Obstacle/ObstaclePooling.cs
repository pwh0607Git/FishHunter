using UnityEngine;

public class ObstaclePooling : ObjectPooling<ObstacleData>
{
    protected override void MakeClone(ObstacleData data){
        GameObject clone = Instantiate(data.model, transform);
        pool[data].Enqueue(clone);
        clone.SetActive(false);

        //리턴 이벤트.
    }

    public override void ReturnProps(GameObject prop){
        Obstacle data = prop.GetComponent<Obstacle>();

        if(!pool.ContainsKey(data.Data)) return;

        data.OnDisenableEventS -= ReturnProps;
        pool[data.Data].Enqueue(prop);
        prop.transform.SetParent(transform);
        prop.SetActive(false);
    }
}