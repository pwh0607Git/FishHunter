using UnityEngine;

public class PropsPooling : ObjectPooling<PropData>
{
    protected override void MakeClone(PropData data){
        PropController prop = Instantiate(data.prefab, gameObject.transform).GetComponent<PropController>();
        
        if(prop == null){
            Debug.Log("prop is null..");
            return;
        }
        prop.Data = data;
        pool[data].Enqueue(prop.gameObject);
        prop.gameObject.SetActive(false);

        prop.OnDisableEvent += ReturnProps;
    }

    public override void ReturnProps(GameObject prop){
        PropData data = prop.GetComponent<PropController>().Data;
        
        if(!pool.ContainsKey(data)) return;
        
        prop.GetComponent<PropController>().OnDisableEvent -= ReturnProps;

        pool[data].Enqueue(prop);
        prop.transform.SetParent(transform);
        prop.SetActive(false);
    }
}