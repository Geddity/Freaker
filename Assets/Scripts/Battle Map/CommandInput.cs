using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    CommandManager commandManager;
    MouseInput mouseInput;
    MoveCharacter moveCharacter;
    CharacterAttack characterAttack;
    SelectCharacter selectedCharacter;
    ClearUtility clearUtility;

    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
        selectedCharacter = GetComponent<SelectCharacter>();
        clearUtility = GetComponent<ClearUtility>();
    }

    [SerializeField] CommandType currentCommand;
    bool isInputCommand;

    public void SetCommandType(CommandType commandtype)
    {
        currentCommand = commandtype;
    }

    public void InitCommand()
    {
        isInputCommand = true;
        switch (currentCommand)
        {
            case CommandType.MoveTo:
                HighlightWalkableTerrain();
                break;
            case CommandType.Attack:
                characterAttack.CalculateAttackArea(
                    selectedCharacter.selected.GetComponent<GridObject>().positionOnGrid,
                    selectedCharacter.selected.GetIntValue(CharacterStatsEnum.AttackRange)
                    );
                break;
        }
    }

    private void Start()
    {
        //HighlightWalkableTerrain();
        //
    }

    private void Update()
    {
        if ( isInputCommand == false ) { return; }
        switch (currentCommand)
        {
            case CommandType.MoveTo:
                MoveCommandInput();
                break;
            case CommandType.Attack:
                AttackCommandInput();
                break;
        }
    }

    private void AttackCommandInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(characterAttack.Check(mouseInput.positionOnGrid) == true)
            {
                GridObject gridObject = characterAttack.GetAttackTarget(mouseInput.positionOnGrid);
                if(gridObject == null) { return; }
                commandManager.AddAttackCommand(selectedCharacter.selected, mouseInput.positionOnGrid, gridObject);
                StopCommandInput();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightAttack();
        }
    }

    private void MoveCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(moveCharacter.CheckOccupied(mouseInput.positionOnGrid) == true) { return; }
            List<PathNode> path = moveCharacter.GetPath(mouseInput.positionOnGrid);
            if (path == null) { return; }
            if (path.Count == 0) { return; }
            commandManager.AddMoveCommand(selectedCharacter.selected, mouseInput.positionOnGrid, path);
            StopCommandInput();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightMove();
            clearUtility.ClearPathfinding();
        }
    }

    private void StopCommandInput()
    {
        selectedCharacter.Deselect();
        selectedCharacter.enabled = true;
        isInputCommand = false;
    }

    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectedCharacter.selected);
    }
}
