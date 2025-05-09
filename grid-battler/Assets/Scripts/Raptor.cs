using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raptor : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(5);
        setDef(2);
        setmRange(4);
        setaRange(4);
        setactRange(3);
        setAbillity("lowerM");
        setSupportType(false);
        setName("Raptor");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
