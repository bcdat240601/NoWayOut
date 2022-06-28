using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Latern : MonoBehaviour, IInteraction,IMessage
{
    public event Action OnPickUp;    
    public Transform LeftHand;
    [SerializeField] private Transform Candle;
    [SerializeField] private Light LaternLight;
    private CandleDuration CD;
    private float ScaleMax = 1f;
    private float LaternMaxRange;
    // Start is called before the first frame update
    void Start()
    {
        CD = FindObjectOfType<CandleDuration>();
        CD.OnChanged += CD_OnChanged;
        LaternMaxRange = LaternLight.range;
    }

    private void CD_OnChanged()
    {
        Candle.localScale = new Vector3(Candle.localScale.x, (CD.CurrentTime * ScaleMax) / CD.MaxTime, Candle.localScale.z);
        LaternLight.range = (CD.CurrentTime * LaternMaxRange) / CD.MaxTime;
    }
    
    public void interact()
    {
        transform.SetParent(LeftHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<BoxCollider>().enabled = false;        
        OnPickUp?.Invoke();
    }
    public string ShowMessage()
    {
        return "Press [E] to pick up Latern";
    }    
}
