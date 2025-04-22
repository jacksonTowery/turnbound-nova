using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Character
{
    /* private int atk;
     private int def;
     private int mRange;
     private int aRange;
    //[SerializeField] private GameObject selectionPrefab;
    //private GameObject newSelection;
    private int health = 100;
    private bool attacked = false;
    private bool moved = false;
    private bool usedAbillity = false;
    public bool owner;*/
    // Start is called before the first frame update
    void Start()
    {
        setAtk(3);
        setDef(3);
        setmRange(4);
        setaRange(4);
        setactRange(4);
        setName("Astronaut");
        setAbillity("boostA");
        //Sprite sprite=Resources.Load("Assets/Sprites/Human (2)") as Sprite;
        Sprite sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        setSprite(sprite);
    }


}
