using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CarriedOnData : MonoBehaviour
{
    [SerializeField] Dropdown carriedA;
    [SerializeField] Dropdown carriedB;
    [SerializeField] Dropdown carriedC;
    [SerializeField] Dropdown carriedD;
    [SerializeField] Dropdown carriedE;
    [SerializeField] Dropdown carriedF;
    [SerializeField] Image spriteA;
    [SerializeField] Image spriteB;
    [SerializeField] Image spriteC;
    [SerializeField] Image spriteD;
    [SerializeField] Image spriteE;
    [SerializeField] Image spriteF;
    [SerializeField] Image status;
    [SerializeField] Text statusText;
    [SerializeField] GameObject obj;
    [SerializeField] List<Character> charOptions;
    public void Start()
    {
        CharacterList.charList = charOptions;
        addToList(carriedA);
        addToList(carriedB);
        addToList(carriedC);
        addToList(carriedD);
        addToList(carriedE);
        addToList(carriedF);
    }
    public void addToList(Dropdown dropdown)
    {
        List<string> list = new List<string>();
        foreach (Character character in CharacterList.charList)
        {
            list.Add(character.getName());
        }
        dropdown.AddOptions(list);
    }
    public bool hasUpdated = true;
    private void Update()
    {
        if (!hasUpdated && obj.GetComponent<Character>() != null)
        {
            setStatus();
            //hasUpdated = true;
        }
        //if(!hasUpdated)
         //   hasUpdated = true;
    }
    public void setSelectCharA()
    {
        CharList.selectCharA = carriedA.options[carriedA.value].text;
        //Debug.Log(CharList.selectCharA);
        setSprite(spriteA, carriedA.options[carriedA.value].text);
        setChar(CharList.selectCharA);
    }
    public void setSelectCharB()
    {
        CharList.selectCharB = carriedB.options[carriedB.value].text;
        //Debug.Log(CharList.selectCharA);
        setSprite(spriteB, carriedB.options[carriedB.value].text);
        setChar(CharList.selectCharB);
    }
    public void setSelectCharC()
    {
        CharList.selectCharC = carriedC.options[carriedC.value].text;
        //Debug.Log(CharList.selectCharA);
        setSprite(spriteC, carriedC.options[carriedC.value].text);
        setChar(CharList.selectCharC);
    }
    public void setSelectCharD()
    {
        CharList.selectCharD = carriedD.options[carriedD.value].text;
        //Debug.Log(CharList.selectCharA);
        setSprite(spriteD, carriedD.options[carriedD.value].text);
        setChar(CharList.selectCharD);
    }
    public void setSelectCharE()
    {
        CharList.selectCharE = carriedE.options[carriedE.value].text;
        //Debug.Log(CharList.selectCharA);
        setSprite(spriteE, carriedE.options[carriedE.value].text);
        setChar(CharList.selectCharE);
    }
    public void setSelectCharF()
    {
        CharList.selectCharF = carriedF.options[carriedF.value].text;
        //Debug.Log(CharList.selectCharA);
        setSprite(spriteF, CharList.selectCharF);
        setChar(CharList.selectCharF);
    }
    public void setSprite(Image i, string name)
    {
        Sprite sprite= Resources.Load<Sprite>("Sprites/Human (2)");
        if (name.Equals("Human"))
        {
            sprite = Resources.Load<Sprite>("Sprites/Human (2)");
        }
        else if (name.Equals("Tank"))
        {
            sprite = Resources.Load<Sprite>("Sprites/Tank");
            //Debug.Log("Tank");
        }
        else if (name.Equals("Robot"))
        {
            sprite = Resources.Load<Sprite>("Sprites/Medic");
        }
        else if (name.Equals("Alien"))
        {
            sprite = Resources.Load<Sprite>("Sprites/Alien");
        }
        else if (name.Equals("Astronomer"))
        {
            sprite = Resources.Load<Sprite>("Sprites/Long_Range");
        }
        else if (name.Equals("Axellottle"))
        {
            sprite = Resources.Load<Sprite>("Sprites/Squishy");
        }
        i.sprite = sprite;
       // Debug.Log("good "+name);
        
    }
    public void setChar(string name)
    {
        //obj = new GameObject();
        //obj.AddComponent<SpriteRenderer>();
        Destroy(obj.GetComponent<Character>());
        if (name.Equals("Human"))
        {
            obj.AddComponent<Human>();
        }
        else if (name.Equals("Tank"))
        {
            obj.AddComponent<Tank>();
        }
        else if (name.Equals("Robot"))
        {
            obj.AddComponent<Robot>();
        }
        else if (name.Equals("Alien"))
        {
            obj.AddComponent<Alien>();
        }
        else if (name.Equals("Astronomer"))
        {
            obj.AddComponent<Astronomer>();
        }
        else if (name.Equals("Axellottle"))
        {
            obj.AddComponent<Axellottle>();
        }
        else
        {
            obj.AddComponent<Character>();
        }
        hasUpdated = false;
    }
    public void setStatus()
    {
        Character stat=obj.GetComponent<Character>();
        status.sprite = stat.GetComponent<SpriteRenderer>().sprite;
        string name = stat.getName();
        string health = "" + stat.getHealth();
        string atk = "" + stat.getAtk();
        string def = "" + stat.getDef();
        string m = "" + stat.getmRange();
        string ar = "" + stat.getaRange();
        string acr = "" + stat.getactRange();
        string ab = "" + stat.getAction();
        statusText.text = "Name: " + name + "\r\nHealth: " + health + "\r\nAttack: " + atk + "\r\nDefense: " + def + "\r\nMovement Range: " + m + "\r\nAttack Range: " + ar + "\r\nAbillity Range: " + acr + "\r\nAbillity: " + ab;
       // Debug.Log("good");
    }
}

