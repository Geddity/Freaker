using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;
using Spine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int armorModifier;
    public int damageModifier;

    [Header("Skin Settings")]
    public SkeletonDataAsset skeletonDataAsset;
    public SkinChange skinsSystem;
    [SpineSkin(dataField: "skeletonDataAsset")] public string itemSkin;
    public SkinChange.ItemType itemType;

    [Header("Weapon Settings")]
    public bool isWeapon = false;
    public bool twoHandWeapon = false;

    [Header("Armor Settings")]
    public bool highHeels = false;
    public bool isChangingFoot = false;
    public bool isReplacingHand = false;
    public bool isHidingHair = false;
    public bool isHidingEars = false;
    [SpineSkin(dataField: "skeletonDataAsset")] public string[] handSkins = { "hands/c1", "hands/c2", "hands/c3", "hands/c4", "hands/c5", "hands/iron1", };
    public int replacingSkin = 0;
    public override void Use()
    {
        base.Use();
        RemoveFromInventory();
        EquipmentManager.instance.Equip(this);
    }
}

