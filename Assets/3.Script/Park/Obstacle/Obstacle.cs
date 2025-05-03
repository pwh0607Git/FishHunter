using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public ObstacleData Data{get; set;}
    public event UnityAction<GameObject> OnDisenableEventS;
    private List<GameObject> meshList = new();
    
    [SerializeField] float minScaleY;
    [SerializeField] float maxScaleY;

    void Start()
    {
        InitModelData();
    }

    void OnEnable()
    {
        SetMesh();
    }

    void OnDisable(){
        ResetMesh();
    }

    private void ResetMesh()
    {
        foreach(var mesh in meshList){
            if(mesh.activeSelf) mesh.SetActive(false);
        }
    }

    private void InitModelData(){
        // foreach(var model in Data){
        //     GameObject o = Instantiate(model, transform);
        //     o.transform.localPosition = Vector3.zero;
        //     meshList.Add(o);
        // }
    }

    private void SetMesh(){
        int i = Random.Range(0, meshList.Count);
        meshList[i].SetActive(true);
    }
}