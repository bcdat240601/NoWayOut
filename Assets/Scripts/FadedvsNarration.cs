using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadedvsNarration : MonoBehaviour
{
    public GameObject FadedScene;
    private MouseLook mouseLook;
    private PlayerController playerController;
    public Text Text;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        mouseLook = GetComponent<MouseLook>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController.enabled = false;
        mouseLook.enabled = false;
        StartCoroutine(GameBegin());
    }
    IEnumerator GameBegin()
    {
        FadedScene.GetComponent<Animation>().Play("Faded");
        Text.text = "I need to get out of here";
        yield return new WaitForSeconds(2f);
        playerController.enabled = true;
        mouseLook.enabled = true;
        Text.text = "";
    }
}