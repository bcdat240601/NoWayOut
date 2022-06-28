using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    public static float DistanceFromTarget;
    public static RaycastHit hit;
    public float ToTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit))
        {
            ToTarget = hit.distance;
            DistanceFromTarget = ToTarget;            
        }
    }
}
