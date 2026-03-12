using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableUnitNode : MonoBehaviour
{
    public CharacterObject characterObject;
    UnitPlacementManager manager;
    public GridObject gridObject;

    private void Awake()
    {
        manager = FindObjectOfType<UnitPlacementManager>();
        gridObject = GetComponent<GridObject>();
    }

    void Start()
    {
        manager.AddMe(this);
    }

}
