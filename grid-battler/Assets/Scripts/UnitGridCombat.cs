using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGridCombat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveTo(Vector3 targetPosition, Action onReachedPosition)
    {
        transform.position = targetPosition;
        onReachedPosition();
    }

    public Vector3 getPosition() 
    { return transform.position; }

}
