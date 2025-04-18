using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static System.Collections.Specialized.BitVector32;
using static UnityEngine.GraphicsBuffer;

public class ValueCalculator
{
    // Start is called before the first frame update
    Character character = null;
    int deccision = 0;
    Vector3 pos = Vector3.zero;
    Character target = null;
    int value = int.MinValue;
    int posX = 0;
    int posY = 0;
    Character[] characters = new Character[3];
    int[] deccisions = new int[3];
    Vector3[] positions = new Vector3[3];
    Character[] targets = new Character[3];
    int[] targetsX = new int[3];
    int[] targetsY = new int[3];
    int testD = 0;
    int testX = 0;
    int testY = 0;
    Character testChar = null;
    Character testTar = null;
    public ValueCalculator()
    {
    }
    public int endOfTurn(Character characterT, int deccisionT, Vector3 posT, Character targetT, List<Character> charListCopy, int xT, int yT, int action, int v)
    {
        if (action == 0)
        {
            return v;
        }
        List<Character> charList = charListCopy;









        return v;
    }
    public void calculate(Character characterT, int deccisionT, Vector3 posT, Character targetT, List<Character> charListCopy, int xT, int yT, int a)
    {
        //Debug.Log(characterT);
        int valueT = 0;
        List<Character> charList = charListCopy;
        if (deccisionT == 0)
        {
            foreach (Character c in charList)
            {
                // float t = 0;
                if (c == characterT)
                {
                    //t = Vector3.Distance(posT, new Vector3(55, 55)) - 70;
                    valueT += c.calculateMoveValue(posT);
                }
                else
                {
                    // t = Vector3.Distance(c.getPosition(), new Vector3(55, 55)) - 70;
                    valueT += c.calculateValue();
                }

                // if (t < 0)
                //     t *= -1;


                // valueT += c.calculateValue() * (int)t;

            }
        }
        else if (deccisionT == 1)
        {
            foreach (Character c in charList)
            {


                if (c == targetT)
                {
                    valueT += c.calculateDamValue(characterT.getAtk());
                }
                else
                {
                    valueT += c.calculateValue();
                }


            }
        }
        if (deccisionT == 2)
        {
            if (characterT.getAction().Equals("heal"))
            {
                foreach (Character c in charList)
                {
                    if (c == targetT)
                    {
                        valueT += c.calculateHealValue();
                    }
                    else
                    {
                        valueT += c.calculateValue();
                    }
                }
            }
            else
            {
                foreach (Character c in charList)
                {
                    if (c == targetT && characterT.getActType())
                    {
                        valueT += c.calculateBoostValue(characterT.getAb());
                    }
                    else if (c == targetT && !characterT.getActType())
                    {
                        valueT += c.calculateLowerValue(characterT.getAb());
                    }
                    else
                    {
                        valueT += c.calculateValue();
                    }
                }
            }
        }
        if (valueT > value)
        {
            character = characterT;
            //Debug.Log(characterT);
            deccision = deccisionT;
            pos = posT;
            value = valueT;
            target = targetT;
            posX = xT;
            posY = yT;
        }
        
    }
    public void calculate(List<Character> charList)
    {
        int valueT = 0;
        foreach (Character c in charList)
        {
            valueT += c.calculateValue();
        }
        if (valueT > value)
        {

            value = valueT;

        }
    }
    public int calculateValue(List<Character> charList)
    {
        int valueT = 0;
        foreach (Character c in charList)
        {
            if (c!=null)
            valueT += c.calculateValue();
        }
        return valueT;
    }
    public List<Character> updateList(List<Character> charList, Character c)
    {
        for (int i = 0; i < charList.Count; i++)
        {
            if (charList[i].GameObject() == c.GameObject())
            {
                charList[i] = c;
            }
        }


        return charList;
    }

    public int getValue()
    {
        return value;
    }
    public bool containsCharacter(Vector3 position, List<Character> charList)
    {
        foreach (Character characterPos in charList)
        {
            if (characterPos!=null&& characterPos.getPosition()!=null &&characterPos.getPosition() == position)
            {
                return true;
            }
        }
        return false;
    }
    public Character getCharacter()
    {
        return character;
    }
    public Character getTarget()
    {
        return target;
    }
    public int getAction()
    {
        return deccision;
    }
    public void getPosition(out int x, out int y)
    {
        x = posX;
        y = posY;
    }
    public void reset()
    {
        character = null;
        deccision = 0;
        pos = Vector3.zero;
        target = null;
        value = int.MinValue;
        posX = 0;
        posY = 0;
    }

