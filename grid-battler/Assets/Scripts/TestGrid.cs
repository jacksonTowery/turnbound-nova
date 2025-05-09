using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestGrid : MonoBehaviour
{
    // Start is called before the first frame update
    private PathFinding pathFinding;
    //[SerializeField] private CharacterPathfinding characterPathfinding;
    [SerializeField] private GameObject objA;
    [SerializeField] private GameObject objB;
    [SerializeField] private GameObject objC;
    [SerializeField] private GameObject objD;
    [SerializeField] private GameObject objE;
    [SerializeField] private GameObject objF;
    // [SerializeField] private Character characterA;
    //[SerializeField] private Character characterB;
    //[SerializeField] private Character characterC;
    //[SerializeField] private Character characterD;
    //[SerializeField] private Character characterE;
   // [SerializeField] private Character characterF;
    [SerializeField] private Text turnIdentifier;
    [SerializeField] private Text StatDisplay;
    [SerializeField] private Text actionCount;
    [SerializeField] private UnityEngine.UI.Image StatDisplaySprite;
    [SerializeField] private UnityEngine.UI.Image CurrentSprite;
    [SerializeField] private Text currentDisplay;
    [SerializeField] private Text Victor;
    //[SerializeField] private List<Character> characters;
    private Character character;
    private Character targetedCharacter;
    private List<Character> characters;
    bool attack;
    bool move;
    bool act;
    bool target;
    private int actions = 3;
    int turn = 0;
    ValueCalculator calc = new ValueCalculator();
    // [SerializeField] public Camera camera;
    // [SerializeField] public Component.camera camera;
    void Start()
    {
        Sprite spriteP = Resources.Load<Sprite>("Sprites/Selectable_Tile");
        Sprite spriteD = Resources.Load<Sprite>("Sprites/Tile");
        //Debug.Log(spriteD);
        //  Grid<bool> grid = new Grid<bool>(11, 11, 10f, new Vector3(20,0), ()=> new bool());
        pathFinding = new PathFinding(11, 11, spriteP,spriteD);
         characters = new List<Character>();
        objA = attatchChar(CharList.selectCharA,objA);
        //objA.GetComponent<Character>().SetPosition(objA.GetComponent<Character>().getPosition());
         //characters.Add(characterA);
        //Debug.Log(CharList.selectChar);
         characters.Add(objA.GetComponent<Character>());
         //characters.Add(characterB);
         objB=attatchChar(CharList.selectCharB,objB);
        //objB.GetComponent<Character>().SetPosition(objB.GetComponent<Character>().getPosition());
        characters.Add(objB.GetComponent<Character>());
        //characters.Add(characterC);
        objC = attatchChar(CharList.selectCharC, objC);
       // objC.GetComponent<Character>().SetPosition(objC.GetComponent<Character>().getPosition());
        characters.Add(objC.GetComponent<Character>());
        //characters.Add(characterD);
        objD = attatchChar(CharList.selectCharD, objD);
       // objD.GetComponent<Character>().SetPosition(objD.GetComponent<Character>().getPosition());
        characters.Add(objD.GetComponent<Character>());
        // characters.Add(characterE);
        objE = attatchChar(CharList.selectCharE, objE);
        //objE.GetComponent<Character>().SetPosition(objE.GetComponent<Character>().getPosition());
        characters.Add(objE.GetComponent<Character>());
        // characters.Add(characterF);
        objF = attatchChar(CharList.selectCharF, objF);
        //objF.GetComponent<Character>().SetPosition(objF.GetComponent<Character>().getPosition());
        characters.Add(objF.GetComponent<Character>());
        //changeCharacter(characterA);
        //changeTargetedCharacter(characterE);
        int t = 0;
        foreach (Character c in characters)
        {
            c.calculateMoveValue(c.getPosition());
            if (t <= 2)
                c.change();

            t++;
        }
        
    }
    public GameObject attatchChar(String name, GameObject obj)
    {
      //  obj.AddComponent<Character>();
        if (name.Equals("Astronaut"))
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
        else if (name.Equals("Axellotle"))
        {
            obj.AddComponent<Axellottle>();
        }
        else
        {
            attatchChar(randChar(), obj);
        }

       /* foreach (Character c in CharacterList.charList)
        {
            Component component=c.getChar();
            if (name.Equals(c.getName()))
            {
                obj.GetComponent<Character>() = c;
            }
        }*/

        return obj;
    }
    public string randChar()
    {
        int num = UnityEngine.Random.Range(0, 6);
        if(num == 0)
        {
            return "Astronaut";
        }
        else if(num == 1)
        {
            return "Tank";
        }
        else if(num == 2)
        {
            return "Astronomer";
        }
        else if(num == 3)
        {
            return "Alien";
        }
        else if (num == 4)
        {
            return "Robot";
        }
        else if(num == 5)
        {
            return "Axellottle";
        }
        return "Astronaut";
    }
    public void changeCharacter(Character character)
    {
        if(characters.Contains(character))
        {
            this.character = character;
        }
        updateCurrentDisplay(character);
    }
    public void changeTargetedCharacter(Character tarCharacter)
    {
        if (characters.Contains(tarCharacter))
        {
            this.targetedCharacter = tarCharacter;
        }
    }
    public bool containsCharacter(Vector3 position)
    {
        pathFinding.getGrid().GetXY(position, out int xEnd, out int yEnd);
        Vector3 gridPosition=new Vector3(xEnd, yEnd);
        Vector3 characterPosition;
        foreach (Character characterPos in characters)
        {
            if (characterPos != null)
            {
                pathFinding.getGrid().GetXY(characterPos.transform.position, out int xChar, out int yChar);
                characterPosition = new Vector3(xChar, yChar);
                //Debug.Log("Clicked: " + gridPosition + ", Character: " + characterPos.transform.position);
                if (characterPosition == gridPosition)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public Character GetCharacter(Vector3 pos)
    {
        pathFinding.getGrid().GetXY(pos, out int xEnd, out int yEnd);
        Vector3 gridPosition = new Vector3(xEnd, yEnd);
        Vector3 characterPosition;
        foreach (Character characterPos in characters)
        {
            pathFinding.getGrid().GetXY(characterPos.transform.position, out int xChar, out int yChar);
            characterPosition = new Vector3(xChar, yChar);
           // Debug.Log("Clicked: " + gridPosition + ", Character: " + characterPos.transform.position);
            if (characterPosition == gridPosition)
            {
                return characterPos;
            }
        }
        return null;

    }
    public void resetList()
    {
        foreach (Character character in characters)
        {
            if(character == null)
            {
                characters.Remove(character);
            }
        }
    }
    public void changeTurn()
    {
        foreach(Character character in characters)
        {
            if(character.getIsOwner())
            {
                character.resetBools();
            }
            character.change();
        }
        resetActionCount();
        pathFinding.getGrid().resetSprites();
        character =null;
        targetedCharacter = null;
        updateCurrentDisplay(character);
        turn += 1;
        turnIdentifier.text = "P" + ((turn % 2)+1);
    }
    public void checkForVictory()
    {
        bool stillAnOpponent = false;
        foreach (Character character in characters)
        {
            if (!character.getIsOwner())
            {
                stillAnOpponent= true;
            }
        }
        if (!stillAnOpponent)
        {
           
            //Debug.Log("Player " + ((turn % 2)+1) + " Wins");
            Victor.text = "Player " + ((turn % 2) + 1) + " Wins";
            CharList.AI=false;
        }
    }
    public bool player1Turn()
    {
        if(turn%2==0)
            return true;

        return false;
    }
    public void updateStatDisplay(Character stat)
    {
        //Debug.Log(stat.calculateValue()*Math.Abs(Vector3.Distance(stat.getPosition(),new Vector3(55,55))-70));
       // Debug.Log(Vector3.Distance(new Vector3(55, 55), new Vector3(105, 105)));
        StatDisplaySprite.sprite=stat.GetComponent<SpriteRenderer>().sprite;
        string name =stat.getName();
        string health=""+stat.getHealth();
        string atk=""+stat.getAtk();
        string def = "" + stat.getDef();
        string m = "" + stat.getmRange(); 
        string ar = "" + stat.getaRange();
        string acr = "" + stat.getactRange();
        string ab=""+stat.getAction();
        StatDisplay.text = "Name: " + name + "\r\nHealth: " + health + "\r\nAttack: " + atk + "\r\nDefense: " + def + "\r\nMovement Range: " + m + "\r\nAttack Range: " + ar + "\r\nAbillity Range: " + acr + "\r\nAbillity: " + ab;
    }
    public void updateCurrentDisplay(Character current)
    {
        if (current == null)
        {
            CurrentSprite.sprite = Resources.Load<Sprite>("Sprites/New Piskel");
            currentDisplay.text = "Select a Character.";
        }
        else
        {
            CurrentSprite.sprite = current.GetComponent<SpriteRenderer>().sprite;
            String state = "";
            if (attack)
            {
                state = " to Attack.";
            }
            else if (move)
            { state = " to Move."; }
            else if (act)
            { state = " to use its Power."; }
            else { state = "."; }
            currentDisplay.text = "" + current.getName() + " is ready" + state;
        }
    }
    public void useAnAction()
    {
        actions--;
        actionCount.text = "" + actions;
    }
    public void resetActionCount()
    {
        actions = 3;
        actionCount.text=""+actions;
    }
    public void attackTrue()
    {
        if (!attack && character != null && !character.getAttack())
        {
            attack = true;
            move = false;
            act = false;
            target = false;
            updateCurrentDisplay(character);
            pathFinding.getGrid().resetSprites();
            pathFinding.getGrid().GetXY(character.getPosition(), out int x, out int y);
            pathFinding.setPathSprite(character.getaRange() + 1, x, y, false);
        }
        else
        {
            attack = false;
            pathFinding.getGrid().resetSprites();
            updateCurrentDisplay(character);
        }
    }
    public void moveTrue() 
    {
        if (!move && character != null && !character.getMoved())
        {
            attack = false;
            move = true;
            act = false;
            target = false;
            updateCurrentDisplay(character);
            pathFinding.getGrid().resetSprites();
            pathFinding.getGrid().GetXY(character.getPosition(), out int x, out int y);
            pathFinding.setPathSprite(character.getmRange() + 1, x, y, true);
            // Debug.Log("move");
        }
        else
        {
            move = false;
            pathFinding.getGrid().resetSprites();
            updateCurrentDisplay(character);
        }
    }
    public void actTrue() 
    {
        if (!act&&character!=null&&!character.getAct())
        {
            attack = false;
            move = false;
            act = true;
            target = false;
            updateCurrentDisplay(character);
            pathFinding.getGrid().resetSprites();
            pathFinding.getGrid().GetXY(character.getPosition(), out int x, out int y);
            pathFinding.setPathSprite(character.getactRange() + 1, x, y, false);
        }
        else
        {
            act = false;
            pathFinding.getGrid().resetSprites();
            updateCurrentDisplay(character);
        }
    }
    public void targetTrue()
    {
        attack = false;
        move = false;
        act = false;
        target=true ;
        updateCurrentDisplay(character);
    }
    public void pass()
    {
        changeTurn();
    }
    public void randomChar()
    {
        int teamSize = 0;
        foreach (Character c in characters)
        {
            if (c.getIsOwner())
            {
               teamSize++;
            }
        }
        int chosen=UnityEngine.Random.Range(0, teamSize);
        teamSize = 0;

        foreach (Character c in characters)
        {
            if (c.getIsOwner())
            {
                if(chosen==teamSize)
                {
                    changeCharacter(c);
                }
                teamSize++;

            }
        }
    }
    public void randomEnemy()
    {
        int teamSize = 0;
        foreach (Character c in characters)
        {
            if (!c.getIsOwner())
            {
                teamSize++;
            }
        }
        int chosen = UnityEngine.Random.Range(0, teamSize);
        teamSize = 0;

        foreach (Character c in characters)
        {
            if (!c.getIsOwner())
            {
                if (chosen == teamSize)
                {
                    changeTargetedCharacter(c);
                }
                teamSize++;

            }
        }
    }
    public void randomTarget(Character ch)
    {
        int teamSize = 0;
        foreach (Character c in characters)
        {
            if (c.getIsOwner()==ch.getActType())
            {
                teamSize++;
            }
        }
        int chosen = UnityEngine.Random.Range(0, teamSize);
        teamSize = 0;

        foreach (Character c in characters)
        {
            if (c.getIsOwner()==ch.getActType())
            {
                if (chosen == teamSize)
                {
                    changeTargetedCharacter(c);
                }
                teamSize++;

            }
        }
    }
    public void Update()
    {
        /* if(Input.GetMouseButtonDown(1))
         {
             Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
             if (containsCharacter(mouseWorldPosition))
             {
                 changeTargetedCharacter(GetCharacter(mouseWorldPosition));
                 Debug.Log("target");
             }
         }

         else*/
        if (!player1Turn()&&CharList.AI)
        {
            // randomChar();
            //Debug.Log(character);
            //int decission = UnityEngine.Random.Range(0, 3);
            // int decission = 0;

            //calc.calculateAll(characters, pathFinding, actions);
            //calc.calculateR(actions, characters[0], characters[1], characters[2], characters[3], characters[4], characters[5], true);
            calc.calculateR(actions, characters, true);

            int decission = calc.getAction();
           

            changeCharacter(calc.getCharacter());
            //Debug.Log(calc.getCharacter());
            changeTargetedCharacter(calc.getTarget());
            //Debug.Log(character.getName() + " " + decission+" ");
            // Debug.Log(character);
            //Debug.Log(decission);
            //Debug.Log(targetedCharacter);
            // Debug.Log(!character.getMoved());
            if (character == null)
                decission = 4;

            if (decission == 0&&!character.getMoved())
            {
                
                pathFinding.getGrid().GetXY(character.getPosition(), out int xStart, out int yStart);
                // int xEnd = ((UnityEngine.Random.Range(0, 10))*10)+5;
                // int yEnd = ((UnityEngine.Random.Range(0, 10)) * 10)+5;
                calc.getPosition(out int xEnd, out int yEnd);
                //Debug.Log(xEnd + ", " + yEnd);
                //Debug.Log(xEnd + ", " + yEnd);
                xEnd = (xEnd * 10) + 5;
                yEnd = (yEnd * 10) + 5; 
                //pathFinding.getGrid().GetXY(new Vector3(xEnda, yEnda), out int xEnd, out int yEnd);
                // Debug.Log(xEnd + " " + yEnd);
                // Debug.Log("Good");
                // float dis = Math.Abs(Vector3.Distance(new Vector3(xEnd,yEnd), character.getPosition()));
                //dis /= 10;
                //Debug.Log(xEnd + ", " + yEnd);

                //List<PathNode> path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd, true);
                // Debug.Log("Great");
                //if (path.Count <= character.getmRange() + 1&&!containsCharacter(new Vector3(xEnd,yEnd)))
                // Debug.Log(dis+"<="+character.getmRange());
               // Debug.Log("Xa: " + xEnd + ", Ya: " + yEnd + ", X: " + xEnd + ", Y: " + yEnd);
                if ( !containsCharacter(new Vector3(xEnd, yEnd)))
                {
                    
                    //Debug.Log("good");
                    // character.SetPosition(pathFinding.getGrid().GetWorldPosition(xEnd, yEnd) + new Vector3(5, 5));
                    character.SetPosition(new Vector3(xEnd,yEnd));
                    character.Moved();
                   Debug.Log(character.getName()+" moved to "+character.getPosition());
                    //Debug.Log(character.getPosition()+"Pos");
                   // move = false;
                   // pathFinding.getGrid().resetSprites();
                   // useAnAction();
                }
                //calc.calculate(character, decission, new Vector3(xEnd, yEnd), targetedCharacter, characters);
               // Debug.Log(calc.getValue());
            }
            else if(decission == 1&&!character.getAttack())
            {
                //randomEnemy();
                Vector3 tarPos = targetedCharacter.getPosition();

                float dis = Vector3.Distance(targetedCharacter.getPosition(), character.getPosition());
                dis /= 10;
                // Vector3 dis = (character.getPosition() - targetedCharacter.getPosition()).magnitude;

                if (dis <= character.getaRange()&&!character.getAttack())
                {
                    // Random rand=new Random();
                    int num = UnityEngine.Random.Range(20, 30);
                    targetedCharacter.takeDammage(character.getAtk(), num);
                    character.attack();
                    attack = false;
                    pathFinding.getGrid().resetSprites();
                    //useAnAction();
                    // Debug.Log("HP: " + targetedCharacter.getHealth());
                    if (targetedCharacter.getHealth() <= 0)
                    {
                        characters.Remove(targetedCharacter);
                        characters.Add(null);
                        checkForVictory();
                      //  Debug.Log(character.getName()+" attacked "+ targetedCharacter.getName());
                    }
                }
            }
            else if(decission == 2 && !character.getAct())
            {
                //randomTarget(character);
                Vector3 tarPos = targetedCharacter.getPosition();

                float dis = Vector3.Distance(targetedCharacter.getPosition(), character.getPosition());
                dis /= 10;
                // Vector3 dis = (character.getPosition() - targetedCharacter.getPosition()).magnitude;

                if (dis <= character.getactRange())
                {
                    targetedCharacter = character.action(targetedCharacter);
                    pathFinding.getGrid().resetSprites();
                    character.usedAction();
                    act = false;
                    Debug.Log(character.getName() + " boosted " + targetedCharacter.getName());

                    //useAnAction();
                    // Debug.Log("Acted");

                }
            }

            useAnAction();
            calc.reset();
           // Debug.Log(actions);
            if(actions<=0)
                changeTurn();
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            if (containsCharacter(mouseWorldPosition))
            {
                updateStatDisplay(GetCharacter(mouseWorldPosition));
            }
        }

        

        if (Input.GetMouseButtonDown(0))
        {
            
           // Debug.Log("good");
          //  Console.Write("good");
            // Vector3 mouseWorldPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            //mouseWorldPosition.z = 0f;
            //  Debug.Log(UtilsClass.GetMouseWorldPosition());
            if (containsCharacter(mouseWorldPosition) && GetCharacter(mouseWorldPosition).getIsOwner()&&((target)||!move&&!act&&!attack))
            {
                changeCharacter(GetCharacter(mouseWorldPosition));
                pathFinding.getGrid().resetSprites();
            }
            else if (move &&character!=null&& !containsCharacter(mouseWorldPosition) && !character.getMoved()&&mouseWorldPosition.x>=0&&mouseWorldPosition.y>=0&&mouseWorldPosition.x<=110&&mouseWorldPosition.y<=110)
            {
                pathFinding.getGrid().GetXY(mouseWorldPosition, out int xEnd, out int yEnd);
                pathFinding.getGrid().GetXY(character.getPosition(), out int xStart, out int yStart);

                // List<PathNode> path = pathFinding.FindPath(0, 0, xEnd, yEnd); 
                List<PathNode> path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd, true);
                //PathNode end= new PathNode(xEnd, yEnd);
                //List<PathNode> path = pathFinding.calculatePath(end);
                //int pathSize= pathFinding.FindPath(xStart, yStart, xEnd, yEnd);
                //characterPathfinding.setTargetPosition(mouseWorldPosition);
                /* if (path != null)
                 {
                     for (int i = 1; i < path.Count - 1; i++)
                     {
                                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green);
                       // characterPathfinding.movementHandler();
                       //pathFinding.getGrid().GetXY(mouseWorldPosition, out x, out y);
                       //characterPathfinding.transform.position = pathFinding.getGrid().GetWorldPosition((x/5)*5,(y/5)*5);
                     }
                 }*/
                //String a=mouseWorldPosition.ToString();
                // Debug.Log(a);!!!!!!!!!!!!!!!!!!
                //   characterPathfinding.setTargetPosition(mouseWorldPosition);!!!!!!!!!!
                // pathFinding.getGrid().GetXY(mouseWorldPosition, out x, out y);
                /*pathFinding.getGrid().GetXY(mouseWorldPosition, out int unitX, out int unitY);

                   for (int xV=unitX-range; xV<=unitX+range; xV++)
                   {
                       for(int yV=unitY-range; yV<=unitY+range;yV++)
                       {
                           if (path.Count <= range)
                           {
                               characterPathfinding.transform.position = pathFinding.getGrid().GetWorldPosition(xEnd, yEnd) + new Vector3(5, 5);
                           }
                           else
                           {
                               Debug.Log("Out of Range");
                           }
                       }
                   }*/

                if (path.Count <= character.getmRange()+1&&pathFinding.checkIsWalkable(path))
                {
                    character.SetPosition(pathFinding.getGrid().GetWorldPosition(xEnd, yEnd) + new Vector3(5, 5));
                    character.Moved();
                    move = false;
                    pathFinding.getGrid().resetSprites();
                    useAnAction();
                }
                else
                {
                    //Debug.Log("Out of Range ");
                }
            }
            else if (attack &&character!=null&& containsCharacter(mouseWorldPosition)&&!GetCharacter(mouseWorldPosition).getIsOwner() && !character.getAttack())
            {
               changeTargetedCharacter(GetCharacter(mouseWorldPosition));

            /*    pathFinding.getGrid().GetXY(targetedCharacter.getPosition(), out int xTar, out int yTar);
                pathFinding.getGrid().GetXY(character.getPosition(), out int xStart, out int yStart);
                List<PathNode> path = pathFinding.FindPath(xStart, yStart, xTar, yTar);*/

                Vector3 tarPos=targetedCharacter.getPosition();

                 float dis=Vector3.Distance(targetedCharacter.getPosition(),character.getPosition());
                dis /= 10;
               // Vector3 dis = (character.getPosition() - targetedCharacter.getPosition()).magnitude;

                if (dis<=character.getaRange())
                {
                   // Random rand=new Random();
                    int num = UnityEngine.Random.Range(20, 30);
                    targetedCharacter.takeDammage(character.getAtk(), num);
                    character.attack();
                    attack = false;
                    pathFinding.getGrid().resetSprites();
                    useAnAction() ;
                   // Debug.Log("HP: " + targetedCharacter.getHealth());
                    if(targetedCharacter.getHealth()<=0)
                    {
                        characters.Remove(targetedCharacter);
                        checkForVictory();
                    }
                }
                else
                {
                   // Debug.Log("Out of Range: "+dis);
                }

            }
            else if (act &&character!=null&& containsCharacter(mouseWorldPosition)&&!character.getAct())
            {
                changeTargetedCharacter(GetCharacter(mouseWorldPosition));
                if (targetedCharacter.getIsOwner() == character.getActType()) 
                {
                    Vector3 tarPos = targetedCharacter.getPosition();

                    float dis = Vector3.Distance(targetedCharacter.getPosition(), character.getPosition());
                    dis /= 10;
                    // Vector3 dis = (character.getPosition() - targetedCharacter.getPosition()).magnitude;

                    if (dis <= character.getactRange())
                    {
                        targetedCharacter=character.action(targetedCharacter);
                        pathFinding.getGrid().resetSprites();
                        character.usedAction();
                        act = false;

                        useAnAction();
                      //  Debug.Log("HP: " + targetedCharacter.getHealth());
                        
                    }
                    else
                    {
                        //Debug.Log("Out of Range: " + dis);
                    }
                }
            }
            // characterPathfinding.transform.position = pathFinding.getGrid().GetWorldPosition(x,y)+new Vector3(5,5);


            //characterPathfinding.transform.position = mouseWorldPosition;
            /*while(characterPathfinding.transform.position!=mouseWorldPosition)
            {
                characterPathfinding.movementHandler();
            }*/
            //  characterPathfinding.movementHandler();
            //  characterPathfinding.changePosition(mouseWorldPosition);
            /*  for (int i = 1; i < path.Count - 1; i++)
              {
                  //Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green);
                   characterPathfinding.movementHandler();
              }*/
            //  Vector3 pos=pathFinding.getGrid().GetWorldPosition((int)mouseWorldPosition.x, (int)mouseWorldPosition.y);
            //  characterPathfinding.transform.position = pos;
            if (containsCharacter(mouseWorldPosition))
            {
                updateStatDisplay(GetCharacter(mouseWorldPosition));
            }
            /*act = false;
            move=false;
            attack=false;*/
            if (actions==0)
            {
                changeTurn();
            }
        }
    }
}
