using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Candle : MonoBehaviour,IInteraction, IMessage
{
    public event Action OnPickedUp;
    public void interact()
    {
        OnPickedUp?.Invoke();
        Destroy(this.gameObject);        
    }
    public string ShowMessage()
    {
        string message = "Press [E] to pick up candle";
        return message;
    }
}
