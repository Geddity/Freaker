using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] GridHighlight highlight;

    List<Vector2Int> attackPosition;

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        targetGrid = stageManager.stageGrid;
        highlight = stageManager.attackHighlight;
    }

    public void CalculateAttackArea(Vector2Int characterPositionOnGrid, int attackRange, bool selfTargetable = false)
    {
        if(attackPosition == null)
        {
            attackPosition = new List<Vector2Int>();
        }
        else
        {
            attackPosition.Clear();
        }

        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int y = -attackRange; y <= attackRange; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) > attackRange) { continue; }

                if (selfTargetable == false)
                {
                    if (x == 0 && y == 0) { continue; }
                }
                if (targetGrid.CheckBoundry(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y) == true)
                {
                    attackPosition.Add(
                        new Vector2Int(
                            characterPositionOnGrid.x + x,
                            characterPositionOnGrid.y + y
                            )
                        );
                }
            }
        }

        highlight.Highlight(attackPosition);
    }

    internal bool Check(Vector2Int positionOnGrid)
    {
        return attackPosition.Contains(positionOnGrid);
    }

    internal GridObject GetAttackTarget(Vector2Int positionOnGrid)
    {
        GridObject target = targetGrid.GetPlacedObject(positionOnGrid);
        return target;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
        //    {
        //        Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);

        //        if(attackPosition.Contains(gridPosition))
        //        {
        //            GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
        //            if(gridObject == null) { return; }
        //            selectedCharacter.GetComponent<Attack>().AttackPosition(gridObject);
        //        }
        //    }
        //}
    }
}
