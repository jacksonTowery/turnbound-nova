using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre :Character
{
    // Start is called before the first frame update
    void Start()
    {
        setAtk(4);
        setDef(4);
        setmRange(3);
        setaRange(4);
        setactRange(3);
        setAbillity("Attack");
        setSupportType(false);
        setName("Ogre");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }
}
