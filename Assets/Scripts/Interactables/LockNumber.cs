using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LockNumber : MonoBehaviour
{
    public event Action<string, int> OnChangedNumberLock;

    private Text NumberShowUI;    

    [SerializeField]private int numberShown = 0;

    private void Start()
    {
        NumberShowUI = GetComponentInChildren<Text>();
        NumberShowUI.text = "" + numberShown;        
    }
       
    public void ChangeNumber()
    {        
        if (numberShown == 9)
        {
            numberShown = 0;
        }
        else
        {
            numberShown++;
        }
        NumberShowUI.text = "" + numberShown;
        if (OnChangedNumberLock != null)
        {
            OnChangedNumberLock(name, numberShown);
        }
    }
}
