using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField nameTextField;
    CharacterPanel characterPanel;

    [SerializeField] TMPro.TMP_Dropdown classDropdown;
    [SerializeField] TMPro.TMP_Dropdown raceDropdown;

    [SerializeField] ListOfRaces listOfRaces;
    [SerializeField] ListOfClasses listOfClasses;

    [SerializeField] List<GameObject> trainCharButtons;

    private void Awake()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    private void Start()
    {
        UpdateNameField();
        UpdateDropdownLists();
        UpdateCharacterRace(true);
    }

    private void UpdateDropdownLists()
    {
        classDropdown.ClearOptions();
        List<string> classOptions = new List<string>();
        for(int i = 0; i < listOfClasses.classes.Count;  i++)
        {
            classOptions.Add(listOfClasses.classes[i].Name);
        }
        classDropdown.AddOptions(classOptions);
        if(characterPanel.character.classOfCharacter != null)
        {
            classDropdown.value = listOfClasses.classes.IndexOf(characterPanel.character.classOfCharacter);
        }

        raceDropdown.ClearOptions();
        List<string> raceOptions = new List<string>();
        for (int i = 0; i < listOfRaces.races.Count; i++)
        {
            raceOptions.Add(listOfRaces.races[i].Name);
        }
        raceDropdown.AddOptions(raceOptions);
        if(characterPanel.character.race != null)
        {
            raceDropdown.value = listOfRaces.races.IndexOf(characterPanel.character.race);
        }
        
    }

    public void UpdateCharacterClass()
    {
        if(characterPanel.character.classOfCharacter != null)
        {
            List<CharacterStats> originalClassChars = characterPanel.character.classOfCharacter.availableToTrain;
            characterPanel.character.SetTrainableStats(originalClassChars, false);
        }

        characterPanel.character.classOfCharacter = listOfClasses.classes[classDropdown.value];
        List<CharacterStats> charsTrainableForClass = listOfClasses.classes[classDropdown.value].availableToTrain;
        characterPanel.character.SetTrainableStats(charsTrainableForClass);

        UpdateCharButtons();
    }

    private void UpdateCharButtons()
    {
        for(int i = 0; i < characterPanel.character.stats.Count; i++)
        {
            trainCharButtons[i].SetActive(characterPanel.character.stats[i].canBeTrained);
        }
    }

    public void UpdateCharacterRace(bool first = false)
    {
        if(characterPanel.character.race != null)
        {
            if(first == false)
            {
                characterPanel.character.AddRaceBonus(characterPanel.character.race.attributeBonuses, true);
            } 
        }

        characterPanel.character.race = listOfRaces.races[raceDropdown.value];

        characterPanel.character.AddRaceBonus(characterPanel.character.race.attributeBonuses);
    }

    private void UpdateNameField()
    {
        nameTextField.text = characterPanel.character.Name;
    }

    public void UpdateCharacterName()
    {
        characterPanel.character.Name = nameTextField.text;
    }
}
