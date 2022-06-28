using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{    
    //public GameObject Pistol;
    public Text Intertext;
    private Outline outlineSave;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (PlayerCast.hit.collider != null && PlayerCast.DistanceFromTarget != 0)
        {
            Interaction();                                 
        }        
    }
    void Interaction()
    {        
        Outline outline = PlayerCast.hit.transform.GetComponent<Outline>();        
        IInteraction interact = PlayerCast.hit.transform.GetComponent<IInteraction>();
        IMessage message = PlayerCast.hit.transform.GetComponent<IMessage>();
        if (outline != null && PlayerCast.DistanceFromTarget <= 4)
        {
            outlineSave = outline;
            outline.enabled = true;
        }
        if((outline == null && outlineSave != null) || (outlineSave != null && outline != outlineSave))
        {
            outlineSave.enabled = false;
        }
        if (interact == null)
        {
            Intertext.text = "";
            return;
        }
        if (Input.GetButtonDown("Action"))
        {
            if (PlayerCast.DistanceFromTarget <= 4 && MouseLook.UpDownRotation < 45)
            {
                interact.interact();
            }
            // look down
            if (PlayerCast.DistanceFromTarget <= 5.5 && MouseLook.UpDownRotation > 45)
            {
                interact.interact();
            }
            if (PlayerCast.DistanceFromTarget <= 7 && MouseLook.UpDownRotation > 75)
            {
                interact.interact();
            }
        }
        if (PlayerCast.DistanceFromTarget <= 4 && MouseLook.UpDownRotation < 45)
        {
            Intertext.text = message.ShowMessage();
        }
        else if (PlayerCast.DistanceFromTarget > 4)
        {
            Intertext.text = "";
        }
        // look down
        if (PlayerCast.DistanceFromTarget <= 5.5 && MouseLook.UpDownRotation > 45)
        {            
            Intertext.text = message.ShowMessage();                       
        }        
        else if (PlayerCast.DistanceFromTarget > 5.5)
        {            
            Intertext.text = "";
        }
        if (PlayerCast.DistanceFromTarget <= 7 && MouseLook.UpDownRotation > 75)
        {
            Intertext.text = message.ShowMessage();
        }
        else if (PlayerCast.DistanceFromTarget > 7)
        {

        }
    }    
}
