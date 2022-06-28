using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoor : MonoBehaviour
{
    private UnlockCheck UC;
    private RedKey BK;
    private void Start()
    {
        UC = GetComponent<UnlockCheck>();
        UC.message = "The door is locked, need a red key";
        BK = Object.FindObjectOfType<RedKey>();
        BK.GetRedKey += BK_GetRedKey;
    }

    private void BK_GetRedKey()
    {
        UC.Unlock = true;
    }
}
