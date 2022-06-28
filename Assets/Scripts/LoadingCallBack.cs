using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LoadingCallBack : MonoBehaviour
{
    private bool IsLoadScene = true;
    [SerializeField] private Text Dots;    
    // Update is called once per frame
    void Update()
    {
        if (IsLoadScene)
        {            
            IsLoadScene = false;
            Loader.CallBackLoading(() => StartCoroutine(LoadingEffect()));
        }        
    }
    IEnumerator LoadingEffect()
    {
        Loader.StartEffect = false;
        Dots.text = "";
        yield return new WaitForSeconds(0.1f);
        Dots.text += ".";
        yield return new WaitForSeconds(0.1f);
        Dots.text += ".";
        yield return new WaitForSeconds(0.1f);
        Dots.text += ".";
        yield return new WaitForSeconds(0.1f);
        Loader.StartEffect = true;
    }
}
