using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMovement : MonoBehaviour
{

 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            Debug.Log(UtilsClass.GetMouseWorldPosition());
   //         GetComponent<MovePositionDirect>().SetMovementPosition(UtilsClass.GetMouseWorldPosition());
    
        }
    }
}
