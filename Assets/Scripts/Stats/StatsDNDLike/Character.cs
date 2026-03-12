using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum CharacterAttribute
{
    Strength,
    Dexterity,
    Endurance,
    Perception,
    Intelligence,
    Willpower,
    None
}

public enum CharacterOtherStat
{
    AttributePoints
}

[Serializable]
public class Attribute
{
    public int attributeScore;
    public CharacterAttribute attribute;
    public int bonus;

    public int AttributeScore
    {
        get { 
            return attributeScore + bonus;
        }
    }

    public int Mod
    {
        get {
            int mod = attributeScore - 10;
            mod += bonus;
            mod += mod < 0 ? -1 : 0;
            return mod / 2; 
        }
    }

    public Attribute(CharacterAttribute attribute, int attributeScore)
    {
        this.attribute = attribute;
        this.attributeScore = attributeScore;
    }
}

public enum CharacterStats
{
    maximumHP,
    maximumMP,
    dodge,
    initiative,
    attackRange,
    moveDistance,
    armor,
    magicResistance,
    physicalDamageBonus,
    magicalDamageBonus,
    accuracy,
    intuition,
    physalCritChance,
    physalCritDamage,
    magicalCritChance,
    magicalCritDamage,
}

[Serializable]
public class Stat
{
    public CharacterStats charStats;
    public CharacterAttribute attributeMod;
    public CharacterAttribute attributeMod2;
    public bool trained;
    public bool canBeTrained = false;
    public int bonus;

    //public int hp = 40;
    //public int attackRange = 1;
    //public int armor = 10;
    //public int magicResistance = 10;

    //public float dodge = 0.15f;
    //public float accuracy = 0.7f;
    //public float critChance = 0.15f;
    //public float critDamageBonus = 1.7f;
    //public float movementPoints = 50f;

    //public float GetFloatValue(CharacterStats characterStats)
    //{
    //    switch (characterStats)
    //    {
    //        case CharacterStats.dodge:
    //            return dodge;
    //        case CharacterStats.moveDistance:
    //            return movementPoints;
    //        case CharacterStats.physicalDamageBonus:
    //            return dodge;
    //        case CharacterStats.magicalDamageBonus:
    //            return dodge;
    //        case CharacterStats.accuracy:
    //            return accuracy;
    //        case CharacterStats.intuition:
    //            return accuracy;
    //        case CharacterStats.physalCritChance:
    //            return critChance;
    //        case CharacterStats.physalCritDamage:
    //            return critDamageBonus;
    //        case CharacterStats.magicalCritChance:
    //            return critChance;
    //        case CharacterStats.magicalCritDamage:
    //            return critDamageBonus;
    //    }
    //    return 0f;
    //}

    //public int GetIntValue(CharacterStats characterStats)
    //{
    //    switch (characterStats)
    //    {
    //        case CharacterStats.maximumHP:
    //            return hp;
    //        case CharacterStats.maximumMP:
    //            return hp;
    //        case CharacterStats.initiative:
    //            return hp;
    //        case CharacterStats.attackRange:
    //            return attackRange;
    //        case CharacterStats.armor:
    //            return armor;
    //        case CharacterStats.magicResistance:
    //            return magicResistance;
    //    }
    //    return 0;
    //}


    public Stat(CharacterStats charStats, CharacterAttribute attribute, CharacterAttribute attribute2, bool trained = false, bool canBeTrained = false, int bonus = 0)
    {
        this.charStats = charStats;
        this.attributeMod = attribute;
        this.attributeMod2 = attribute2;
        this.trained = trained;
        this.canBeTrained = canBeTrained;
        this.bonus = bonus;
    }

    public int GetStatValue(Character character)
    {
        int v = character.GetAttributeMod(attributeMod);
        int b = character.GetAttributeMod(attributeMod2);

        int n = Mathf.RoundToInt(0.6f * v) + Mathf.RoundToInt(0.4f * b);
        n += bonus;
        n += trained ? 4 : 0;
        return n;
    }

    public int GetStatValue2(Character character)
    {
        int v = character.GetAttributeMod(attributeMod);

        int n = 32 + (int)v * 6;
        n += bonus;
        n += trained ? 4 : 0;
        return n;
    }

    public int GetStatValue3(Character character)
    {
        int v = character.GetAttributeMod(attributeMod);
        int b = character.GetAttributeMod(attributeMod2);

        int n = 15 + Mathf.RoundToInt(0.6f * v) + Mathf.RoundToInt(0.4f * b);
        n += bonus;
        n += trained ? 4 : 0;
        return n;
    }

    public int GetStatValue4(Character character)
    {
        int v = character.GetAttributeMod(attributeMod);
        int b = character.GetAttributeMod(attributeMod2);

        int n = 70 + Mathf.RoundToInt(1.2f * v) + Mathf.RoundToInt(0.8f * b);
        n += bonus;
        n += trained ? 4 : 0;
        return n;
    }

