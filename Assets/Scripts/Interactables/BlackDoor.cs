using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDoor : MonoBehaviour
{
    private UnlockCheck UC;
    private BlackKey BK;
    private void Start()
    {
        UC = GetComponent<UnlockCheck>();
        UC.message = "The door is locked, need a black key";
        BK = Object.FindObjectOfType<BlackKey>();
        BK.GetBlackKey += BK_GetBlackKey;
    }

    private void BK_GetBlackKey()
    {
        UC.Unlock = true;
    }
}
