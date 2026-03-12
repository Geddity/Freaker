using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI characterName;
    [SerializeField] TMPro.TextMeshProUGUI levelText;
    [SerializeField] Slider expBar;
    [SerializeField] TMPro.TextMeshProUGUI expValueText;

    [SerializeField] TMPro.TextMeshProUGUI attributePointsText;

    [Header("Attributes")]

    [SerializeField] CharacterAttributeText strAttText;
    [SerializeField] CharacterAttributeText dexAttText;
    [SerializeField] CharacterAttributeText endAttText;
    [SerializeField] CharacterAttributeText senAttText;
    [SerializeField] CharacterAttributeText intAttText;
    [SerializeField] CharacterAttributeText wilAttText;

    [Header("Stats")]

    [SerializeField] CharacterAttributeText healthText;
    [SerializeField] CharacterAttributeText manaText;
    [SerializeField] CharacterAttributeText physicalDamageText;
    [SerializeField] CharacterAttributeText magicalDamageText;
    [SerializeField] CharacterAttributeText armorText;
    [SerializeField] CharacterAttributeText magicResText;
    [SerializeField] CharacterAttributeText dodgeText;
    [SerializeField] CharacterAttributeText initiativeText;
    [SerializeField] CharacterAttributeText accuracyText;
    [SerializeField] CharacterAttributeText intuitionText;
    [SerializeField] CharacterAttributeText attackRangeText;
    [SerializeField] CharacterAttributeText moveDistanceText;
    [SerializeField] CharacterAttributeText physicalCritChanceText;
    [SerializeField] CharacterAttributeText physicalCritDamageText;
    [SerializeField] CharacterAttributeText magicalCritChanceText;
    [SerializeField] CharacterAttributeText magicalCritDamageText;

    public void UpdateStatus(CharacterObject character)
    {
        characterName.text = character.characterData.Name;

        expBar.maxValue = character.characterData.level.RequiredExpToLvlUp;
        expBar.value = character.characterData.level.experience;
        levelText.text = "Lvl. " + character.characterData.level.level.ToString() + " "
            + character.characterData.race.Name.ToString() + " " 
            + character.characterData.characterClass.Name.ToString();
        expValueText.text = character.characterData.level.experience.ToString() + " / " + character.characterData.level.RequiredExpToLvlUp;

        attributePointsText.text = character.characterData.level.attributePoints.ToString();

        strAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Strength));
        dexAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Dexterity));
        endAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Endurance));
        senAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Senses));
        intAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Intellect));
        wilAttText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Will));

        healthText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.HP));
        manaText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.MP));
        physicalDamageText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.PhysicalDamage));
        magicalDamageText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.MagicalDamage));
        armorText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.Armor));
        magicResText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.MagicResistance));
        dodgeText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.Dodge));
        initiativeText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.Initiative));
        accuracyText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.Accuracy));
        intuitionText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.Intuition));
        attackRangeText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.AttackRange));
        moveDistanceText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.MoveDistance));
        physicalCritChanceText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.PhysicalCritChance));
        physicalCritDamageText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.PhysicalCritDamage));
        magicalCritChanceText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.MagicalCritChance));
        magicalCritDamageText.UpdateText(character.characterData.GetIntValue(CharacterStatsEnum.MagicalCritDamage));
    }
}
