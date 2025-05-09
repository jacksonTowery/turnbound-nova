using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abillity
{
    // Start is called before the first frame update
   public  Abillity()
    {

    }

    public  Character useAnAbillity(string type, Character character)
    {
        if (type.Equals("heal"))
        return healChar(character);

        if (type.Equals("boostA"))
            return boostA(character);

        if (type.Equals("boostD"))
            return boostD(character);

        if (type.Equals("boostM"))
            return boostM(character);

        if (type.Equals("lowerA"))
            return lowerA(character);

        if (type.Equals("lowerD"))
            return lowerD(character);

        if (type.Equals("lowerM"))
            return lowerM(character);

        if (type.Equals("Poison"))
            return Poison(character);

        if (type.Equals("swapA/D"))
            return swapAD(character);

        if (type.Equals("Dub"))
            return dub(character);
        if (type.Equals("Attack"))
            return attack(character);
        if (type.Equals("DissableAb"))
            return disableAb(character);
        if (type.Equals("Thorn"))
            return Thorn(character);

        return character;
    }
    public Character healChar(Character character)
    {
       character.heal(25);
        //Debug.Log("Health: " + character.getHealth());
        return character;
    }
    public Character boostA(Character character)
    {
        character.setAtk(character.getAtk() + 1);
        //Debug.Log("Attack: " + character.getAtk());
        return character;
    }
    public Character boostD(Character character)
    {
        character.setDef(character.getDef() + 1);
        //Debug.Log("Deffense: " + character.getDef());
        return character;
    }
    public Character boostM(Character character)
    {
        character.setmRange(character.getmRange() + 1);
        //Debug.Log("Movement Range: " + character.getmRange());
        return character;
    }

    public Character lowerA(Character character)
    {
        if(character.getAtk()>1)
        {
            character.setAtk(character.getAtk()-1);
        }
        return character;
    }
    public Character lowerD(Character character)
    {
        if (character.getDef() > 1)
        {
            character.setDef(character.getDef() - 1);
        }
        return character;
    }
    public Character lowerM(Character character)
    {
        if (character.getmRange() > 1)
        {
            character.setmRange(character.getmRange() - 1);
        }
        return character;
    }
    public Character Poison(Character character)
    {
        character.poison();
        //Debug.Log("Movement Range: " + character.getmRange());
        return character;
    }
    public Character swapAD(Character character)
    {
        int a=character.getAtk();
        character.setAtk(character.getDef());
        character.setDef(a);
        //Debug.Log("Movement Range: " + character.getmRange());
        return character;
    }
    public Character dub(Character character)
    {
        character.dub();
        //Debug.Log("Movement Range: " + character.getmRange());
        return character;
    }
    public Character attack(Character character)
    {
        int num = UnityEngine.Random.Range(25, 35);
        character.takeDammage(4, num);
        //Debug.Log("Movement Range: " + character.getmRange());
        return character;
    }
    public Character disableAb(Character character)
    {
        character.usedAction();
        //Debug.Log("Movement Range: " + character.getmRange());
        return character;
    }
    public Character Thorn(Character character)
    {
        character.gainThorn();
        //Debug.Log("Movement Range: " + character.getmRange());
        return character;
    }
}
