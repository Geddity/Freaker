using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [HideInInspector]
    public UnityEvent levelUpEvent = new UnityEvent();

    public string Name = "Nameless";

    public CharacterRace race;
    public CharacterClass characterClass;

    public Level level;
    public CharacterAttributes attributes;
    public CharacterAttributes levelUpRates;

    public Stats stats;

    [SerializeField] BaseStats baseStats;

    public Equipment[] equipment;
    public Slot[] inventory;
    public Skins skins;

    [ContextMenu("Generate Character")]
    public void GenerateCharacterBase()
    {
        //level
        level.level = 1;
        level.RequiredExpToLvlUp = 100;
        level.experience = 0;
        level.attributePoints = 0;

        //attributes
        attributes.strength = race.attributeBonus.strength + characterClass.attributeBonus.strength;
        attributes.dexterity = race.attributeBonus.dexterity + characterClass.attributeBonus.dexterity;
        attributes.endurance = race.attributeBonus.endurance + characterClass.attributeBonus.endurance;
        attributes.senses = race.attributeBonus.senses + characterClass.attributeBonus.senses;
        attributes.intellect = race.attributeBonus.intellect + characterClass.attributeBonus.intellect;
        attributes.will = race.attributeBonus.will + characterClass.attributeBonus.will;

        //lvl up rates
        levelUpRates.strength = race.levelUpRateBonus.strength + characterClass.levelUpRateBonus.strength;
        levelUpRates.dexterity = race.levelUpRateBonus.dexterity + characterClass.levelUpRateBonus.dexterity;
        levelUpRates.endurance = race.levelUpRateBonus.endurance + characterClass.levelUpRateBonus.endurance;
        levelUpRates.senses = race.levelUpRateBonus.senses + characterClass.levelUpRateBonus.senses;
        levelUpRates.intellect = race.levelUpRateBonus.intellect + characterClass.levelUpRateBonus.intellect;
        levelUpRates.will = race.levelUpRateBonus.will + characterClass.levelUpRateBonus.will;

        //stats
        stats.hp = GetIntValue(CharacterStatsEnum.HP);
        stats.mp = GetIntValue(CharacterStatsEnum.MP);
        stats.dodge = GetIntValue(CharacterStatsEnum.Dodge);
        stats.initiative = GetIntValue(CharacterStatsEnum.Initiative);
        stats.attackRange = GetIntValue(CharacterStatsEnum.AttackRange);
        stats.movementPoints = GetIntValue(CharacterStatsEnum.MoveDistance);

        stats.armor = GetIntValue(CharacterStatsEnum.Armor);
        stats.magicResistance = GetIntValue(CharacterStatsEnum.MagicResistance);
        stats.accuracy = GetIntValue(CharacterStatsEnum.Accuracy);
        stats.intuition = GetIntValue(CharacterStatsEnum.Intuition);
        stats.physicalDamage = GetIntValue(CharacterStatsEnum.PhysicalDamage);
        stats.magicalDamage = GetIntValue(CharacterStatsEnum.MagicalDamage);

        stats.physicalCritChance = GetIntValue(CharacterStatsEnum.PhysicalCritChance);
        stats.physicalCritDamage = GetIntValue(CharacterStatsEnum.PhysicalCritDamage);
        stats.magicalCritChance = GetIntValue(CharacterStatsEnum.MagicalCritChance);
        stats.magicalCritDamage = GetIntValue(CharacterStatsEnum.MagicalCritDamage);

        stats.extraSlots = GetIntValue(CharacterStatsEnum.ExtraSlots);
    }

    [ContextMenu("Reset Skins")]
    public void ResetSkins()
    {
        skins.skeleton = 0;
        skins.skinColor = 0;
        skins.eyeColor = 0;
        skins.mouth = 0;
        skins.scar = 0;
        skins.nose = 0;
        skins.forehead = 0;
        skins.jaw = 0;
        skins.eyeType = 0;
        skins.ear = 0;
        skins.hair = 0;
        skins.hairColor = 0;
        skins.brow = 0;
        skins.browColor = 0;
        skins.mustash = 0;
        skins.mustashColor = 0;
        skins.beard = 0;
        skins.beardColor = 0;
    }

    [ContextMenu("Reset Equipment")]
    public void ResetEquipment()
    {
        equipment = null;
    }

    [ContextMenu("Reset Inventory")]
    public void ResetInventory()
    {
        inventory = null;
    }

    public void SetEquipment(EquipmentManager equiped)
    {
        equipment = equiped.currentEquipment;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory.items;
    }

    public void LoadEquipment(EquipmentManager equiped)
    {
        equiped.currentEquipment = equipment;
        equiped.items = equipment;
    }

    public void LoadInventory(Inventory inventory)
    {
        //inventory.items = this.inventory;

        if (this.inventory == null) { return; }
        else
        {
            for(int i = 0; i < this.inventory.Length; i++) 
            {
                inventory.items[i].AddItem(this.inventory[i].item, this.inventory[i].amount);
            }
            
        }
    }

    public int GetDamage(DamageType damageType)
    {
        int damage = 0;

        switch (damageType)
        {
            case DamageType.Physical:
                damage += attributes.strength;
                break;
            case DamageType.Magical:
                damage += attributes.intellect;
                break;
        }

        return damage;
    }

    public void AddExperience(int exp)
    {
        level.AddExperience(exp);
        if (level.CheckLevelUp())
        {
            LevelUp();
            levelUpEvent.Invoke();
        }
    }

    [ContextMenu("Level Up")]
    public void LevelUp()
    {
        level.LevelUp();
        LevelUpAttributes();
        UpdateStats();
    }

    public void AddAttributePoint(CharacterAttributeEnum attribute)
    {
        if(level.attributePoints <= 0) { return; }
        for (int i = 0; i < 1; i++)
        {
            level.attributePoints -= 1;
            attributes.Sum(attribute, 1);
            stats.StatSum(attribute, 1);
        } 
    }

    private void LevelUpAttributes()
    {
        for (int i = 0; i < CharacterAttributes.attributesCount; i++)
        {
            int rate = levelUpRates.Get((CharacterAttributeEnum)i);
            rate += UnityEngine.Random.Range(0, 100);
            rate /= 100;
            if (rate > 0)
            {
                attributes.Sum((CharacterAttributeEnum)i, rate);
            }
        }
    }

    private float AddStatBonus(CharacterStatsEnum characterStat)
    {
        float value = 0;
        switch (characterStat)
        {
            case CharacterStatsEnum.HP:
                value = baseStats.GetFloatValue(characterStat) 
                    + attributes.Get(CharacterAttributeEnum.Endurance) * 6f;
                return value;
            case CharacterStatsEnum.MP:
                value = baseStats.GetFloatValue(characterStat) 
                    + attributes.Get(CharacterAttributeEnum.Intellect) * 2f 
                    + attributes.Get(CharacterAttributeEnum.Will) * 4f;
                return value;

            case CharacterStatsEnum.Dodge:
                value = baseStats.GetFloatValue(characterStat) 
                    + attributes.Get(CharacterAttributeEnum.Dexterity) / 2f;
                return value;
            case CharacterStatsEnum.Initiative:
                value = baseStats.GetFloatValue(characterStat)
                    + (attributes.Get(CharacterAttributeEnum.Dexterity) * 3f
                    + attributes.Get(CharacterAttributeEnum.Will) * 2f) / 5f;
                return value;

            case CharacterStatsEnum.AttackRange:
                value = baseStats.GetFloatValue(characterStat) 
                    + attributes.Get(CharacterAttributeEnum.Senses) / 5f;
                return value;
            case CharacterStatsEnum.MoveDistance:
                value = baseStats.GetFloatValue(characterStat) 
                    + attributes.Get(CharacterAttributeEnum.Dexterity) / 10f;
                return value;

            case CharacterStatsEnum.Armor:
                value = baseStats.GetFloatValue(characterStat) 
                    + attributes.Get(CharacterAttributeEnum.Endurance) / 10f;
                return value;
            case CharacterStatsEnum.MagicResistance:
                value = baseStats.GetFloatValue(characterStat) 
                    + attributes.Get(CharacterAttributeEnum.Will) / 2f;
                return value;

            case CharacterStatsEnum.Accuracy:
                value = baseStats.GetFloatValue(characterStat)
                    + (attributes.Get(CharacterAttributeEnum.Senses) * 3f
                    + attributes.Get(CharacterAttributeEnum.Dexterity) * 2f) / 5f;
                return value;
            case CharacterStatsEnum.Intuition:
                value = baseStats.GetFloatValue(characterStat)
                    + (attributes.Get(CharacterAttributeEnum.Senses) * 3f
                    + attributes.Get(CharacterAttributeEnum.Will) * 2f) / 5f;
                return value;

            case CharacterStatsEnum.PhysicalDamage:
                value = baseStats.GetFloatValue(characterStat)
                    + attributes.Get(CharacterAttributeEnum.Strength) / 2f;
                return value;
            case CharacterStatsEnum.MagicalDamage:
                value = baseStats.GetFloatValue(characterStat)
                    + attributes.Get(CharacterAttributeEnum.Intellect) / 2f;
                return value;

            case CharacterStatsEnum.PhysicalCritChance:
                value = baseStats.GetFloatValue(characterStat)
                    + (attributes.Get(CharacterAttributeEnum.Senses) * 2f
                    + attributes.Get(CharacterAttributeEnum.Dexterity) * 3f) / 5f;
                return value;
            case CharacterStatsEnum.PhysicalCritDamage:
                value = baseStats.GetFloatValue(characterStat)
                    + (attributes.Get(CharacterAttributeEnum.Strength) * 3f
                    + attributes.Get(CharacterAttributeEnum.Dexterity) * 2f) / 4f;
                return value;

            case CharacterStatsEnum.MagicalCritChance:
                value = baseStats.GetFloatValue(characterStat)
                    + (attributes.Get(CharacterAttributeEnum.Senses) * 2f
                    + attributes.Get(CharacterAttributeEnum.Will) * 3f) / 5f;
                return value;
            case CharacterStatsEnum.MagicalCritDamage:
                value = baseStats.GetFloatValue(characterStat)
                    + (attributes.Get(CharacterAttributeEnum.Intellect) * 3f
                    + attributes.Get(CharacterAttributeEnum.Will) * 2f) / 4f;
                return value;
            case CharacterStatsEnum.ExtraSlots:
                value = baseStats.GetFloatValue(characterStat)
                    + attributes.Get(CharacterAttributeEnum.Strength) / 10f;
                if (value > 12) { return 12; }
                return value;
        }
        return 0f;
    }

    private void UpdateStats()
    {
        stats.hp = GetIntValue(CharacterStatsEnum.HP);
        stats.mp = GetIntValue(CharacterStatsEnum.MP);
        stats.dodge = GetIntValue(CharacterStatsEnum.Dodge);
        stats.initiative = GetIntValue(CharacterStatsEnum.Initiative);
        stats.attackRange = GetIntValue(CharacterStatsEnum.AttackRange);
        stats.movementPoints = GetIntValue(CharacterStatsEnum.MoveDistance);

        stats.armor = GetIntValue(CharacterStatsEnum.Armor);
        stats.magicResistance = GetIntValue(CharacterStatsEnum.MagicResistance);
        stats.accuracy = GetIntValue(CharacterStatsEnum.Accuracy);
        stats.intuition = GetIntValue(CharacterStatsEnum.Intuition);
        stats.physicalDamage = GetIntValue(CharacterStatsEnum.PhysicalDamage);
        stats.magicalDamage = GetIntValue(CharacterStatsEnum.MagicalDamage);

        stats.physicalCritChance = GetIntValue(CharacterStatsEnum.PhysicalCritChance);
        stats.physicalCritDamage = GetIntValue(CharacterStatsEnum.PhysicalCritDamage);
        stats.magicalCritChance = GetIntValue(CharacterStatsEnum.MagicalCritChance);
        stats.magicalCritDamage = GetIntValue(CharacterStatsEnum.MagicalCritDamage);

        stats.extraSlots = GetIntValue(CharacterStatsEnum.ExtraSlots);
    }

    public int GetIntValue(CharacterStatsEnum characterStat)
    {
        return Mathf.FloorToInt(AddStatBonus(characterStat) 
            + race.GetFloatValue(characterStat)
            + characterClass.GetFloatValue(characterStat));
    }

    public float GetFloatValue(CharacterStatsEnum characterStat)
    {
        return stats.GetFloatValue(characterStat);
    }
}
