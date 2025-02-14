using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class Character: MonoBehaviour 
{
    // Start is called before the first frame update
    [SerializeField] private int atk;
    [SerializeField] private int def;
    [SerializeField] private int mRange;
    [SerializeField] private int aRange;
    [SerializeField] private int actRange;

  
    private int health = 100;
    private bool attacked=false;
    private bool moved=false;
    private bool usedAbillity = false;
    public bool isSelected;
    public bool owner;
    public bool supporter=true;
    public Abillity ab=new Abillity();
    public string abType = "heal";
    public string name = "Beta";
   // public Sprite[] sprites=Resources.LoadAll("Spites",  typeof(Sprite)).Cast<Sprite>().ToArray();

    public int getmRange()
    {
        return mRange;
    }
    public int getaRange() 
    {
        return aRange;
    }
    public int getactRange()
    {
        return actRange;
    }
    public int getAtk()
    {
        return atk;
    }
    public void setAtk(int a)
    {
        atk = a;
    }
    public int getDef()
    {
        return def;
    }
    public void setDef(int d)
    {
        def = d;
    }
    public void setmRange(int m)
    {
        mRange = m;
    }
    public void setaRange(int m)
    {
        aRange = m;
    }
    public void setactRange(int m)
    {
        actRange = m;
    }
    public void setAbillity(string a)
    {
        abType = a;
    }
    public int getHealth()
    {
        return health;
    }
    public bool getMoved()
    {
       return moved;
    }
    public string getName()
    {
        return name;
    }
    public void setName(string a)
    {
        name = a;
    }
    public string getAction()
    {
        return abType;
    }
    public void setSupportType(bool b)
    {
        supporter= b;
    }

    public void Moved()
    {
        moved = true;
    }
    public void attack()
    {
        attacked = true;
    }
    public bool getAttack()
    {
        return attacked;
    }
    public bool getAct()
    {
        return usedAbillity;
    }
    public void acted()
    {
        usedAbillity = false;
    }
    public bool getActType()
    {
        return supporter;
    }
    public bool getIsOwner()
    {
        return owner;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }
    

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void takeDammage(int power)
    {
        health -= power*25 / def;
        if(health <= 0)
        {
            defeated();
        }
    }
    public void heal(int percent)
    {
        if (health >= 100 - percent)
        {
            health = 100;
        }
        else
        {
            health += percent;
        }
    }
    public Character action(Character target)
    {
        return ab.useAnAbillity(abType, target);

    }
    private void defeated()
    {
        Destroy(gameObject);
    }
    public void resetBools() 
    {
        attacked = false;
        moved = false;
        usedAbillity = false;
    }
    public void change()
    {
        owner = !owner;
    }
    public void setSprite(Sprite s)
    {

            gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }


    /*private void OnMouseDown()
    {
        if (newSelection==null)
        {
            newSelection = Instantiate(selectionPrefab, transform.position,Quaternion.identity);
            newSelection.transform.SetParent(gameObject.transform);
            newSelection.SetActive(false);
        }

        isSelected=!isSelected;

        if(isSelected)
        {
            newSelection.SetActive(true);
        }
        else
        {
            newSelection.SetActive(false);
        }
    }*/




}
