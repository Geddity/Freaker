using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterClass : ScriptableObject
{
    public string Name;
    public string classInfo;
    public CharacterAttributes attributeBonus;
    public CharacterAttributes levelUpRateBonus;
    public Stats statBonus;

    public float GetFloatValue(CharacterStatsEnum characterStats)
    {
        switch (characterStats)
        {
            case CharacterStatsEnum.HP:
                return statBonus.hp;
            case CharacterStatsEnum.MP:
                return statBonus.mp;
            case CharacterStatsEnum.Dodge:
                return statBonus.dodge;
            case CharacterStatsEnum.Initiative:
                return statBonus.initiative;
            case CharacterStatsEnum.AttackRange:
                return statBonus.attackRange;
            case CharacterStatsEnum.MoveDistance:
                return statBonus.movementPoints;
            case CharacterStatsEnum.Armor:
                return statBonus.armor;
            case CharacterStatsEnum.MagicResistance:
                return statBonus.magicResistance;
            case CharacterStatsEnum.Accuracy:
                return statBonus.accuracy;
            case CharacterStatsEnum.Intuition:
                return statBonus.intuition;
            case CharacterStatsEnum.PhysicalDamage:
                return statBonus.physicalDamage;
            case CharacterStatsEnum.MagicalDamage:
                return statBonus.magicalDamage;
            case CharacterStatsEnum.PhysicalCritChance:
                return statBonus.physicalCritChance;
            case CharacterStatsEnum.PhysicalCritDamage:
                return statBonus.physicalCritDamage;
            case CharacterStatsEnum.MagicalCritChance:
                return statBonus.magicalCritChance;
            case CharacterStatsEnum.MagicalCritDamage:
                return statBonus.magicalCritDamage;
        }
        return 0f;
    }
}
