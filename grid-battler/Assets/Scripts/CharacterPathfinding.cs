using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class CharacterPathfinding : MonoBehaviour
{
    private float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    

    // Start is called before the first frame update

   /* public void Updat()
    {
        movementHandler();
    }*/

    public Vector3 getPosition()
    {
        return transform.position;
    }
  /*  public void setTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        //Debug.Log("good B");
        pathVectorList = PathFinding.Instance.FindPath(getPosition(), targetPosition);
        //Debug.Log("Help " +pathVectorList);
      //  Debug.Log(PathFinding.Instance.FindPath(getPosition(), targetPosition));
        if (targetPosition != null &&pathVectorList!=null&& pathVectorList.Count>1)
        {
          // Debug.Log("isGood");
            pathVectorList.RemoveAt(0);
        }
        /* if (pathVectorList == null)
         {
             //Debug.Log("Vector List is null");
         }
        //movementHandler();
    }*/

    public void movementHandler()
    {
        if(pathVectorList!=null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if(Vector3.Distance(transform.position, targetPosition) >1f)
            {
                Vector3 moveDir=(targetPosition-transform.position).normalized;

               // GetComponent<MoveVelocity>().SetVelocity(moveDir);
              // GetComponent<MovePositionDirect>().SetMovementPosition(moveDir);
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //setvelocity
                transform.position=transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();

                    //   GetComponent<MoveVelocity>().SetVelocity(Vector3.zero);
                 //  GetComponent<MovePositionDirect>().SetMovementPosition(Vector3.zero);
                }
                else
                {
                    // GetComponent<MoveTransformVelocity>().SetVelocity(Vector3.zero);
                  //  GetComponent<MovePositionDirect>().SetMovementPosition(Vector3.zero); 
                }
            }
                
        }
    }
    private void StopMoving()
    {
        pathVectorList = null;
    }

   /* public void changePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }*/

}
