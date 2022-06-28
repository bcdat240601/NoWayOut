using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockControl : MonoBehaviour
{
    [SerializeField]private int[] CurrentCombination ;
    public int[] CorrectCombination;
    [SerializeField]private LockNumber[] lockNumbers;
    public event Action OnSolved;
    public event Action OnDoneRandom;
    private PadLock padLock;
    [SerializeField] private GameObject Chain;
    private void Start()
    {
        CurrentCombination = new int[] { 0,0,0,0 };
        CorrectCombination = new int[] { UnityEngine.Random.Range(1,10), UnityEngine.Random.Range(1, 10), UnityEngine.Random.Range(1, 10), UnityEngine.Random.Range(1, 10) };
        OnDoneRandom?.Invoke();
        //lockNumbers = FindObjectsOfType<LockNumber>();
        padLock = GetComponent<PadLock>();
        foreach (LockNumber LN in lockNumbers)
        {
            LN.OnChangedNumberLock += LN_OnChangedNumberLock;
        }        
    }

    private void LN_OnChangedNumberLock(string LockName, int Number)
    {
        switch (LockName)
        {
            case "Lock1":
                CurrentCombination[0] = Number;
                break;
            case "Lock2":
                CurrentCombination[1] = Number;
                break;
            case "Lock3":
                CurrentCombination[2] = Number;
                break;
            case "Lock4":
                CurrentCombination[3] = Number;
                break;
        }
        int flag = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (CurrentCombination[i] != CorrectCombination[i])
            {
                break;
            }
            else
            {
                flag++;
            }
        }
        if (flag == 4)
        {
            OnSolved?.Invoke();
            padLock.Close();
            Destroy(this.gameObject);
            Destroy(Chain);
        }        
    }
    

    private void OnDestroy()
    {
        foreach (LockNumber LN in lockNumbers)
        {
            LN.OnChangedNumberLock -= LN_OnChangedNumberLock;
        }
    }
}
