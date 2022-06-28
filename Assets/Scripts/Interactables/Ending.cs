using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour, IInteraction, IMessage
{
    public void interact()
    {
        SceneManager.LoadScene("Win");
    }
    public string ShowMessage()
    {
        string message = "Escape";
        return message;
    }
}
