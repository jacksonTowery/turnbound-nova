using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(4);
        setDef(4);
        setmRange(3);
        setaRange(3);
        setactRange(0);
        setAbillity("Dub");
        // setSupportType(false);
        setName("Chip");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