    public void calculateAll(List<Character> charList, PathFinding pathFinding, int action, string s)
    {

        List<Character> charListCopy = charList;
        // for (int a = 0; a < action; a++)
        //  {
        for (int a = action; a >= 0; a--)
        {
            if (a == 0)
            {
                calculate(charList);
            }
            foreach (Character c in charListCopy)
            {
                //Debug.Log(c.getName()+" "+charList.Count);
                if (c.getIsOwner())
                {
                    // Debug.Log("Is owner");
                    foreach (Character t in charListCopy)
                    {
                        // Debug.Log(t + " targetted");
                        for (int i = 0; i < 3; i++)
                        {
                            // Debug.Log("i"); good
                            if (i == 0)
                            {

                                pathFinding.getGrid().GetXY(c.getPosition(), out int xPos, out int yPos);
                                int maxMove = c.getmRange() + 1;
                                List<PathNode> path;
                                for (int x = xPos - maxMove; x <= xPos + maxMove; x++)
                                {
                                    for (int y = yPos - maxMove; y <= yPos + maxMove; y++)
                                    {
                                        if (pathFinding.getGrid().hasValue(x, y))
                                        {
                                            if (pathFinding.getGrid().getValue(x, y).getIsWalkable())
                                            {
                                                path = pathFinding.FindPath(xPos, yPos, x, y, true);
                                                if (path.Count <= maxMove && pathFinding.checkIsWalkable(path) && !c.getMoved() && c.getPosition() != new Vector3(x, y) && !containsCharacter(new Vector3(x, y), charListCopy))
                                                {
                                                    // c.calculateMoveValue(new Vector3(xPos, yPos));
                                                    calculate(c, i, new Vector3(xPos, yPos), t, charListCopy, xPos, yPos, action);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (i == 1)
                            {
                                float dis = Vector3.Distance(t.getPosition(), c.getPosition());
                                dis /= 10;
                                if (!t.getIsOwner() && !c.getAttack() && dis <= c.getaRange())
                                {
                                    //Debug.Log(c);
                                    calculate(c, i, Vector3.zero, t, charListCopy, 0, 0, action);
                                }
                            }
                            if (i == 2)
                            {
                                float dis = Vector3.Distance(t.getPosition(), c.getPosition());
                                dis /= 10;
                                //Debug.Log(c+" "+!c.getAct());
                                if (dis <= c.getactRange() && !c.getAct() && c.getActType() == t.getIsOwner())
                                {
                                    //Debug.Log(c);
                                    calculate(c, i, Vector3.zero, t, charListCopy, 0, 0, action);
                                }
                            }
                            // Debug.Log(Mathf.Abs(Vector3.Distance(new Vector3(posX, posY), c.getPosition()))+",="+c.getmRange()+1);
                        }
                    }
                }
            }
            // }
            //Debug.Log(character + " " + target + " " + deccision + " " + pos);
        }
    }

    public void calculateAll(List<Character> charList, PathFinding pathFinding, int action)
    {

        List<Character> charListCopy = charList;
        // for (int a = 0; a < action; a++)
        //  {
        for (int a = action; a >= 0; a--)
        {
            if (a == 0)
            {
                calculate(charList);
            }
            foreach (Character c in charListCopy)
            {
                //Debug.Log(c.getName()+" "+charList.Count);
                if (c.getIsOwner())
                {
                    // Debug.Log("Is owner");
                    foreach (Character t in charListCopy)
                    {
                        // Debug.Log(t + " targetted");
                        for (int i = 0; i < 3; i++)
                        {
                            // Debug.Log("i"); good
                            if (i == 0)
                            {

                                pathFinding.getGrid().GetXY(c.getPosition(), out int xPos, out int yPos);
                                int maxMove = c.getmRange() + 1;
                                List<PathNode> path;
                                for (int x = xPos - maxMove; x <= xPos + maxMove; x++)
                                {
                                    for (int y = yPos - maxMove; y <= yPos + maxMove; y++)
                                    {
                                        if (pathFinding.getGrid().hasValue(x, y))
                                        {
                                            if (pathFinding.getGrid().getValue(x, y).getIsWalkable())
                                            {
                                                path = pathFinding.FindPath(xPos, yPos, x, y, true);
                                                if (path.Count <= maxMove && pathFinding.checkIsWalkable(path) && !c.getMoved() && c.getPosition() != new Vector3(x, y) && !containsCharacter(new Vector3(x, y), charListCopy))
                                                {
                                                    // c.calculateMoveValue(new Vector3(xPos, yPos));
                                                    calculate(c, i, new Vector3(xPos, yPos), t, charListCopy, xPos, yPos, action);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (i == 1)
                            {
                                float dis = Vector3.Distance(t.getPosition(), c.getPosition());
                                dis /= 10;
                                if (!t.getIsOwner() && !c.getAttack() && dis <= c.getaRange())
                                {
                                    //Debug.Log(c);
                                    calculate(c, i, Vector3.zero, t, charListCopy, 0, 0, action);
                                }
                            }
                            if (i == 2)
                            {
                                float dis = Vector3.Distance(t.getPosition(), c.getPosition());
                                dis /= 10;
                                //Debug.Log(c+" "+!c.getAct());
                                if (dis <= c.getactRange() && !c.getAct() && c.getActType() == t.getIsOwner())
                                {
                                    //Debug.Log(c);
                                    calculate(c, i, Vector3.zero, t, charListCopy, 0, 0, action);
                                }
                            }
                            // Debug.Log(Mathf.Abs(Vector3.Distance(new Vector3(posX, posY), c.getPosition()))+",="+c.getmRange()+1);
                        }
                    }
                }
            }
            // }
            //Debug.Log(character + " " + target + " " + deccision + " " + pos);
        }
    }
   // public void calculateR(int actions, Character a, Character b, Character c, Character d, Character e, Character f,  bool first)
        public void calculateR(int actions, List<Character> list, bool first)
    {

        //List<Character> list = new List<Character>();
        //list.Add(a);
       // list.Add(b);
       // list.Add(c);
       // list.Add(d);
       // list.Add(e);
       // list.Add(f);
        if (actions == 0)
        {
            if (calculateValue(list) > value)
            {
                value = calculateValue(list);
                //Debug.Log(value);
                character = testChar;
                target = testTar;
                deccision = testD;
                posX = testX;
                posY = testY;
            }
            
        }
        else
        {
            actions -= 1;

            foreach (Character chara in list)
            {
                if (chara != null && chara.getIsOwner())
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                        {
                            // pathFinding.getGrid().GetXY(c.getPosition(), out int xPos, out int yPos);
                            // int xPos = (int)c.getPosition().x;
                            //int yPos = c.getPosition().y;
                            //int xPos = Mathf.FloorToInt((chara.getPosition().x/10f));
                            //int yPos = Mathf.FloorToInt((chara.getPosition().y / 10f));
                            int xPos = (int)(chara.getPosition().x - 5) / 10;
                            int yPos = (int)(chara.getPosition().y - 5) / 10;

                            int maxMove = chara.getmRange()+1;
                            //int maxMove = chara.getmRange();
                            //List<PathNode> path;
                            int path = 0;
                            //for (int x = xPos - maxMove; x <= xPos + maxMove; x++)
                            for (int x=0; x<=10; x++)
                            {
                                //for (int y = yPos - maxMove; y <= yPos + maxMove; y++)
                                for (int y=0; y<=10; y++)
                                {
                                    //if (pathFinding.getGrid().hasValue(x, y))
                                    {
                                       // if (pathFinding.getGrid().getValue(x, y).getIsWalkable())
                                        {
                                            //path = pathFinding.FindPath(xPos, yPos, x, y, true);
                                            //path=(int)((Math.Abs(chara.getPosition().x - x)/10)+ (Math.Abs(chara.getPosition().y - y) / 10)-1);
                                            path = (int)((Math.Abs(xPos - x)) + (Math.Abs(yPos - y)))-1;
                                            //Debug.Log((path<=maxMove)+",  "+path+"<="+ maxMove);
                                            //Debug.Log(path <= maxMove && !chara.getMoved() && chara.getPosition() != new Vector3(x, y) && !containsCharacter(new Vector3(x, y), list));
                                            //Debug.Log(chara.getMoved());
                                           // Debug.Log(chara.getName() + ": " + xPos + ", " + yPos);
                                            //Debug.Log(chara.getName() + " to: " + x + ", " + y+". Path = "+path);
                                            //Debug.Log(chara.getName()+" from "+xPos+ ", " +yPos +" to: "+x+", "+y+". Path = "+path+" Can Move: "+!chara.getMoved());
                                            if (path <= maxMove && !chara.getMoved() && chara.getPosition() != new Vector3(x, y) && !containsCharacter(new Vector3(x, y), list))
                                            {
                                                //Debug.Log(chara.getName()+" from "+xPos+ ", " +yPos +" to: "+x+", "+y+". Path = "+path);
                                                //Debug.Log("Good");
                                                chara.calculateMoveValue(new Vector3(x, y));
                                                if (first)
                                                {
                                                    testChar = chara;
                                                    testX = x;
                                                    //Debug.Log(testX);
                                                    testY = y;
                                                    //Debug.Log(testX+", "+testY);
                                                   
                                                    //Debug.Log("Path = "+path+", Range = "+maxMove);
                                                    testD = i;
                                                }

                                                //chara.Moved();//Preventing both infinite loop and checking all movement options

                                                //calculate(c, i, new Vector3(xPos, yPos), t, charListCopy, xPos, yPos, action);
                                                //PathFinding p=pathFinding;

                                                //int dis=(int)(((Math.Abs(c.getPosition().x - x)-5)/10)+((Math.Abs(c.getPosition().y-y)-5)/10)-1);


                                               // Debug.Log(chara.getPosition().x + ", " + chara.getPosition().y);
                                               // Debug.Log(x + ", " + y);
                                                //calculateR(actions, a, b, c, d, e, f,false);
                                                calculateR(actions,list, false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (Character t in list)
                            {
                                if (t != null)
                                {
                                    if (i == 1)
                                    {
                                        //float dis = Vector3.Distance(t.getPosition(), c.getPosition());
                                        // dis /= 10;
                                        int dis = (int)((Math.Abs(chara.getPosition().x - t.getPosition().x) / 10) + (Math.Abs(chara.getPosition().y - t.getPosition().y) / 10) - 1);
                                        if (!t.getIsOwner() && !chara.getAttack() && dis <= chara.getaRange())
                                        {
                                            //Debug.Log(c.getName()+": "+c.getPosition()+", "+t.getName()+": "+t.getPosition());
                                            //calculate(c, i, Vector3.zero, t, charListCopy, 0, 0, action);
                                            t.calculateDamValue(chara.getAIAtk());
                                            if (first)
                                            {
                                                testChar = chara;
                                                testTar = t;
                                                testD = i;
                                            }
                                            //chara.attack();
                                            //calculateR(actions, a, b, c, d, e, f, false);
                                            calculateR(actions, list, false);
                                        }
                                    }
                                    else if (i == 2)
                                    {
                                        //float dis = Vector3.Distance(t.getPosition(), c.getPosition());
                                        //dis /= 10;
                                        int dis = (int)((Math.Abs(chara.getPosition().x - t.getPosition().x) / 10) + (Math.Abs(chara.getPosition().y - t.getPosition().y) / 10) - 1);
                                        //Debug.Log(c+" "+!c.getAct());
                                        if (dis <= chara.getactRange() && !chara.getAct() && chara.getActType() == t.getIsOwner())
                                        {
                                           // chara.acted();
                                            // Debug.Log(chara.getAction().Equals("heal"));
                                            //Debug.Log(c);
                                            //  calculate(c, i, Vector3.zero, t, charListCopy, 0, 0, action);
                                            if (chara.getAction().Equals("heal") && t.getHealth() != 100)
                                            {
                                                //Debug.Log(t.getHealth()!=100);
                                                t.calculateHealValue();
                                                if (first)
                                                {
                                                    testChar = chara;
                                                    testTar = t;
                                                    testD = i;
                                                }
                                                //calculateR(actions, a, b, c, d, e, f, false);
                                                calculateR(actions, list, false);

                                            }
                                            else if (chara.getActType() && !(chara.getAction().Equals("heal")))
                                            {
                                                t.calculateBoostValue(chara.getAb());
                                                if (first)
                                                {
                                                    testChar = chara;
                                                    testTar = t;
                                                    testD = i;
                                                }
                                                //calculateR(actions, a, b, c, d, e, f, false);
                                                calculateR(actions, list, false);
                                            }
                                            else if (!chara.getActType())
                                            {
                                                t.calculateLowerValue(chara.getAb());
                                                if (first)
                                                {
                                                    testChar = chara;
                                                    testTar = t;
                                                    testD = i;
                                                }
                                                //calculateR(actions, a, b, c, d, e, f, false);
                                                calculateR(actions, list, false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

