using UnityEngine;

public class ObstaclePooling : ObjectPooling<ObstacleData>
{
    protected override void MakeClone(ObstacleData data){
        Obstacle clone = Instantiate(data.model, transform).GetComponent<Obstacle>();
        clone.Data = data;
        pool[data].Enqueue(clone.gameObject);
        clone.gameObject.SetActive(false);

        clone.OnDisableEvent += ReturnProps;
    }

    public override void ReturnProps(GameObject prop){
        Debug.Log("장애물 리턴");
        Obstacle data = prop.GetComponent<Obstacle>();

        if(!pool.ContainsKey(data.Data)) return;
        
        pool[data.Data].Enqueue(prop);
        prop.transform.SetParent(transform);
        prop.SetActive(false);
    }
}