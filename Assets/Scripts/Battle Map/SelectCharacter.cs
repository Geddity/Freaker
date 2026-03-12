using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    MouseInput mouseInput;
    CommandMenu commandMenu;
    GameMenu gameMenu;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
        commandMenu = GetComponent<CommandMenu>();
        gameMenu = GetComponent<GameMenu>();
    }

    public CharacterObject selected;
    bool isSelected;
    GridObject hoverOverGridObject;
    public CharacterObject hoverOverCharacter;
    Vector2Int positionOnGrid = new Vector2Int(-1,-1);
    Grid targetGrid;

    private void Start()
    {
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

    private void Update()
    {
        if (positionOnGrid != mouseInput.positionOnGrid)
        { 
            HoverOverObject();
        }

        SelectInput();
        DeselectInput();
    }

    private void LateUpdate()
    {
        if(selected != null)
        {
            if(isSelected == false) 
            {
                selected = null;
            }
        }
    }

    private void HoverOverObject()
    {
        positionOnGrid = mouseInput.positionOnGrid;
        hoverOverGridObject = targetGrid.GetPlacedObject(positionOnGrid);
        if (hoverOverGridObject != null)
        {
            hoverOverCharacter = hoverOverGridObject.GetComponent<CharacterObject>();
        }
        else
        {
            hoverOverCharacter = null;
        }
    }

    private void DeselectInput()
    {
        if (Input.GetMouseButtonUp(1))
        {
            selected = null;
            UpdatePanel();
        }
    }

    private void SelectInput()
    {
        HoverOverObject();
        if(selected != null) { return; }
        if(gameMenu.panel.activeInHierarchy == true) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverCharacter != null && selected == null)
            {
                selected = hoverOverCharacter;
                isSelected = true;
            }
            UpdatePanel();
        }
    }

    private void UpdatePanel()
    {
        if(selected != null)
        {
            commandMenu.OpenPanel(selected.GetComponent<CharacterTurn>());
        }
        else
        {
            commandMenu.ClosePanel();
        }
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
