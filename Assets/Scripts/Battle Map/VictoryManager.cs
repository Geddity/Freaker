using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] ForceContainer enemyForces;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] MouseInput mouseInput;

    public void CheckPlayerVictory()
    {
        if(enemyForces.CheckDefeated() == true)
        {
            Victory();
        }
    }

    private void Victory()
    {
        mouseInput.enabled = false;
        victoryPanel.SetActive(true);
        Debug.Log("VICTORY!");
    }

    public void ReturnToWorldMap()
    {
        SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
    }
}
