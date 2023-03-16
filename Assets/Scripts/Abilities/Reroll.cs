using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reroll : Ability
{
    public Reroll(Entity e): base(e)
    {
        abilityName = "Reroll";
        canTargetAlly = true;
        canTargetOpponent = true;
        priority = 1;
    }

    public override void Execute()
    {
        target.Roll();
    }
}
