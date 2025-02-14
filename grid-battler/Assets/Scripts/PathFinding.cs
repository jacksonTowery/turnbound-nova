using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    //private const int MOVE_DIAGONAL_COST = 14;
    private const int MOVE_DIAGONAL_COST = 100;

    public static PathFinding Instance { get; private set; }

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private int width;
    private int height;
    private Mesh mesh;
    private Sprite defaultSprite;
    private Sprite pathSprite;
    public PathFinding(int width, int height, Sprite pathSprite,Sprite defaultSprite)
    {
        Instance = this;
        this.width = width;
        this.height = height;
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y),defaultSprite);
        mesh = new Mesh();
        this.pathSprite = pathSprite;
        this.defaultSprite = defaultSprite;
        // Instance=this;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY, bool useIsWalkable)
    {
        PathNode startNode = grid.getValue(startX, startY);
        PathNode endNode = grid.getValue(endX, endY);
        openList = new List<PathNode> { startNode };
        // Debug.Log("OpenList count 0: " + openList.Count);
        // openList = new List<PathNode> ();
        closedList = new List<PathNode>();
        //  int attampt = 0;

        for (int x = 0; x < grid.getWidth(); x++)
        {
            for (int y = 0; y < grid.getHeight(); y++)
            {
                PathNode pathNode = grid.getValue(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.calculateFcost();
                //Debug.Log(pathNode.fCost);
                // pathNode.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = calculateDistance(startNode, endNode);
        startNode.calculateFcost();
        //  Debug.Log("OpenList count: " + openList.Count);
        while (openList.Count > 0)
        {
            //  attampt++;
            //Debug.Log("Attempt: " + attampt);
            // Debug.Log("OpenList count "+attampt+": " + openList.Count);
            PathNode currentNode = getLowestFcostNode(openList);

            if (currentNode == endNode)
            {
                // Debug.Log("done");
                //Problem
                return calculatePath(endNode);
            }


            openList.Remove(currentNode);

            closedList.Add(currentNode);

            foreach (PathNode neighborNode in getNeighbourList(currentNode)) {
                if (closedList.Contains(neighborNode)) continue;
                /*if (useIsWalkable)
                {
                    if (!neighborNode.getIsWalkable())
                    {
                        closedList.Add(neighborNode);
                        continue;
                    }
                }*/

                int tentativeGcost = currentNode.gCost + calculateDistance(currentNode, neighborNode);


                if (tentativeGcost < neighborNode.gCost) //
                {

                    neighborNode.cameFromNode = currentNode;

                    neighborNode.gCost = tentativeGcost;

                    neighborNode.hCost = calculateDistance(neighborNode, endNode);

                    neighborNode.calculateFcost();

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
        return null;
    }

    private List<PathNode> getNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            //Left
            neighbourList.Add(getNode(currentNode.x - 1, currentNode.y));
            //LeftDown
            if (currentNode.y - 1 >= 0)
            {
                neighbourList.Add(getNode(currentNode.x - 1, currentNode.y - 1));

            }
            //LeftUp
            if (currentNode.y + 1 < grid.getHeight())
            {
                neighbourList.Add(getNode(currentNode.x - 1, currentNode.y + 1));

            }
        }

        if (currentNode.x + 1 < grid.getWidth())
        {
            //Right
            neighbourList.Add(getNode(currentNode.x + 1, currentNode.y));
            //RighttDown
            if (currentNode.y - 1 >= 0)
            {
                neighbourList.Add(getNode(currentNode.x + 1, currentNode.y - 1));

            }
            //RightUp
            if (currentNode.y + 1 < grid.getHeight())
            {
                neighbourList.Add(getNode(currentNode.x + 1, currentNode.y + 1));

            }
        }

        if (currentNode.y - 1 >= 0)
        {
            //Down
            neighbourList.Add(getNode(currentNode.x, currentNode.y - 1));

        }

        if (currentNode.y + 1 < grid.getHeight())
        {
            //Up
            neighbourList.Add(getNode(currentNode.x, currentNode.y + 1));

        }
        return neighbourList;
    }

    private PathNode getNode(int x, int y)
    {
        return grid.getValue(x, y);
    }
    //Problem
    private List<PathNode> calculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        //int attempt = 0;

        while (currentNode.cameFromNode != null)
        {
           // attempt++;
          //  Debug.Log("Attempt "+attempt+" count:"+ path.Count);
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        // Debug.Log("good here "+ path);
        // return path;
       // grid=new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y), null);
       grid.resetGrid((Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        return path;
    }

    private int calculateDistance(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode getLowestFcostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFcostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFcostNode.fCost)
            {
                lowestFcostNode = pathNodeList[i];
            }
        }
        return lowestFcostNode;
    }

    public Grid<PathNode> getGrid()
    {
        //Debug.Log("Good"+grid);
        return grid;
    }
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition, bool walkable)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);
        // Debug.Log("good here");*/
        List<PathNode> path = new List<PathNode>();
        path = FindPath(startX, startY, endX, endY, walkable);
        // Debug.Log("goody");
        if (path == null)
        {
            // Debug.Log(startX+" "+startY+" "+endX+" "+endY);
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.getCellSize() + Vector3.one * grid.getCellSize() * .5f);
            }
            return vectorPath;
        }

    }
    public bool checkIsWalkable(List<PathNode> pathNodes)
    {
        foreach(PathNode pathNode in pathNodes)
        {
            if (!pathNode.isWalkable)
                return false;
        }
        return true;
    }
    public void setPathSprite(int maxMove, int xPos, int yPos, bool walkable)
    {
        List<PathNode> path;
        for (int x = xPos-maxMove; x <= xPos+maxMove; x++)
        {
            for(int y = yPos-maxMove; y <= yPos+maxMove; y++)
            {
                if (grid.hasValue(x, y))
                {
                    if (grid.getValue(x, y).getIsWalkable())
                    {
                        path = FindPath(xPos, yPos, x, y, walkable);
                        if (path.Count <= maxMove&&checkIsWalkable(path))
                        {
                            grid.setSprite(pathSprite, x, y);
                           
                        }
                    }
                }
            }
        }
    }
}

