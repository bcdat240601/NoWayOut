using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class DoorCheck : MonoBehaviour, IInteraction, IMessage
{
    private string message = "";
    [HideInInspector] public bool DoorOpen = false;
    private bool Clicked = false;
    private Animation Clips;
    private UnlockCheck UC;
    public event Action<Vector3> OnSounded;
    public Transform Position1;
    public Transform Position2;

        

    // Start is called before the first frame update
    void Start()
    {
        Clips = GetComponentInParent<Animation>();
        UC = GetComponent<UnlockCheck>();
    }
    
    private IEnumerator DoorAnim()
    {
        if (DoorOpen == true)
        {
            int i = 0;
            foreach (AnimationState clip in Clips)
            {
                if (i == 0)
                {
                    Clips.Play(clip.name);
                    break;
                }
                i++;
            }
            yield return new WaitForSeconds(1.25f);
            Clicked = false;
        }
        else if (DoorOpen == false)
        {
            int i = 0;
            foreach (AnimationState clip in Clips)
            {
                if (i == 1)
                {
                    Clips.Play(clip.name);
                    break;
                }
                i++;
            }
            yield return new WaitForSeconds(1.25f);
            Clicked = false;
        }
    }
    public void interact()
    {
        if (UC != null && UC.Unlock == false)
        {
            return;
        }
        if (DoorOpen == false && Clicked == false)
        {
            OpenDoor();
        }
        if (DoorOpen == true && Clicked == false)
        {
            CloseDoor();
        }
        if (OnSounded != null)
        {
            OnSounded(Position1.transform.position);
        }
    }
    public string ShowMessage()
    {
        if (UC != null && UC.Unlock == false)
        {
            message = UC.ShowMessage();
            return message;
        }
        if (DoorOpen == false)
        {
            message = "Press [E] to open the door";
        }
        else
        {
            message = "Press [E] to close the door";
        }
        return message;
    }  
    public void OpenDoor()
    {
        DoorOpen = true;
        Clicked = true;
        StartCoroutine(DoorAnim());
    }
    public void CloseDoor()
    {
        DoorOpen = false;
        Clicked = true;
        StartCoroutine(DoorAnim());
    }
}
