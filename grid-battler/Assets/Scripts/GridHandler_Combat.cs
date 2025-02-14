using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridHandler_Combat : MonoBehaviour
{
    public static GridHandler_Combat instance {  get; private set; }

    [SerializeField] Transform cinemachineFollowTransfornm;

    private Grid<EmptyGridObject> grid;
    public PathFinding gridPathFinding;

    private void Awake()
    {
        instance = this;
        int mapWidth = 11;
        int mapHeight = 11;
        float cellsize = 10f;
        Vector3 origin = new Vector3(0, 0);
        //bool t = true;
        Sprite sprite = null;
        grid = new Grid<EmptyGridObject>( mapWidth, mapHeight, cellsize, origin, (Grid<EmptyGridObject> g, int x, int y) => new EmptyGridObject(), sprite);
       // gridPathFinding = new PathFinding(origin + new Vector3(1, 1) * cellsize * .5f, new Vector3(mapWidth, mapHeight));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
