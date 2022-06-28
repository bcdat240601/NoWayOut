using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour, IMessage, IInteraction
{
    private string message;
    private bool MessageCheck = false;
    private Animation Clips;
    private Triggers TriggerScript;

    private void Start()
    {
        Clips = GetComponentInParent<Animation>();
        TriggerScript = Object.FindObjectOfType<Triggers>();
        TriggerScript.OnActive += TriggerScript_OnActive;

    }

    private void TriggerScript_OnActive()
    {
        MessageCheck = true;
    }

    public string ShowMessage()
    {
        if (!MessageCheck)
        {
            message = "You shouldn't open this door yet";
        }
        else
        {
            message = "Press [E] to open it";
        }
        return message;
    }
    public void interact()
    {
        if (MessageCheck)
        {
            Clips.Play();
        }
    }
}
