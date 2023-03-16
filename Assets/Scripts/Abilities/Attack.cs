using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{
    public Attack(Entity e): base(e)
    {
        abilityName = "Attack";
        canTargetAlly = false;
        canTargetOpponent = true;
    }

    public override void Execute()
    {
        target.Damage(entity.value);
    }
}
