using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] GameObject characterModel;

    List<GameObject> characterObjects = new List<GameObject>();

    //[SerializeField] PartyData partyData;

    List<PlaceableUnitNode> nodes;
    MouseInput mouseInput;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        characterObjects = new List<GameObject>();

        //for(int i = 0; i < partyData.charactersInParty.Count; i++)
        //{
        //    InitCharacter(partyData.charactersInParty[i]);
        //}
    }

    private void InitCharacter(CharacterData characterData)
    {
        GameObject newCharacterGO = Instantiate(characterPrefab);
        GameObject newCharacterModel = Instantiate(characterModel);

        newCharacterModel.transform.parent = newCharacterGO.transform;

        //newCharacterGO.GetComponent<CharacterObject>().SetCharacterData(characterData);

        characterObjects.Add(newCharacterGO);
    }

    public void AddMe(PlaceableUnitNode placeableUnitNode)
    {
        if(nodes == null) { nodes = new List<PlaceableUnitNode>(); }

        nodes.Add(placeableUnitNode);
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlaceableUnitNode placeNode = nodes.Find(x => x.gridObject.positionOnGrid == mouseInput.positionOnGrid);
            if (placeNode != null )
            {
                if(placeNode.characterObject == null)
                {
                    PlaceCharacter(placeNode, characterObjects[0]);
                }
            }
        }
    }

    private void PlaceCharacter(PlaceableUnitNode placeNode, GameObject characterGO)
    {
        characterGO.transform.position = placeNode.transform.position;
        placeNode.characterObject = characterGO.GetComponent<CharacterObject>();

        characterObjects.Remove(characterGO);
    }
}
