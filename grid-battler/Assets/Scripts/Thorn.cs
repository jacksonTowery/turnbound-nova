using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(4);
        setDef(3);
        setmRange(4);
        setaRange(3);
        setactRange(4);
        setAbillity("Thorn");
        setSupportType(false);
        setName("Thorn");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
