using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlackKey : MonoBehaviour, IInteraction, IMessage
{
    public event Action GetBlackKey;
    public void interact()
    {
        GetBlackKey?.Invoke();
        this.gameObject.SetActive(false);
    }
    public string ShowMessage()
    {
        return "Pick up black key";
    }
}
