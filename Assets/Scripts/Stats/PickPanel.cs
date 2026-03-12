using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField nameTextField;
    CreatePanel createPanel;

    [SerializeField] TMPro.TMP_Dropdown classDropdown;
    [SerializeField] TMPro.TMP_Dropdown raceDropdown;

    [SerializeField] RaceList raceList;
    [SerializeField] ClassList classList;

    [SerializeField] TMPro.TextMeshProUGUI raceInfo;
    [SerializeField] TMPro.TextMeshProUGUI classInfo;

    private void Awake()
    {
        createPanel = GetComponentInParent<CreatePanel>();
    }

    private void Start()
    {
        UpdateNameField();
        UpdateDropdownLists();
        UpdateCharacterRace();
        createPanel.UpdateStatus();
    }

    private void UpdateDropdownLists()
    {
        classDropdown.ClearOptions();
        List<string> classOptions = new List<string>();
        for (int i = 0; i < classList.classes.Count; i++)
        {
            classOptions.Add(classList.classes[i].Name);
        }
        classDropdown.AddOptions(classOptions);
        if (createPanel.characterData.characterClass != null)
        {
            classDropdown.value = classList.classes.IndexOf(createPanel.characterData.characterClass);
        }

        raceDropdown.ClearOptions();
        List<string> raceOptions = new List<string>();
        for (int i = 0; i < raceList.races.Count; i++)
        {
            raceOptions.Add(raceList.races[i].Name);
        }
        raceDropdown.AddOptions(raceOptions);
        if (createPanel.characterData.race != null)
        {
            raceDropdown.value = raceList.races.IndexOf(createPanel.characterData.race);
        }

    }

    public void UpdateCharacterClass()
    {
        createPanel.characterData.characterClass = classList.classes[classDropdown.value];

        createPanel.characterData.GenerateCharacterBase();

        createPanel.UpdateStatus();

        classInfo.text = createPanel.characterData.characterClass.classInfo;
    }

    public void UpdateCharacterRace()
    {
        createPanel.characterData.race = raceList.races[raceDropdown.value];

        createPanel.characterData.GenerateCharacterBase();

        createPanel.UpdateStatus();

        raceInfo.text = createPanel.characterData.race.raceInfo;
    }

    private void UpdateNameField()
    {
        nameTextField.text = createPanel.characterData.Name;
    }

    public void UpdateCharacterName()
    {
        createPanel.characterData.Name = nameTextField.text;
    }
}
