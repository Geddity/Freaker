using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Grid grid;
    [SerializeField] GameObject highlightPoint;
    List<GameObject> highlightPointGO;
    [SerializeField] GameObject container;


    void Awake()
    {
        grid = GetComponentInParent<Grid>();
        highlightPointGO = new List<GameObject>();
        //Highlight(testTargetPosition);
    }

    private GameObject CreatePointHighlightObject()
    {
        GameObject go = Instantiate(highlightPoint);
        highlightPointGO.Add(go);
        go.transform.SetParent(container.transform);
        return go;
    }

    public void Highlight(List<Vector2Int> positions)
    {
        for(int i  = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetHighlightPointGO(i));
        }
    }

    public void Highlight(List<PathNode> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].pos_x, positions[i].pos_y, GetHighlightPointGO(i));
        }
    }

    private GameObject GetHighlightPointGO(int i)
    {
        if(i < highlightPointGO.Count)
        {
            return highlightPointGO[i];
        }

        GameObject newHighlightObject = CreatePointHighlightObject();
        return newHighlightObject;
    }

    public void Highlight(int posX, int posY, GameObject highlightObject)
    {
        highlightObject.SetActive(true);
        Vector3 position = grid.GetWorldPosition(posX, posY, true);
        position += Vector3.up * 0.1f;
        highlightObject.transform.position = position;
    }

    internal void Hide()
    {
        for(int i = 0; i < highlightPointGO.Count; i++)
        {
            highlightPointGO[i].SetActive(false);
        }
    }
}