    public int GetStatValue5(Character character)
    {
        int v = character.GetAttributeMod(attributeMod);
        int b = character.GetAttributeMod(attributeMod2);

        int n = 170 + v + Mathf.RoundToInt(0.5f * b);
        n += bonus;
        n += trained ? 4 : 0;
        return n;
    }
}

[Serializable]
public class Statistic
{
    public CharacterOtherStat statistic;
    public CharacterAttribute attributeMod;
    public int bonus;
    public int currentValue;

    public Statistic(CharacterOtherStat statistic, CharacterAttribute attributeMod, int bonus = 0)
    {
        this.statistic = statistic;
        this.attributeMod = attributeMod;
        this.bonus = bonus;
    }

    public int GetStatisticValue(Character character)
    {
        int v = character.GetAttributeMod(this.attributeMod);
        v += bonus;
        v += currentValue;
        return v;
    }
}

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public string Name = "Nameless";
    public Level level;
    public Race race;
    public Class classOfCharacter;

    private Stat stat;

    public List<Attribute> attributes;
    public List<Stat> stats;
    public List<Statistic> statistics;

    [SerializeField] StatList baseStats;

    const int defaultAttributeValue = 10;
    const int changeAttributeMinValue = 4;
    const int changeAttributeMaxValue = 20;

    

    public void ChangeAtrScore(int by, CharacterAttribute attribute)
    {
        Attribute a = attributes[(int)attribute];

        if(a.attributeScore + by > changeAttributeMaxValue || a.attributeScore + by < changeAttributeMinValue)
        {
            return;
        }

        Statistic attributePoints = statistics[(int)CharacterOtherStat.AttributePoints];
        if(by > 0)
        {
            if(a.attributeScore + 1 > 16)
            {
                if(attributePoints.bonus >= 2)
                {
                    attributePoints.bonus -= 2;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (attributePoints.bonus >= 1)
                {
                    attributePoints.bonus -= 1;
                }
                else
                {
                    return;
                }

            }
        }
        else
        {
            if(a.attributeScore > 16)
            {
                attributePoints.bonus += 2;
            }
            else
            {
                attributePoints.bonus += 1;
            }
        }

        a.attributeScore += by;
    }

    [ContextMenu("Generate Character")]
    public void GenerateCharacterBase()
    {
        attributes = new List<Attribute>();
        attributes.Add(new Attribute(CharacterAttribute.Strength, defaultAttributeValue));
        attributes.Add(new Attribute(CharacterAttribute.Dexterity, defaultAttributeValue));
        attributes.Add(new Attribute(CharacterAttribute.Endurance, defaultAttributeValue));
        attributes.Add(new Attribute(CharacterAttribute.Perception, defaultAttributeValue));
        attributes.Add(new Attribute(CharacterAttribute.Intelligence, defaultAttributeValue));
        attributes.Add(new Attribute(CharacterAttribute.Willpower, defaultAttributeValue));

        stats = new List<Stat>();
        for(int i  = 0; i < baseStats.stats.Count; i++)
        {
            Stat c = baseStats.stats[i];
            stats.Add(new Stat(c.charStats, c.attributeMod, c.attributeMod2));
        }

        statistics = new List<Statistic>();
        statistics.Add(new Statistic(CharacterOtherStat.AttributePoints, CharacterAttribute.None, 27));
    }

    internal int GetAttributeMod(CharacterAttribute attributeMod)
    {
        if(attributeMod == CharacterAttribute.None)
        {
            return 0;
        }

        return attributes[(int)attributeMod].Mod;
    }

    internal Stat GetCharStat(CharacterStats charStats)
    {
        return stats[(int)charStats];
    }

    public Statistic GetStats(CharacterOtherStat stat)
    {
        return statistics[(int)stat];
    }

    internal void TrainCharStat(CharacterStats charStats)
    {
        Stat charToTrain = GetCharStat(charStats);
        charToTrain.trained = !charToTrain.trained;
    }

    internal void SetTrainableStats(List<CharacterStats> statsTrainableForClass, bool set = true)
    {
        for(int i = 0; i < statsTrainableForClass.Count; i++)
        {
            CharacterStats charStats = statsTrainableForClass[i];
            Stat charToTrain = GetCharStat(charStats);
            charToTrain.canBeTrained = set;
        }
    }

    public void AddRaceBonus(List<Attribute> attributeBonuses, bool subtract = false)
    {
        for (int i = 0; i < attributeBonuses.Count; i++)
        {
            Attribute a = attributes[(int)attributeBonuses[i].attribute];
            if(subtract == false)
            {
                a.bonus += attributeBonuses[i].attributeScore;
            }
            else
            {
                a.bonus -= attributeBonuses[i].attributeScore;
            }
            
        }
    }

    //public int GetIntValue(CharacterStats characterStat)
    //{
    //    return stat.GetIntValue(characterStat);
    //}

    //public float GetFloatValue(CharacterStats characterStat)
    //{
    //    return stat.GetFloatValue(characterStat);
    //}
}
