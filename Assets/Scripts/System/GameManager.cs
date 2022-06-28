using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private CandleStored CS;
    private GameObject player;
    // Start is called before the first frame update    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        GetObjects();
        SubcribeEvents();
        if (scene.name == "Basement")
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (scene.name == "Basement2")
        {
            player.transform.position = new Vector3(-193.4f, 3.353f, -42.98f);
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.GetComponent<FadedvsNarration>().FadedScene.GetComponent<Animation>().Play("Faded");
            player.GetComponent<PlayerController>().Doors = FindObjectsOfType<DoorCheck>();
        }
    }
    private void GetObjects()
    {
        if (CS == null)
        {
            CS = FindObjectOfType<CandleStored>();
        }
    }
    private void SubcribeEvents()
    {
        if (CS != null)
        {
            CS.SubcribeCandleEvent();
        }
    }
}
