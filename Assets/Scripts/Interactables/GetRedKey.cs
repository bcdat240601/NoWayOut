using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRedKey : MonoBehaviour, IInteraction , IMessage
{
    [SerializeField] private Transform Jaw;
    [SerializeField] private BoxCollider Collider;
    public void interact() {
        Jaw.RotateAroundLocal(Vector3.forward, -30f);
        Collider.enabled = false;
    }
    public string ShowMessage()
    {
        string message = "Open it";
        return message;
    }
}
