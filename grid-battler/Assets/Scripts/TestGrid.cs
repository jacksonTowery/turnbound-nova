using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestGrid : MonoBehaviour
{
    // Start is called before the first frame update
    private PathFinding pathFinding;
    //[SerializeField] private CharacterPathfinding characterPathfinding;
    [SerializeField] private Character characterA;
    [SerializeField] private Character characterB;
    [SerializeField] private Character characterC;
    [SerializeField] private Character characterD;
    [SerializeField] private Character characterE;
    [SerializeField] private Character characterF;
    [SerializeField] private Text turnIdentifier;
    [SerializeField] private Text StatDisplay;
    [SerializeField] private Text actionCount;
    [SerializeField] private UnityEngine.UI.Image StatDisplaySprite;
    [SerializeField] private UnityEngine.UI.Image CurrentSprite;
    [SerializeField] private Text currentDisplay;
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
    // [SerializeField] public Camera camera;
   // [SerializeField] public Component.camera camera;
    void Start()
    {
        Sprite spriteP = Resources.Load<Sprite>("Sprites/Selectable_Tile");
        Sprite spriteD = Resources.Load<Sprite>("Sprites/Tile");
        Debug.Log(spriteD);
        //  Grid<bool> grid = new Grid<bool>(11, 11, 10f, new Vector3(20,0), ()=> new bool());
        pathFinding = new PathFinding(11, 11, spriteP,spriteD);
         characters = new List<Character>();
         characters.Add(characterA);
         characters.Add(characterB);
         characters.Add(characterC);
         characters.Add(characterD);
         characters.Add(characterE);
         characters.Add(characterF);
        //changeCharacter(characterA);
        //changeTargetedCharacter(characterE);
        
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
            pathFinding.getGrid().GetXY(characterPos.transform.position, out int xChar, out int yChar);
            characterPosition = new Vector3(xChar, yChar);
            //Debug.Log("Clicked: " + gridPosition + ", Character: " + characterPos.transform.position);
            if (characterPosition==gridPosition)
            {
                return true;
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
        turnIdentifier.text = "" + ((turn % 2)+1);
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
            Debug.Log("Player " + ((turn % 2)+1) + " Wins");
        }
    }
    public void updateStatDisplay(Character stat)
    {
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
            CurrentSprite.sprite = null;
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
        attack = true;
        move = false;
        act = false;
        target=false;
        updateCurrentDisplay(character);
        pathFinding.getGrid().resetSprites();
        pathFinding.getGrid().GetXY(character.getPosition(), out int x, out int y);
        pathFinding.setPathSprite(character.getaRange() + 1, x, y, false);
    }
    public void moveTrue() 
    {
        attack=false;
        move = true;
        act = false;
        target=false;
        updateCurrentDisplay(character);
        pathFinding.getGrid().resetSprites();
        pathFinding.getGrid().GetXY(character.getPosition(),out int x,out int y);
        pathFinding.setPathSprite(character.getmRange()+1, x, y, true);
        // Debug.Log("move");
    }
    public void actTrue() 
    {
        attack = false;
        move = false;
        act = true;
        target=false ;
        updateCurrentDisplay(character);
        pathFinding.getGrid().resetSprites();
        pathFinding.getGrid().GetXY(character.getPosition(), out int x, out int y);
        pathFinding.setPathSprite(character.getactRange()+1, x, y, false);
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
            if (containsCharacter(mouseWorldPosition) && GetCharacter(mouseWorldPosition).getIsOwner()&&target)
            {
                changeCharacter(GetCharacter(mouseWorldPosition));
                pathFinding.getGrid().resetSprites();
            }
            else if (move && !containsCharacter(mouseWorldPosition) && !character.getMoved())
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
                    pathFinding.getGrid().resetSprites();
                    useAnAction();
                }
                else
                {
                    Debug.Log("Out of Range ");
                }
            }
            else if (attack && containsCharacter(mouseWorldPosition)&&!GetCharacter(mouseWorldPosition).getIsOwner() && !character.getAttack())
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
                    targetedCharacter.takeDammage(character.getAtk());
                    character.attack();
                    pathFinding.getGrid().resetSprites();
                    useAnAction() ;
                    Debug.Log("HP: " + targetedCharacter.getHealth());
                    if(targetedCharacter.getHealth()<=0)
                    {
                        characters.Remove(targetedCharacter);
                        checkForVictory();
                    }
                }
                else
                {
                    Debug.Log("Out of Range: "+dis);
                }

            }
            else if (act && containsCharacter(mouseWorldPosition)&&!character.getAct())
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
                        useAnAction();
                        Debug.Log("HP: " + targetedCharacter.getHealth());
                        
                    }
                    else
                    {
                        Debug.Log("Out of Range: " + dis);
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
            if (actions==0)
            {
                changeTurn();
            }
        }
    }
}
