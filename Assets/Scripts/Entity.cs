using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int value;
    public int health = 10;
    public Ability[] abilities;
    public Ability selectedAbility;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Damage(int amount)
    {
        health -= amount;
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    public bool CanAct()
    {
        return health > 0;
    }

    public void Roll()
    {
        value = Random.Range(1, 6);
    }
}