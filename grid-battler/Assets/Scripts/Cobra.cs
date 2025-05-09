using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : Character
{
    // Start is called before the first frame update
    void Start()
    {

        setAtk(2);
        setDef(5);
        setmRange(4);
        setaRange(3);
        setactRange(4);
        setAbillity("Poison");
        setSupportType(false);
        setName("Cobra");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
