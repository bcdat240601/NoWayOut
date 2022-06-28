using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public Animation DoorBang;
    public static bool isExecuted = false;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isExecuted == false)
        {
            DoorBang.Play("JumpScare");
            isExecuted = true;            
        }
    }
}
