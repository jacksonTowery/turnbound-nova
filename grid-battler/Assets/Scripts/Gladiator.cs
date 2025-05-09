using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladiator :Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(4);
        setDef(4);
        setmRange(3);
        setaRange(3);
        setactRange(4);
        setAbillity("lowerD");
        setSupportType(false);
        setName("Gladiator");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
