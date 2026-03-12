using Spine.Unity.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

[Serializable]
public class Int2Val
{
    public int current;
    public int max;

    public bool canGoNegative;

    public Int2Val(int current, int max)
    {
        this.current = current;
        this.max = max;
    }

    internal void Subtract(int amount)
    {
        current -= amount;

        if(canGoNegative == false)
        {
            if (current < 0)
            {
                current = 0;
            }
        }
    }
}

[Serializable]
public class Level
{
    public int RequiredExpToLvlUp = 100;
    //{
    //    get {
    //        return level * 100;  
    //    }
    //}

    public int level = 1;
    public int experience = 0;
    public int attributePoints = 0;

    public void AddExperience(int exp)
    {
        experience += exp;
    }

    public bool CheckLevelUp()
    {
        return experience >= RequiredExpToLvlUp;
    }

    public void LevelUp()
    {
        experience -= RequiredExpToLvlUp;
        level += 1;
        attributePoints += 2;
        RequiredExpToLvlUp += (level - 1) * 100;
    }
}

public enum CharacterAttributeEnum
{
    Strength,
    Dexterity,
    Endurance,
    Senses,
    Intellect,
    Will,
}

[Serializable]
public class CharacterAttributes
{
    public const int attributesCount = 6;

    public int strength;
    public int dexterity;
    public int endurance;
    public int senses;
    public int intellect;
    public int will;

    public CharacterAttributes() 
    {

    }

    public void Sum(CharacterAttributeEnum attribute, int val)
    {
        switch (attribute)
        {
            case CharacterAttributeEnum.Strength:
                strength += val;
                break;
            case CharacterAttributeEnum.Dexterity:
                dexterity += val;
                break;
            case CharacterAttributeEnum.Endurance:
                endurance += val;
                break;
            case CharacterAttributeEnum.Senses:
                senses += val;
                break;
            case CharacterAttributeEnum.Intellect:
                intellect += val;
                break;
            case CharacterAttributeEnum.Will:
                will += val;
                break;
        }
    }

    public int Get(CharacterAttributeEnum i)
    {
        switch (i)
        {
            case CharacterAttributeEnum.Strength:
                return strength;
            case CharacterAttributeEnum.Dexterity:
                return dexterity;
            case CharacterAttributeEnum.Endurance:
                return endurance;
            case CharacterAttributeEnum.Senses:
                return senses;
            case CharacterAttributeEnum.Intellect:
                return intellect;
            case CharacterAttributeEnum.Will:
                return will;
        }
        return -1;
    }
}

public enum CharacterStatsEnum
{
    HP,
    MP,
    Dodge,
    Initiative,
    AttackRange,
    MoveDistance,
    Armor,
    MagicResistance,
    Accuracy,
    Intuition,
    PhysicalDamage,
    MagicalDamage,
    PhysicalCritChance,
    PhysicalCritDamage,
    MagicalCritChance,
    MagicalCritDamage,
    ExtraSlots
}

[Serializable]
public class Stats
{
    public float hp;
    public float mp;

    public float dodge;
    public float initiative;

    public float attackRange;
    public float movementPoints;

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

    public float extraSlots;

    public int damage;

    public void StatSum(CharacterAttributeEnum attribute, int val)
    {
        switch (attribute)
        {
            case CharacterAttributeEnum.Strength:
                physicalDamage += val / 2f;
                physicalCritDamage += (val * 3f) / 4f;
                extraSlots += val / 10f;
                break;
            case CharacterAttributeEnum.Dexterity:
                dodge += val / 2f;
                initiative += (val * 3f) / 5f;
                movementPoints += val / 10f;
                accuracy += (val * 2f) / 5f;
                physicalCritChance += (val * 3f) / 5f;
                physicalCritDamage += (val * 2f) / 4f;
                break;
            case CharacterAttributeEnum.Endurance:
                hp += val * 6f;
                armor += val / 10f;
                break;
            case CharacterAttributeEnum.Senses:
                attackRange += val / 5f;
                accuracy += (val * 3f) / 5f;
                intuition += (val * 3f) / 5f;
                physicalCritChance += (val * 2f) / 5f;
                magicalCritChance += (val * 2f) / 5f;
                break;
            case CharacterAttributeEnum.Intellect:
                mp += val * 2f;
                magicalDamage += val / 2f;
                magicalCritDamage += (val * 3f) / 4f;
                break;
            case CharacterAttributeEnum.Will:
                mp += val * 4f;
                initiative += (val * 2f) / 5f;
                magicResistance += val / 2f;
                intuition += (val * 2f) / 5f;
                magicalCritChance += (val * 3f) / 5f;
                magicalCritDamage += (val * 2f) / 4f;
                break;
        }
    }

    public float GetFloatValue(CharacterStatsEnum characterStat)
    {
        switch (characterStat)
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
                return movementPoints;
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
            case CharacterStatsEnum.ExtraSlots:
                return extraSlots;
        }

        return 0f;
    }

    public float GetIntValue(CharacterStatsEnum characterStats)
    {
        switch (characterStats)
        {
            case CharacterStatsEnum.HP:
                return hp;
            case CharacterStatsEnum.MP:
                return mp; 
            case CharacterStatsEnum.PhysicalDamage:
                return damage;
            case CharacterStatsEnum.MagicalDamage:
                return damage;
            case CharacterStatsEnum.Initiative:
                return hp;
            case CharacterStatsEnum.AttackRange:
                return attackRange;
            case CharacterStatsEnum.Armor:
                return armor;
            case CharacterStatsEnum.MagicResistance:
                return magicResistance;
        }
        return 0;
    }
}

