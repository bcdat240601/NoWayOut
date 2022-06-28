using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour, IInteraction, IMessage
{
    [SerializeField] private NoteSO NoteContentGiver;
    [SerializeField] private Text NoteContentUI;
    [SerializeField] private GameObject NoteDisplay;
    [SerializeField] private GameObject Cursor;
    private MouseLook MLScript;
    private Triggers TriggerEvent;    
    private void Start()
    {
        MLScript = FindObjectOfType<MouseLook>();
        TriggerEvent = GetComponent<Triggers>();
        Cursor = GameObject.Find("Cursor");
    }
    private void Update()
    {
        CloseNote();
    }
    public void interact()
    {
        NoteContentUI.text = NoteContentGiver.NoteContent;
        if (!NoteDisplay.active)
        {
            NoteDisplay.SetActive(true);
            Cursor.SetActive(false);
            MLScript.enabled = false;
        }
        if (TriggerEvent != null)
        {
            TriggerEvent.isActive = true;
        }
    }
    public string ShowMessage()
    {
        string message = "Press [E] to read";
        return message;
    }
    public void CloseNote()
    {
        if (Input.GetKey(KeyCode.X) && NoteDisplay.active)
        {
            NoteDisplay.SetActive(false);
            Cursor.SetActive(true);
            MLScript.enabled = true;
            if (!TriggerEvent.isClose && TriggerEvent != null)
            {
                TriggerEvent.isClose = true;
            }
        }
    }    
}
