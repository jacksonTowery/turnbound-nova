using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronomer : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(4);
        setDef(2);
        setmRange(4);
        setaRange(5);
        setactRange(3);
        setAbillity("boostM");
        setName("Astronomer");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Long_Range");
        setSprite(sprite);
    }
}
