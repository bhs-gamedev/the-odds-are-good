using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public Heal(Entity e): base(e)
    {
        abilityName = "Heal";
        canTargetAlly = true;
        canTargetOpponent = false;
    }

    public override void Execute()
    {
        target.Heal(entity.value);
    }
}
