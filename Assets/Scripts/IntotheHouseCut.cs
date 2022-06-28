using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntotheHouseCut : MonoBehaviour
{
    private bool istrigger = false;
    public GameObject BG;
    public PlayableDirector IntotheHouse;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (istrigger == false)
        {
            istrigger = true;
         
            StartCoroutine(StartCutscene());            
        }
    }
    IEnumerator StartCutscene()
    {
        BG.SetActive(true);
        yield return new WaitForSeconds(1f);
        BG.SetActive(false);
        IntotheHouse.Play();
                
    }
    public void NextScene()
    {
        BG.SetActive(true);        
        Loader.LoadingScene("Basement");
    }
}
