using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextScene : MonoBehaviour, IInteraction, IMessage
{
    [SerializeField] private GameObject BackGround;
    public void interact()
    {
        StartCoroutine(LoadNextScene());
    }

    public string ShowMessage()
    {
        string message = "Enter this door";
        return message;
    }
    IEnumerator LoadNextScene()
    {
        BackGround.GetComponent<Animation>().Play("Faded 2");
        yield return new WaitForSeconds(1f);        
        Loader.LoadingScene("Basement2");
    }
}
