using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(3);
        setDef(4);
        setmRange(4);
        setaRange(3);
        setactRange(4);
        setAbillity("heal");
        setName("Robot");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Medic");
        setSprite(sprite);
    }

}
