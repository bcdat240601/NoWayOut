using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public static class Loader
{
    public static bool StartEffect = true;
    private class LoadingBehavior : MonoBehaviour { }
    
    private static Action<Func<Coroutine>> LoadingCallBack;
    public static void LoadingScene(string SceneName)
    {
        LoadingCallBack = (Func<Coroutine> LoadingEffect) =>
        {
            GameObject GObject = new GameObject();
            GObject.AddComponent<LoadingBehavior>().StartCoroutine(LoadingAsync(SceneName, LoadingEffect));
        };
        SceneManager.LoadScene("Loading");
    }

    private static IEnumerator LoadingAsync(string SceneName, Func<Coroutine> LoadingEffect)
    {
        yield return null;
        AsyncOperation scene = SceneManager.LoadSceneAsync(SceneName);

        while (!scene.isDone)
        {
            if (StartEffect)
            {
                LoadingEffect();
            }
            yield return null;
        }
    }
    public static void CallBackLoading(Func<Coroutine> LoadingEffect)
    {
        if (LoadingCallBack != null)
        {
            LoadingCallBack(LoadingEffect);
            LoadingCallBack = null;
        }
    }
}
