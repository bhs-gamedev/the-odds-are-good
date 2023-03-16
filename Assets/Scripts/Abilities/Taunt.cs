using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : Ability
{
    public Taunt(Entity e): base(e)
    {
        abilityName = "Taunt";
        canTargetAlly = false;
        canTargetOpponent = true;
    }

    public override void Execute()
    {
        if (target.selectedAbility != null)
        {
            target.selectedAbility.target = entity;
        }
    }
}
