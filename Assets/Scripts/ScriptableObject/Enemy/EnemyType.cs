using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyType : ScriptableObject
{
    public float _health;    
    public float _attackRange;
    public string _type;
    public float _damage;
}
