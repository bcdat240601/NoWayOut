using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkullInteract : MonoBehaviour, IInteraction, IMessage
{
    public bool isInteractable;
    public event Action<string> OnInteractSkull;
    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void interact()
    {        
        if (isInteractable && OnInteractSkull != null)
        {
            anim.Play();
            OnInteractSkull(name);
        }
    }
    public string ShowMessage()
    {
        string message = "Press [E] to Touch";
        return message;
    }

    
    
}
