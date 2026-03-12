using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    private void Awake()
    {
        instance = this; 
    }

    [SerializeField] ForceContainer playerForceContainer;
    [SerializeField] ForceContainer enemyForceContainer;

    int round = 1;

    [SerializeField] TMPro.TextMeshProUGUI turnCountText;
    [SerializeField] TMPro.TextMeshProUGUI forceRoundText;

    [SerializeField] MouseInput mouseInput;

    private void Start()
    {
        UpdateTextOnScreen();
    }

    public void AddMe(CharacterTurn character)
    {
        if (character.allegiance == Allegiance.Player)
        {
            playerForceContainer.AddMe(character);
        }

        if (character.allegiance == Allegiance.Enemy)
        {
            enemyForceContainer.AddMe(character);
        }
    }

    Allegiance currentTurn;

    public void NextTurn()
    {
        switch (currentTurn)
        {
            case Allegiance.Player:
                DisablePlayerInput();
                currentTurn = Allegiance.Enemy;
                break;
            case Allegiance.Enemy:
                NextRound();
                EnablePlayerInput();
                currentTurn = Allegiance.Player;
                break;
        }

        GrantTurnToForce();

        UpdateTextOnScreen();
    }

    private void EnablePlayerInput()
    {
        mouseInput.enabled = true;
    }

    private void DisablePlayerInput()
    {
        mouseInput.enabled = false;
    }

    private void GrantTurnToForce()
    {
        switch (currentTurn)
        {
            case Allegiance.Player:
                playerForceContainer.GrantTurn();
                break;
            case Allegiance.Enemy:
                enemyForceContainer.GrantTurn();
                break;
        }
    }

    public void NextRound()
    {
        round += 1;
    }

    void UpdateTextOnScreen()
    {
        turnCountText.text = "Turn: " + round.ToString();
        forceRoundText.text = currentTurn.ToString();
    }
}
