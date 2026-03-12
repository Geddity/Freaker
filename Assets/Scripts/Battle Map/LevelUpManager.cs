using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public CharacterObject targetCharacter;
    [SerializeField] LevelUpPanel levelUpPanel;
    [SerializeField] GameObject levelUpPanelGO;
    [SerializeField] GameObject levelUpButton;

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        targetCharacter = stageManager.stageCharacter;

        targetCharacter.characterData.levelUpEvent.AddListener(ShowLevelUpButton);
    }

    private void Update()
    {
        levelUpPanel.UpdateStatus(targetCharacter);
    }

    public void AddExperience(int exp)
    {
        targetCharacter.AddExperience(exp);
    }

    public void AddAttributePoint(CharacterAttributeEnum attribute)
    {
        targetCharacter.AddAttributePoint(attribute);
    }

    public void ShowLevelUpButton()
    {
        levelUpButton.SetActive(true);
    }

    public void OpenLevelUpMenu()
    {
        levelUpPanelGO.SetActive(true);
        levelUpPanel.UpdateStatus(targetCharacter);
    }

    public void CloseLevelUpMenu()
    {
        levelUpPanelGO.SetActive(false);
    }
}
