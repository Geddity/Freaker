using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum CommandType
{
    MoveTo,
    Attack
}

public class Command
{
    public CharacterObject character;
    public Vector2Int selectedGrid;
    public CommandType commandType;

    public Command(CharacterObject character, Vector2Int selectedGrid, CommandType commandType)
    {
        this.character = character;
        this.selectedGrid = selectedGrid;
        this.commandType = commandType;
    }

    public List<PathNode> path;
    public GridObject target;
}

public class CommandManager : MonoBehaviour
{
    VictoryManager victoryManager;
    ClearUtility clearUtility;

    private void Awake()
    {
        clearUtility = GetComponent<ClearUtility>();
        victoryManager = GetComponent<VictoryManager>();
    }

    Command currentCommand;

    CommandInput commandInput;

    private void Start()
    {
        commandInput = GetComponent<CommandInput>();
    }

    private void Update()
    {
        if(currentCommand != null) 
        {
            ExecuteCommand();
        }
    }

    public void ExecuteCommand()
    {
        switch (currentCommand.commandType)
        {
            case CommandType.MoveTo:
                MovementCommandExecute();
                break;
            case CommandType.Attack:
                AttackCommandExecute();
                break;
        }
    }

    private void AttackCommandExecute()
    {
        CharacterObject receiver = currentCommand.character;
        receiver.GetComponent<Attack>().AttackGridObject(currentCommand.target);
        receiver.GetComponent<CharacterTurn>().canAct = false;
        victoryManager.CheckPlayerVictory();

        currentCommand = null;
        clearUtility.ClearGridHighlightAttack();
    }

    private void MovementCommandExecute()
    {
        CharacterObject receiver = currentCommand.character;
        receiver.GetComponent<Movement>().Move(currentCommand.path);
        receiver.GetComponent<CharacterTurn>().canWalk = false;

        currentCommand = null;
        clearUtility.ClearPathfinding();
        clearUtility.ClearGridHighlightMove();
    }

    public void AddMoveCommand(CharacterObject character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.MoveTo);
        currentCommand.path = path;
    }

    internal void AddAttackCommand(CharacterObject attacker, Vector2Int selectGrid, GridObject target)
    {
        currentCommand = new Command(attacker, selectGrid, CommandType.Attack);
        currentCommand.target = target;
    }
}
