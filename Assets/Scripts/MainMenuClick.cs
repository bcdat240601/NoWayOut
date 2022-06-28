using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuClick : MonoBehaviour
{   
    [SerializeField] private GameObject FadeBG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveNextScene()
    {
        StartCoroutine(NextSceneHandle());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator NextSceneHandle()
    {        
        FadeBG.SetActive(true);
        FadeBG.GetComponent<Animation>().Play();        
        yield return new WaitForSeconds(1f);               
        Loader.LoadingScene("Forest");
        
    }
}
