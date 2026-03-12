using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseStats : ScriptableObject
{
    public float hp;
    public float mp;

    public float dodge;
    public float initiative;
   
    public float attackRange;
    public float moveDistance;
        
    public float armor;
    public float magicResistance;
         
    public float accuracy;
    public float intuition;
          
    public float physicalDamage;
    public float magicalDamage;
          
    public float physicalCritChance;
    public float physicalCritDamage;
    public float magicalCritChance;
    public float magicalCritDamage;

    public float GetFloatValue(CharacterStatsEnum characterStats)
    {
        switch (characterStats)
        {
            case CharacterStatsEnum.HP:
                return hp;
            case CharacterStatsEnum.MP:
                return mp;
            case CharacterStatsEnum.Dodge:
                return dodge;
            case CharacterStatsEnum.Initiative:
                return initiative;
            case CharacterStatsEnum.AttackRange:
                return attackRange;
            case CharacterStatsEnum.MoveDistance:
                return moveDistance;
            case CharacterStatsEnum.Armor:
                return armor;
            case CharacterStatsEnum.MagicResistance:
                return magicResistance;
            case CharacterStatsEnum.Accuracy:
                return accuracy;
            case CharacterStatsEnum.Intuition:
                return intuition;
            case CharacterStatsEnum.PhysicalDamage:
                return physicalDamage;
            case CharacterStatsEnum.MagicalDamage:
                return magicalDamage;
            case CharacterStatsEnum.PhysicalCritChance:
                return physicalCritChance;
            case CharacterStatsEnum.PhysicalCritDamage:
                return physicalCritDamage;
            case CharacterStatsEnum.MagicalCritChance:
                return magicalCritChance;
            case CharacterStatsEnum.MagicalCritDamage:
                return magicalCritDamage;
        }
        return 0f;
    }
}
