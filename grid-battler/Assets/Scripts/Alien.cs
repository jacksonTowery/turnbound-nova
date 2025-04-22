using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(3);
        setDef(3);
        setmRange(5);
        setaRange(4);
        setactRange(3);
        setAbillity("lowerA");
        setName("Alien");
        setSupportType(false);
        Sprite sprite = Resources.Load<Sprite>("Sprites/Alien");
        setSprite(sprite);
    }
}
