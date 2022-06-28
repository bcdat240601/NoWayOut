using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGiver : MonoBehaviour
{
    [SerializeField] private Material[] NumberMaterial;
    [SerializeField] private GameObject[] ObjectGetter;
    [SerializeField] private LockControl LC;

    private void Awake()
    {                
        LC.OnDoneRandom += LC_OnDoneRandom;
    }

    private void LC_OnDoneRandom()
    {        
        for (int i = 0; i < LC.CorrectCombination.Length; i++)
        {            
            ObjectGetter[i].GetComponent<Renderer>().material = NumberMaterial[LC.CorrectCombination[i] - 1];
        }
    }
}
