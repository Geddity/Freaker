using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    Grid targetGrid;

    Pathfinding pathfinding;

    GridHighlight gridHighlight;

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        targetGrid = stageManager.stageGrid;
        gridHighlight = stageManager.moveHighlight;
        pathfinding = targetGrid.GetComponent<Pathfinding>();
    }

    public void CheckWalkableTerrain(CharacterObject targetCharacter)
    {
        GridObject gridObject = targetCharacter.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        pathfinding.Clear();
        pathfinding.CalculateWalkableNodes(
            gridObject.positionOnGrid.x,
            gridObject.positionOnGrid.y,
            targetCharacter.GetFloatValue(CharacterStatsEnum.MoveDistance),
            ref walkableNodes
            );

        gridHighlight.Hide();
        gridHighlight.Highlight(walkableNodes);
    }

    public List<PathNode> GetPath(Vector2Int from)
    {
        List<PathNode> path = pathfinding.TraceBackPath(from.x, from.y);

        if (path == null) { return null; }
        if (path.Count == 0) { return null; }

        path.Reverse();

        return path;
    }

    private void Update()
    {
        
    }

    public bool CheckOccupied(Vector2Int positionOnGrid)
    {
        return targetGrid.CheckOccupied(positionOnGrid);
    }
}
