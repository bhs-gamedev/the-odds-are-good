using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joker : Entity
{
    void Start()
    {
        abilities = new Ability[2] {new Attack(this), new Reroll(this)};
    }
}
