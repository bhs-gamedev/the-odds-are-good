using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ability
{
    public Entity entity;
    public string abilityName;
    public bool canTargetAlly {get; protected set;}
    public bool canTargetOpponent {get; protected set;}
    public Entity target;
    public int priority = 3;

    public Ability(Entity e)
    {
        entity = e;
    }

    public virtual void Execute()
    {
        Debug.Log("Executed " + abilityName);
    }

    public virtual bool IsValidTarget(Entity entity)
    {
        return true;
    }
}
