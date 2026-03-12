using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Physical,
    Magical
}

public class Attack : MonoBehaviour
{
    GridObject gridObject;
    CharacterAnimator characterAnimator;
    [SerializeField] AnimationHandle animationHandle;
    CharacterObject characterObject;

    private void Awake()
    {
        characterObject = GetComponent<CharacterObject>();
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    public void AttackGridObject(GridObject targetGridObject)
    {
        RotateCharacter(targetGridObject.transform.position);
        characterAnimator.Attack();

        CharacterObject target = targetGridObject.GetComponent<CharacterObject>();

        float hitChance = (characterObject.GetFloatValue(CharacterStatsEnum.Accuracy) - target.GetFloatValue(CharacterStatsEnum.Dodge)) 
            / (characterObject.GetFloatValue(CharacterStatsEnum.Accuracy) + target.GetFloatValue(CharacterStatsEnum.Dodge));

        if (UnityEngine.Random.value >= hitChance)
        {
            Debug.Log("Miss!");
            return;
        }

        float damage = characterObject.GetDamage();
        float damageReduction = target.GetIntValue(CharacterStatsEnum.Armor) / (target.GetIntValue(CharacterStatsEnum.Armor) + 100f);
        float magicResistance = target.GetIntValue(CharacterStatsEnum.MagicResistance) / (target.GetIntValue(CharacterStatsEnum.MagicResistance) + 100f);

        if (UnityEngine.Random.value <= characterObject.GetFloatValue(CharacterStatsEnum.PhysicalCritChance))
        {
            damage = (int)(damage * characterObject.GetFloatValue(CharacterStatsEnum.PhysicalCritDamage));
            Debug.Log("Crit!");
        }

        switch (characterObject.damageType)
        {
            case DamageType.Physical:
                damage *= (1 - damageReduction);
                break;
            case DamageType.Magical:
                damage *= (1 - magicResistance);
                break;
        }

        
        Debug.Log("target takes " + (int)damage + " damage");
        target.TakeDamage((int)damage);
    }

    private void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        if (transform.position.x < towards.x)
        {
            animationHandle.SetFlip(-1);
        }
        else
        {
            animationHandle.SetFlip(1);
        }
        
    }
}
