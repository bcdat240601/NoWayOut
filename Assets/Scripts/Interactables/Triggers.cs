using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Triggers : MonoBehaviour
{
    public bool isActive = false;
    public bool isClose = false;
    public event Action OnActive;
    public event Action OnDisplayLaternUI;    


    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            OnActive?.Invoke();
            
        }
        if (isClose)
        {
            OnDisplayLaternUI?.Invoke();
        }
    }
}
