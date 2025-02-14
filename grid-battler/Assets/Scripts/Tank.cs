using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(4);
        setDef(5);
        setmRange(3);
        setaRange(3);
        setactRange(3);
        setAbillity("boostD");
        setName("Tank");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tank");
        setSprite(sprite);
    }

}
