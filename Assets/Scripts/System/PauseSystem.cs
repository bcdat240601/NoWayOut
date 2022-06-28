using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PauseSystem : Singleton<PauseSystem>
{
    private bool IsPaused = false;    
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject ConfirmUI;
    private MouseLook CameraLook;
    private PlayableDirector[] Timeline;
    //[SerializeField] private GameObject AskingUI;
    // Start is called before the first frame update
    void Start()
    {
        Timeline = FindObjectsOfType<PlayableDirector>();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Timeline = FindObjectsOfType<PlayableDirector>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraLook == null)
        {
            CameraLook = Object.FindObjectOfType<MouseLook>();
        }        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        if (ConfirmUI.active)
        {
            return;
        }
        if (Timeline != null)
        {
            int flag = 0;
            foreach (var Cut in Timeline)
            {
                if (Cut.state == PlayState.Playing)
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
                CameraLook.enabled = true;
            }
        }        
        CameraLook.LockMouse();        
        IsPaused = false;
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
    //public void MainMenu()
    //{
        
    //}
    void Pause()
    {
        CameraLook.UnlockMouse();
        CameraLook.enabled = false;
        IsPaused = true;
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
    }    
    public void MainMenu()
    {
        PauseUI.SetActive(false);
        ConfirmUI.GetComponentInChildren<Text>().text = "Are you sure you want to go back the game menu";
        ConfirmUI.SetActive(true);
    }
    public void ConfirmYes()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ConfirmNO()
    {
        ConfirmUI.SetActive(false);
        PauseUI.SetActive(true);
    }
}
