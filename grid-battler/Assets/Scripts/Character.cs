using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    private bool isSelected;
    private bool owner;
    private bool supporter=true;
    private Abillity ab=new Abillity();
    private string abType = "heal";
    private string charName = "Beta";
    public Sprite charSprite;
    int aBoost = 0;
    int dBoost = 0;
    int mBoost = 0;
    Vector3 pos = Vector3.zero;
    int hpBoost = 0;
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
    public int getAIAtk()
    {
        return atk+aBoost;
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
    public void setPos(Vector3 pos)
    {
        this.pos = pos;
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
        return charName;
    }
    public void setName(string a)
    {
        charName = a;
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
    public void usedAction()
    {
        usedAbillity=true;
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
        pos= transform.position;
    }
    public void takeDammage(int power, int b)
    {
        health -= power*b / def;
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
    public void resetBoosts()
    {
        aBoost = 0;
        dBoost = 0;
        mBoost = 0;
        pos = transform.position;
        hpBoost = 0;
    }
    public void change()
    {
        owner = !owner;
    }
    public void setSprite(Sprite s)
    {

            gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }
    public Component getChar()
    {
        return gameObject.GetComponent<Character>();
    }
    public string getAb()
    {
        return abType;
    }
    public int calculateValue()
    {
        int value = 0;
        //value = atk+def+mRange+aRange+actRange+aBoost+dBoost+mBoost;
        //value *= ((health+hpBoost) / 10);
        // value *= Mathf.Abs((int)Vector3.Distance(pos, new Vector3(55, 55)) - 70);
        value += (((health + hpBoost) / 10) * (def + dBoost));
        value += ((atk + aBoost) * (aRange)); //* (70 - Mathf.Abs((int)Vector3.Distance(pos, new Vector3(55, 55)))))
        value += ((mRange + mBoost) * actRange);

        int dis = (Mathf.Abs((int)Vector3.Distance(pos, new Vector3(55, 55))) - 5) / 10;
        value/=dis;

        if (!owner)
            value *= -1;

        if(health+hpBoost<=0)
            value = 0;

        return value;
    }
    public int calculateDamValue(int pow)
    {
        hpBoost -= (25 * pow /(def+dBoost));
        if (health + hpBoost <= 0)
            hpBoost =100- health;


        return calculateValue();
    }
    public int calculateHealValue()
    {
        if(health-hpBoost>0)
        hpBoost += 25;

        if (health+hpBoost>=100)
            hpBoost=100-health;

        return calculateValue();
    }
    public int calculateMoveValue(Vector3 posb)
    {
       pos=posb;
        return calculateValue();
    }
    public int calculateBoostValue(string action)
    {
        if (action.Equals("boostA"))
            aBoost += 1;

        if (action.Equals("boostD"))
            dBoost += 1;

        if (action.Equals("boostM"))
            mBoost += 1;

        return calculateValue();
    }
    public int calculateLowerValue(string action)
    {
        if (action.Equals("lowerA")&&atk+aBoost>1)
            aBoost -= 1;
        return calculateValue();
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
