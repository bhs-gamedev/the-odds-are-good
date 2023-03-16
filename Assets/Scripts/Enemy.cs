using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public void SelectAbility()
    {
        selectedAbility = new Attack(this);
    }
}
