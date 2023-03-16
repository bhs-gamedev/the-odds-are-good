using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    None,
    Character,
    Enemy,
    Any
}


public class Ability
{
    public string abilityName;
    public TargetType targetType;
    public Entity target;
    public int priority = 3;

    public void Execute()
    {
        Debug.Log("Executed " + abilityName);
    }
}
