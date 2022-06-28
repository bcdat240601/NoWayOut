using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CandleDuration : MonoBehaviour
{
    public float MaxTime = 120f;
    [SerializeField] private RectTransform CandleDurationControl;
    [SerializeField] private GameObject CandleTime;
    public float CurrentTime;
    private float ScaleMax;        
    private bool isTrigger = false;
    private CandleStored candleStored;
    public event Action OnChanged;
    private float TimeLost = 1f;
    [SerializeField] private Animation Latern;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = MaxTime;
        Triggers trigger = FindObjectOfType<Triggers>();
        trigger.OnDisplayLaternUI += Trigger_OnDisplayLaternUI;
        candleStored = FindObjectOfType<CandleStored>();
        candleStored.OnReload += CandleStored_OnReload;
        ScaleMax = CandleDurationControl.localScale.x;
    }

    private void CandleStored_OnReload()
    {
        StartCoroutine(Reload());
    }

    private void Trigger_OnDisplayLaternUI()
    {
        isTrigger = true;
        CandleTime.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {        
        if (isTrigger)
        {            
            Duration();
        }
        //if (Input.GetKey(KeyCode.R) && candleStored.CurrentCandle > 0 && CurrentTime != MaxTime)
        //{
        //    Reload();
        //}
    }
    
    void Duration()
    {
        CurrentTime -= TimeLost * Time.deltaTime;
        if (CurrentTime <= 0)
        {
            CurrentTime = 0;
        }
        OnChanged?.Invoke();
        CandleDurationControl.localScale = new Vector3(((CurrentTime * ScaleMax) / MaxTime), CandleDurationControl.localScale.y, CandleDurationControl.localScale.z);
    }
    IEnumerator Reload()
    {
        Latern.Play("ReloadLatern1");
        yield return new WaitForSeconds(0.2f);
        CurrentTime = MaxTime;
        yield return new WaitForSeconds(1f);
        Latern.Play("ReloadLatern2");
        OnChanged?.Invoke();
    }
}
