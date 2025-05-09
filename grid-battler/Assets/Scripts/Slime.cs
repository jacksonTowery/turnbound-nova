using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(2);
        setDef(5);
        setmRange(4);
        setaRange(3);
        setactRange(0);
        setAbillity("swapA/D");
       // setSupportType(false);
        setName("Slime");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
