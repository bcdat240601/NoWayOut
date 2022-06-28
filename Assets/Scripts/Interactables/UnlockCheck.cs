using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCheck : MonoBehaviour, IMessage
{
    [HideInInspector] public bool Unlock = false;
    [HideInInspector] public string message;
    private DoorCheck Door;
    private bool isSet = false;

    private void Start()
    {
        Door = GetComponent<DoorCheck>();
    }
    private void Update()
    {
        if (!Unlock && Door.DoorOpen && !isSet) StartCoroutine(CloseDoorAuto());        
    }
    public string ShowMessage()
    {
        return message;
    }

    IEnumerator CloseDoorAuto()
    {
        isSet = true;
        yield return new WaitForSeconds(5f);
        Door.CloseDoor();
        yield return new WaitForSeconds(1f);
        isSet = false;
    }

}
