using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fench : MonoBehaviour, IInteraction, IMessage
{
    private bool isResolvePadLock = false;
    private LockControl LC;
    private Animation anim;
    [SerializeField] private GameObject Corpse;
    private bool isPulled = false;
    private void Start()
    {
        LC = FindObjectOfType<LockControl>();
        LC.OnSolved += LC_OnSolved;
        anim = GetComponent<Animation>();
    }

    private void LC_OnSolved()
    {
        isResolvePadLock = true;
    }

    public void interact()
    {
        if (isResolvePadLock && !isPulled)
        {
            anim.Play();
            isPulled = true;
            Corpse.SetActive(true);
        }
    }

    public string ShowMessage()
    {
        if (isResolvePadLock && !isPulled)
        {
            string message = "Pull Out";
            return message;
        }
        return null;
    }
}
