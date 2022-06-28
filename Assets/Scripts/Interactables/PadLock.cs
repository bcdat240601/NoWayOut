using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLock : MonoBehaviour,IInteraction,IMessage
{
    [SerializeField]private GameObject PadLockUIDisplay;    
    private MouseLook MLScript;    

    private void Start()
    {
        MLScript = FindObjectOfType<MouseLook>();        

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.X) && PadLockUIDisplay.active)
        {
            Close();
        }
    }
    public void interact()
    {        
        if (!PadLockUIDisplay.active)
        {
            PadLockUIDisplay.SetActive(true);
            MLScript.UnlockMouse();
            MLScript.enabled = false;

        }        
    }
    public string ShowMessage()
    {
        string message = "Press [E] to solve";
        return message;
    }
    public void Close()
    {        
        PadLockUIDisplay.SetActive(false);
        MLScript.enabled = true;
        MLScript.LockMouse();        
    }

}
