using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI characterName;
    [SerializeField] Slider hpBar;
    [SerializeField] TMPro.TextMeshProUGUI levelText;
    [SerializeField] Slider expBar;

    [SerializeField] CharacterAttributeText strAttText;
    [SerializeField] CharacterAttributeText dexAttText;
    [SerializeField] CharacterAttributeText endAttText;
    [SerializeField] CharacterAttributeText senAttText;
    [SerializeField] CharacterAttributeText intAttText;
    [SerializeField] CharacterAttributeText wilAttText;

    public void UpdateStatus(CharacterObject character)
    {
        hpBar.maxValue = character.hp.max;
        hpBar.value = character.hp.current;
        characterName.text = character.characterData.Name;

        expBar.maxValue = character.characterData.level.RequiredExpToLvlUp;
        expBar.value = character.characterData.level.experience;
        levelText.text = "Lvl. " + character.characterData.level.level.ToString();

        strAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Strength));
        dexAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Dexterity));
        endAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Endurance));
        senAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Senses));
        intAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Intellect));
        wilAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Will));
    }
}
