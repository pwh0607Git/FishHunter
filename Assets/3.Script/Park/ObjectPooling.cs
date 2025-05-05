using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> : MonoBehaviour
{
    protected Dictionary<T, Queue<GameObject>> pool = new();

    public void InitPool(T data, int maxSize = 5){
        if(pool.ContainsKey(data)) return;           //이미 포함되어있는 프리팹은 무시

        pool.Add(data, new());

        for(int i = 0; i<maxSize; i++){
            MakeClone(data);
        }
    }

    public GameObject GetObject(T data){
        if(data == null) return null;
        
        if(!pool.ContainsKey(data)) return null;

        if(pool[data].Count <= 0){  
            MakeClone(data);
        }

        GameObject o = pool[data].Dequeue();
        o.SetActive(true);

        return o;
    }

    protected virtual void MakeClone(T data){ }

    public virtual void ReturnProps(GameObject prop){ }
}
