using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CandleStored : MonoBehaviour
{
    [SerializeField] private Text Amount;
    [SerializeField] private GameObject CandleUI;
    public int CurrentCandle = 0;
    public event Action OnReload;
    private float TimeCooldownReload;


    private void Start()
    {
        Triggers trigger = FindObjectOfType<Triggers>();
        trigger.OnDisplayLaternUI += Trigger_OnDisplayLaternUI;
        Amount.text = CurrentCandle + " x";
    }

    private void Trigger_OnDisplayLaternUI()
    {
        CandleUI.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && CurrentCandle > 0 && Time.time >= TimeCooldownReload)
        {
            TimeCooldownReload = Time.time + 2f;
            OnReload?.Invoke();
            UseCandle();            
        }
    }

    private void Candle_OnPickedUp()
    {
        CurrentCandle++;
        Amount.text = CurrentCandle + " x";
    }
    private void UseCandle()
    {
        CurrentCandle--;
        Amount.text = CurrentCandle + " x";
    }
    public void SubcribeCandleEvent()
    {
        Candle[] candles = FindObjectsOfType<Candle>();
        foreach (Candle candle in candles)
        {
            candle.OnPickedUp += Candle_OnPickedUp;
        }
    }
}
