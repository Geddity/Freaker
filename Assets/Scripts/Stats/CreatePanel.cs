using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePanel : MonoBehaviour
{
    public CharacterData characterData;

    private void Update()
    {
        characterData.GenerateCharacterBase();
    }

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

    public void UpdateStatus()
    {
        strAttText.UpdateText(characterData.attributes.Get(CharacterAttributeEnum.Strength));
        dexAttText.UpdateText(characterData.attributes.Get(CharacterAttributeEnum.Dexterity));
        endAttText.UpdateText(characterData.attributes.Get(CharacterAttributeEnum.Endurance));
        senAttText.UpdateText(characterData.attributes.Get(CharacterAttributeEnum.Senses));
        intAttText.UpdateText(characterData.attributes.Get(CharacterAttributeEnum.Intellect));
        wilAttText.UpdateText(characterData.attributes.Get(CharacterAttributeEnum.Will));

        healthText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.HP));
        manaText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.MP));
        physicalDamageText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.PhysicalDamage));
        magicalDamageText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.MagicalDamage));
        armorText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.Armor));
        magicResText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.MagicResistance));
        dodgeText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.Dodge));
        initiativeText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.Initiative));
        accuracyText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.Accuracy));
        intuitionText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.Intuition));
        attackRangeText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.AttackRange));
        moveDistanceText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.MoveDistance));
        physicalCritChanceText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.PhysicalCritChance));
        physicalCritDamageText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.PhysicalCritDamage));
        magicalCritChanceText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.MagicalCritChance));
        magicalCritDamageText.UpdateText(characterData.GetIntValue(CharacterStatsEnum.MagicalCritDamage));
    }
}