[Serializable]
public class CharacterEquipment
{
    public int test = 0;
}

//[Serializable]
//public class CharacterInventory
//{
//    public Slot[] characterInventory;
//}

public enum CharacterSkin
{
    SkinColor,
    EyeColor,
    Mouth,
    Scar,
    Nose,
    Forehead,
    Jaw,
    EyeType,
    Ear,
    Hair,
    HairColor,
    Brow,
    BrowColor,
    Mustash,
    MustashColor,
    Beard,
    BeardColor
}

[Serializable]
public class Skins
{
    public int skeleton;

    public int skinColor;
    public int eyeColor;
    public int mouth;
    public int scar;
    public int nose;
    public int forehead;
    public int jaw;
    public int eyeType;
    public int ear;
    public int hair;
    public int hairColor;
    public int brow;
    public int browColor;
    public int mustash;
    public int mustashColor;
    public int beard;
    public int beardColor;

    public float height;

    public int GetSkin(CharacterSkin characterSkin)
    {
        switch (characterSkin)
        {
            case CharacterSkin.SkinColor:
                return skinColor;
            case CharacterSkin.EyeColor:
                return eyeColor;
            case CharacterSkin.Mouth:
                return mouth;
            case CharacterSkin.Scar:
                return scar;
            case CharacterSkin.Nose:
                return nose;
            case CharacterSkin.Forehead:
                return forehead;
            case CharacterSkin.Jaw:
                return jaw;
            case CharacterSkin.EyeType:
                return eyeType;
            case CharacterSkin.Ear:
                return ear;
            case CharacterSkin.Hair:
                return hair;
            case CharacterSkin.HairColor:
                return hairColor;
            case CharacterSkin.Brow:
                return brow;
            case CharacterSkin.BrowColor:
                return browColor;
            case CharacterSkin.Mustash:
                return mustash;
            case CharacterSkin.MustashColor:
                return mustashColor;
            case CharacterSkin.Beard:
                return beard;
            case CharacterSkin.BeardColor:
                return beardColor;
        }
        return 0;
    }
}

public class CharacterObject : MonoBehaviour
{
    public CharacterData characterData;
    public EquipmentManager equipmentManager;
    public Inventory inventory;

    public Int2Val hp = new Int2Val(40, 40);

    public DamageType damageType;
    public bool defeated;

    private void Awake()
    {
        //var eManager = GameObject.Find("GameManager");
        //equipmentManager = eManager.GetComponent<EquipmentManager>();
        //inventory = eManager.GetComponent<Inventory>();
    }

    private async void Start()
    {
        //if (attributes == null)
        //{
        //    Init();
        //} 

        int space = inventory.baseSpace + characterData.GetIntValue(CharacterStatsEnum.ExtraSlots);

        if(characterData.inventory == null)
        {
            characterData.inventory = new Slot[space];

            var items = characterData.inventory;

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new Slot();
            }
        }
        

        characterData.LoadEquipment(equipmentManager);
        characterData.LoadInventory(inventory);

        if (inventory != null)
        {
            for (int i = 0; i < characterData.inventory.Length; i++)
            {
                if (inventory.items[i].item != null)
                {
                    inventory.UpdateUI();
                }

            }
        }

        await Task.Delay(1);
        if (equipmentManager.currentEquipment != null)
        {
            if (equipmentManager != null)
            {
                for (int i = 0; i < equipmentManager.currentEquipment.Length; i++)
                {
                    equipmentManager.Equip(equipmentManager.currentEquipment[i], false);
                }
            }
        }

    }

    //public void Init()
    //{
    //    attributes = new CharacterAttributes();
    //    level = new Level();
    //}

    private void Update()
    {

        characterData.SetEquipment(equipmentManager);
        characterData.SetInventory(inventory);
    }

    public int GetDamage()
    {
        return characterData.GetDamage(damageType);
    }

    public void TakeDamage(int damage)
    {
        hp.Subtract(damage);
        CheckDefeat();
    }

    private void CheckDefeat()
    {
        if(hp.current <= 0)
        {
            Defeated();
        }
        else
        {
            Flinch();
        }
    }

    CharacterAnimator characterAnimator;

    private void Flinch()
    {
        if (characterAnimator == null) { characterAnimator = GetComponentInChildren<CharacterAnimator>(); }

        characterAnimator.TakeDamage();
    }

    private void Defeated()
    {
        if (characterAnimator == null) { characterAnimator = GetComponentInChildren<CharacterAnimator>(); }

        defeated = true;
        characterAnimator.Defeated();
    }

    public void AddExperience(int exp)
    {
        characterData.AddExperience(exp);
    }

    public void AddAttributePoint(CharacterAttributeEnum attribute)
    {
        characterData.AddAttributePoint(attribute);
    }

    public int GetIntValue(CharacterStatsEnum characterStat)
    {
        return characterData.GetIntValue(characterStat);
    }

    public float GetFloatValue(CharacterStatsEnum characterStat)
    {
        return characterData.GetFloatValue(characterStat);
    }

    //public void SetCharacterData(CharacterData characterData)
    //{
    //    this.characterData = characterData;
    //}
}
