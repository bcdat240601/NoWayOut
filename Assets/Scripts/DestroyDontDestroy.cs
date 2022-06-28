using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyDontDestroy : MonoBehaviour
{
    private DontDestroy[] AllDestroyedObject;
    // Start is called before the first frame update
    void Awake()
    {
        AllDestroyedObject = FindObjectsOfType<DontDestroy>();
        foreach (DontDestroy obj in AllDestroyedObject)
        {
            Destroy(obj.gameObject);
        }
        Time.timeScale = 1f;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void MainMenu()
    {
        Loader.LoadingScene("MainMenu");
    }
}
