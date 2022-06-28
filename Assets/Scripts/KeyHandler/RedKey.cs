using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RedKey : MonoBehaviour, IInteraction, IMessage
{
    public event Action GetRedKey;
    public void interact()
    {
        GetRedKey?.Invoke();
        this.gameObject.SetActive(false);
    }
    public string ShowMessage()
    {
        return "Pick up red key";
    }
}
