using UnityEngine;
using System;

public class Player
{    
    public event EventHandler Died;
    private float _health = 10;
    

    private float _normalSpeed = 10;
    private float _runSpeed = 20;

    private float _gravity = -9.8f;

    public Player() { }
    public float Health{
        get { return _health; }        
    }    
    public float NormalSpeed
    {
        get { return _normalSpeed; }        
    }
    public float RunSpeed
    {
        get { return _runSpeed; }        
    }
    public float Gravity
    {
        get { return _gravity; }        
    }
    private void IsDead()
    {
        Died?.Invoke(this,EventArgs.Empty);
    }
    public void TakeDamage()
    {
        _health--;        
        if (Health <= 0) IsDead();
    }
}
