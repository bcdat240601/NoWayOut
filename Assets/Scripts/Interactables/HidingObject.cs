using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : MonoBehaviour, IInteraction ,IMessage
{
    [SerializeField] private Transform Locker;    
    private PlayerController player;
    private Transform playerTranform;
    private bool Hiding = false;
    [SerializeField] private Transform PositionOut;
    [SerializeField] private Transform PositionIn;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerTranform = player.GetComponent<Transform>();        
    }        
    public void interact()
    {
        if (!Hiding)
        {
            player.enabled = false;
            playerTranform.position = PositionIn.position;
            player.enabled = true;
            playerTranform.rotation = Quaternion.Euler(0,Locker.rotation.eulerAngles.y, 0);
            player.IsHide = true;
            Hiding = true;
            
        }
        else
        {
            player.enabled = false;
            playerTranform.position = PositionOut.position;
            player.enabled = true;
            playerTranform.rotation = Quaternion.Euler(0, -Locker.rotation.eulerAngles.y, 0);
            player.IsHide = false;
            Hiding = false;
        }
    }
    public string ShowMessage()
    {
        string message;
        if (!Hiding)
        {
            message = "Press [E] to Hide";
        }
        else
        {
            message = "Press [E] to get out";
        }
        return message;
    }
}
