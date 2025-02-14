using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axellottle : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(5);
        setDef(2);
        setmRange(4);
        setaRange(3);
        setactRange(4);
        setAbillity("heal");
        setName("Axellotle");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Squishy");
        setSprite(sprite);
    }

}
