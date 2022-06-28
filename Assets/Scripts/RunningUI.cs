using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningUI : MonoBehaviour
{
    [SerializeField] private GameObject RunningUIDisplay;
    [SerializeField] private Transform RunningControl;
    private PlayerController player;
    public float CurrentStamina;
    private float MaxStamina = 100f;
    private float StaminaRegen = 10f;
    private float StaminaConsume = 7.5f;
    private float MaxScale;
    private float DeadTime;    
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.OnRunning += Player_OnRunning;
        player.OnWalk += Player_OnWalk;
        CurrentStamina = MaxStamina;
        MaxScale = RunningControl.localScale.x;
    }

    private void Player_OnWalk()
    {
        if (Time.time >= DeadTime)
        {
            StaminaIncrease();
        }
    }

    private void Player_OnRunning()
    {
        RunningUIDisplay.SetActive(true);
        StaminaDecrease();
    }
    private void StaminaDecrease()
    {
        CurrentStamina -= StaminaConsume * Time.deltaTime;
        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0;
            DeadTime = Time.time + 2;

        }
        UpdateStamina();
    }
    private void StaminaIncrease()
    {
        CurrentStamina += StaminaRegen * Time.deltaTime;
        if (CurrentStamina >= MaxStamina)
        {
            RunningUIDisplay.SetActive(false);
            CurrentStamina = MaxStamina;
        }
        UpdateStamina();
    }
    private void UpdateStamina()
    {
        RunningControl.localScale = new Vector3(CurrentStamina * MaxScale / MaxStamina, RunningControl.localScale.y, RunningControl.localScale.z);
    }
}
