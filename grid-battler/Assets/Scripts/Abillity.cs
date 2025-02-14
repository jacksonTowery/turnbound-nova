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

        return character;
    }
    public Character healChar(Character character)
    {
       character.heal(25);
        Debug.Log("Health: " + character.getHealth());
        return character;
    }
    public Character boostA(Character character)
    {
        character.setAtk(character.getAtk() + 1);
        Debug.Log("Attack: " + character.getAtk());
        return character;
    }
    public Character boostD(Character character)
    {
        character.setDef(character.getDef() + 1);
        Debug.Log("Deffense: " + character.getDef());
        return character;
    }
    public Character boostM(Character character)
    {
        character.setmRange(character.getmRange() + 1);
        Debug.Log("Movement Range: " + character.getmRange());
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

}
