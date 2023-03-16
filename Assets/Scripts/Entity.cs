using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int value;
    public int health = 10;
    public Ability[] abilities;
    public Ability selectedAbility;
    public EntityUI ui;

    public void Damage(int amount)
    {
        health -= amount;
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public bool CanAct()
    {
        return IsAlive();
    }

    public void Roll()
    {
        value = Random.Range(1, 6);
    }

    public void Execute()
    {
        if (selectedAbility != null)
        {
            selectedAbility.Execute();
            selectedAbility = null;
        }
        value = 0;

    }
}