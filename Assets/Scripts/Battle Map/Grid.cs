using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    public int width = 40;
    public int length = 40;
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask terrainLayer;

    private void Awake()
    {
        GenerateGrid(); 
    }

    private void GenerateGrid()
    {
        grid = new Node[length, width];

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                grid[x, y] = new Node();
            }
        }

        CalculateElevation();
        CheckPassableTerrain();
    }

    private void CalculateElevation()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Ray ray = new Ray(GetWorldPosition(x,y) + Vector3.up * 100f, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
                {
                    grid[x,y].elevation = hit.point.y;
                }
            }
        }
    }

    private void CheckPassableTerrain()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPosition = GetWorldPosition(x, y);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * cellSize, Quaternion.identity, obstacleLayer);
                grid[x, y].passable = passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(grid == null) 
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y);
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
        else
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y, true);
                    Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
        
    }

    public Vector3 GetWorldPosition(int x , int y, bool elevation = false)
    {
        return new Vector3(x * cellSize, elevation == true ? grid[x,y].elevation : 0f, y * cellSize);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition.x += cellSize / 2;
        worldPosition.z += cellSize / 2;
        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x/cellSize), (int)(worldPosition.z/cellSize));
        return positionOnGrid;
    }

    internal void RemoveObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid) == true)
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = null;
        }
        else
        {
            Debug.Log("Object outside the boundries!");
        }
    }

    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if(CheckBoundry(positionOnGrid) == true)
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
        }
        else
        {
            Debug.Log("Object outside the boundries!");
        }
    }

    public bool CheckBoundry(Vector2Int positionOnGrid)
    {
        if(positionOnGrid.x < 0 || positionOnGrid.x >= length)
        {
            return false;
        }
        if (positionOnGrid.y < 0 || positionOnGrid.y >= width)
        {
            return false;
        }

        return true;
    }

    internal bool CheckBoundry(int posX, int posY)
    {
        if (posX < 0 || posX >= length)
        {
            return false;
        }
        if (posY < 0 || posY >= width)
        {
            return false;
        }

        return true;
    }
    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return grid[pos_x, pos_y].passable;
    }

    internal GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        if (CheckBoundry(gridPosition) == true)
        {
            GridObject gridObject = grid[gridPosition.x, gridPosition.y].gridObject;
            return gridObject;
        }
        return null;
    }

    public List<Vector3> ConvertPathNodeToWorldPosition(List<PathNode> path)
    {
        List<Vector3> worldPosition = new List<Vector3>();

        for(int i = 0; i < path.Count; i++)
        {
            worldPosition.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
        }

        return worldPosition;
    }

    internal bool CheckOccupied(Vector2Int positionOnGrid)
    {
        return GetPlacedObject(positionOnGrid) != null;
    }

    public bool CheckElevation(int from_pos_x, int from_pos_y, int to_pos_x, int to_pos_y, float characterClimb = 1.5f)
    {
        float from_elevation = grid[from_pos_x, from_pos_y].elevation;
        float to_elevation = grid[to_pos_x, to_pos_y].elevation;

        float elevation_difference = to_elevation - from_elevation;

        elevation_difference = Math.Abs(elevation_difference);

        return characterClimb > elevation_difference;
    }
}
