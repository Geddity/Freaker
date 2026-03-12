using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;
    CharacterAnimator characterAnimator;

    public Transform targetCamera;

    List<Vector3> pathWorldPosition;

    public bool IS_MOVING
    {
        get
        {
            if(pathWorldPosition == null) { return false; }
            return pathWorldPosition.Count > 0;
        }
    }

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float minSpeed = 3f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float acceleration = 2f;

    public AnimationHandle animationHandle;

    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    public void Move(List<PathNode> path)
    {
        if(IS_MOVING)
        {
            SkipAnimation();
        }

        pathWorldPosition = gridObject.targetGrid.ConvertPathNodeToWorldPosition(path);

        gridObject.targetGrid.RemoveObject(gridObject.positionOnGrid, gridObject);

        gridObject.positionOnGrid.x = path[path.Count - 1].pos_x;
        gridObject.positionOnGrid.y = path[path.Count - 1].pos_y;

        gridObject.targetGrid.PlaceObject(gridObject.positionOnGrid, gridObject);

        //RotateCharacter(transform.position, pathWorldPosition[0]);

        characterAnimator.StartMoving();
    }

    public void RotateCharacter(Vector3 originPosition, Vector3 destinationPosition)
    {
        Vector3 direction = (destinationPosition - originPosition).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        if (pathWorldPosition == null) { return; }
        if (pathWorldPosition.Count == 0) { return; }

        //
        
        if (pathWorldPosition.Count <= 2)
        {
            moveSpeed -= acceleration * Time.deltaTime;
            if (moveSpeed < minSpeed)
            {
                moveSpeed = minSpeed;
            }
        }
        else
        {  
            moveSpeed += acceleration * Time.deltaTime;
            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
        //

        

        transform.position = Vector3.MoveTowards(transform.position, pathWorldPosition[0], moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathWorldPosition[0]) < 0.05f)
        {
            pathWorldPosition.RemoveAt(0);
            if(pathWorldPosition.Count == 0) { characterAnimator.StopMoving(); moveSpeed = minSpeed; }

            //povorot k target
            else
            {
                //RotateCharacter(transform.position, pathWorldPosition[0]);
            }
        } 
        
        for(int i = 0; i < pathWorldPosition.Count; i++)
        {
            if (transform.position.x < pathWorldPosition[i].x)
            {
                animationHandle.SetFlip(-1);
            }
            else
            {
                animationHandle.SetFlip(1);
            }
        } 
    }

    public void SkipAnimation()
    {
        if (pathWorldPosition.Count < 2) { return; }
        transform.position = pathWorldPosition[pathWorldPosition.Count - 1];
        Vector3 originPosition = pathWorldPosition[pathWorldPosition.Count - 2];
        Vector3 destinationPosition = pathWorldPosition[pathWorldPosition.Count - 1];
        RotateCharacter(originPosition, destinationPosition);
        pathWorldPosition.Clear();
        characterAnimator.StopMoving();
    }

    private void LateUpdate()
    {
        if (targetCamera != null)
        {
            Vector3 direction = (targetCamera.transform.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
