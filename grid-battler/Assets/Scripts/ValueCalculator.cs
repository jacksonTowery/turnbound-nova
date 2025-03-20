using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ValueCalculator
{
    // Start is called before the first frame update
    Character character=null;
    int deccision=0;
    Vector3 pos=Vector3.zero;
    Character target=null;
    int value = int.MinValue;
    int posX = 0;
    int posY = 0;
   public ValueCalculator()
    {
    }
    public void calculate(Character characterT, int deccisionT, Vector3 posT,Character targetT, List<Character> charList, int xT, int yT)
    {
        //Debug.Log(characterT);
        int valueT = 0;
        if(deccisionT==0)
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
        else if(deccisionT==1)
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
                foreach(Character c in charList)
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
                    if (c == targetT&&characterT.getActType())
                    {
                        valueT += c.calculateBoostValue();
                    }
                    else if ( c== targetT && !characterT.getActType())
                    {
                        valueT += c.calculateLowerValue();
                    }
                    else
                    {
                        valueT += c.calculateValue();
                    }
                }
            }
        }
        if (valueT>value)
        {
            character = characterT;
            //Debug.Log(characterT);
            deccision=deccisionT;
            pos = posT;
            value = valueT;
            target = targetT;
            posX=xT;
            posY=yT;
        }
    }
    public int getValue()
    {
        return value;
    }
    public bool containsCharacter(Vector3 position, List<Character>charList)
    {
        foreach (Character characterPos in charList)
        {
            if (characterPos.getPosition() == position)
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
        x=posX; 
        y=posY;
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
   
    public void calculateAll(List<Character>charList, PathFinding pathFinding)
    {
        foreach( Character c in charList)
        {
            //Debug.Log(c.getName()+" "+charList.Count);
            if (c.getIsOwner())
            {
               // Debug.Log("Is owner");
                foreach(Character t in charList)
                {
                   // Debug.Log(t + " targetted");
                    for (int i = 0;i<3;i++)
                    {
                       // Debug.Log("i"); good
                        if (i == 0)
                        {
                            /* for (int x = 5; x <= 105; x += 10)
                             {
                                 for (int y = 5; y <= 105; y += 10)
                                 {
                                     if (x >= 0 && y >= 0 && x <= 110 && y <= 110)
                                     {
                                         pathFinding.getGrid().GetXY(c.getPosition(), out int xStart, out int yStart);
                                         pathFinding.getGrid().GetXY(new Vector3(x,y), out int xEnd, out int yEnd);
                                         List<PathNode> path = pathFinding.FindPath(xStart, yStart, x, y, true);
                                         //float dis = Mathf.Abs(Vector3.Distance(new Vector3(x, y), c.getPosition()));
                                         // dis /= 10;
                                         //Debug.Log(dis +"<="+ c.getmRange() + 1);
                                         //Debug.Log(path.Count+" "+c+" "+charList);
                                            // Debug.Log(path.Count);//Error

                                         // if (dis <= c.getmRange() + 1 && !containsCharacter(new Vector3(x, y), charList)&&!c.getMoved())
                                         //{
                                          if (pathFinding.getGrid().hasValue(x,y)&& path.Count <= c.getmRange() + 1 && !containsCharacter(new Vector3(x, y), charList) && !c.getMoved())
                                         {
                                             //Try recreating pathfinding.setpathsprite
                                             //Debug.Log(c);
                                             calculate(c, i, new Vector3(x, y), t, charList);
                                         }
                                     }
                                 }
                             }*/
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
                                            if (path.Count <= maxMove && pathFinding.checkIsWalkable(path))
                                            {
                                                calculate(c, i, new Vector3(x, y), t, charList, x, y);

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
                            if (!t.getIsOwner()&&!c.getAttack()&& dis <= c.getaRange())
                            {
                                //Debug.Log(c);
                                calculate(c,i,Vector3.zero,t,charList, 0, 0);
                            }
                        }
                        if(i == 2)
                        {
                            float dis = Vector3.Distance(t.getPosition(), c.getPosition());
                            dis /= 10;
                            //Debug.Log(c+" "+!c.getAct());
                            if(dis <= c.getactRange() && !c.getAct() && c.getActType() == t.getIsOwner())
                            {
                                //Debug.Log(c);
                                calculate(c, i, Vector3.zero, t, charList, 0, 0);
                            }
                        }
                       // Debug.Log(Mathf.Abs(Vector3.Distance(new Vector3(posX, posY), c.getPosition()))+",="+c.getmRange()+1);
                    }
                }
            }
        }
        //Debug.Log(character + " " + target + " " + deccision + " " + pos);
        
    }

}
