using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Camera))]
public class CameraForPlayers : MonoBehaviour
{
    
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    

    
    private Vector3 velocity;
    
    //private Camera cam;

    void Start()
    {
       // cam = GetComponent<Camera>(); 
    }
    void LateUpdate()
    {
        
        if (targets.Count == 0) { return; }

        Move();
       
    }   
    
    void Move()
    {
        if (GetMaiorDis() <= 13)
        {
            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        }
        else 
        {
            //Parar movimentação
            

            Debug.Log("Não pode andar");
        }
            Debug.Log("Maior Distancia entre os Players"+GetMaiorDis());
    }

    float GetMaiorDis() 
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) 
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
