using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        abilities = new Ability[2] {new Attack(), new Taunt()};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
