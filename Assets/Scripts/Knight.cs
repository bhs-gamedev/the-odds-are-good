using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Entity
{
    void Start()
    {
        abilities = new Ability[2] {new Attack(this), new Taunt(this)};
    }
}
