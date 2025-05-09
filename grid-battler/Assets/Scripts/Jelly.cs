using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(2);
        setDef(4);
        setmRange(5);
        setaRange(3);
        setactRange(4);
        setAbillity("DissableAb");
        setSupportType(false);
        setName("Jelly");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
