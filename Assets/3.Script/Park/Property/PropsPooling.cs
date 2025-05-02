using System.Collections.Generic;
using UnityEngine;

public class PropsPooling : MonoBehaviour
{
    // Key : 프리팹, Value는 풀링을 구현할 큐 => 큐에 있는 GameObject는 인스턴스이다 => 즉, 프리팹과 다름.
    private Dictionary<PropData, Queue<GameObject>> propsPool = new();

    public void InitPool(PropData data, int maxSize = 5){
        if(propsPool.ContainsKey(data)) return;           //이미 포함되어있는 프리팹은 무시

        propsPool.Add(data, new());
        for(int i=0; i<maxSize; i++){
            MakeClone(data);
        }
    }

    public GameObject GetProps(PropData data){
        if(data == null) return null;
        if(!propsPool.ContainsKey(data)) return null;

        if(propsPool[data].Count <= 0){
            MakeClone(data);
        }

        GameObject prop = propsPool[data].Dequeue();
        prop.SetActive(true);

        return prop;
    }

    private void MakeClone(PropData data){
        PropController prop = Instantiate(data.prefab, transform).GetComponent<PropController>();
        prop.Data = data;
        propsPool[data].Enqueue(prop.gameObject);
        prop.gameObject.SetActive(false);

        prop.OnDisenableEvent += ReturnProps;
    }

    public void ReturnProps(GameObject prop){
        PropData data = prop.GetComponent<PropController>().Data;
        
        if(!propsPool.ContainsKey(data)) return;
        
        prop.GetComponent<PropController>().OnDisenableEvent -= ReturnProps;

        propsPool[data].Enqueue(prop);
        prop.transform.SetParent(transform);
        prop.SetActive(false);
    }
}