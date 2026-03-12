using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public SkeletonDataAsset[] skeletonDataAsset;
    public SkinChange[] skinsSystem;
    public GameObject[] skeleton;
    public SkinChange targetScript;

    public CharacterObject characterObject;

    public Transform equipedItems;
    private EquipedSlot[] eSlots;

    public Item[] items;


    public RaceChange raceChange;
    public int activeSkeleton;
    public bool highHeelsOn = false;
    private Vector3 originalPos;

    public WeaponAnims weaponAnims;
    public Item[] mainHand;
    public Item[] offHand;

    private Slot slot;

    //

    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;

        activeSkeleton = characterObject.characterData.skins.skeleton;
    }

    #endregion

    public Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;
    EquipmentManager equipmentory;
    private EquipedSlot eSlot;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(SkinChange.ItemType)).Length;
        currentEquipment = new Equipment[numSlots];

        //

        targetScript = skeleton[activeSkeleton].GetComponent<SkinChange>();

        activeSkeleton = characterObject.characterData.skins.skeleton;

        equipmentory = EquipmentManager.instance;

        eSlots = equipedItems.GetComponentsInChildren<EquipedSlot>();
        eSlot = equipedItems.GetComponentInChildren<EquipedSlot>();

        items = new Item[eSlots.Length];

        originalPos = skeleton[activeSkeleton].transform.position; //mojet potom portovat

        weaponAnims = skeleton[activeSkeleton].GetComponent<WeaponAnims>();

        //StartCoroutine(LoadDelay());
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(0.1f);
        if (currentEquipment != null)
        {
            for (int i = 0; i < currentEquipment.Length; i++)
            {
                Equip(currentEquipment[i], false);
            }
        }
    }

    public void Equip(Equipment newItem, bool addToInventory = true)
    {
        if (newItem != null)
        {
            int slotIndex = (int)newItem.itemType;

            Equipment oldItem = null;

            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                if (addToInventory)
                {
                    inventory.Add(oldItem, 1);
                }
            }

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(newItem, oldItem);

                for (int i = 0; i < eSlots.Length; i++)
                {
                    if (i == slotIndex)
                    {
                        items[i] = newItem;
                        eSlots[i].AddItem(equipmentory.items[i]);
                    }
                }
            }

            currentEquipment[slotIndex] = newItem;

            //change skin to equiped one
            var itemSkin = newItem.itemSkin;
            var itemType = newItem.itemType;

            skinsSystem[activeSkeleton].Equip(itemSkin, itemType);

            //hide hair and ears for helm
            if (slotIndex == 0)
            {
                if (newItem.isHidingHair)
                {
                    targetScript.HideHair();
                }

                if (newItem.isHidingEars)
                {
                    targetScript.HideEars();
                }
            }

            //change hands for bracers
            if (slotIndex == 2)
            {
                if (newItem.isReplacingHand)
                {
                    targetScript.activeHandColorIndex = newItem.replacingSkin;
                    targetScript.ChangeHands();
                }
                else
                {
                    targetScript.ReturnHands();
                }
            }

            //high heels on
            if (slotIndex == 5)
            {
                if (newItem.highHeels && highHeelsOn == false && raceChange.isFemale)
                {
                    highHeelsOn = true;
                    skeleton[activeSkeleton].transform.position += new Vector3(0, 0.7f, 0);
                }
                else if (!newItem.highHeels)
                {
                    highHeelsOn = false;
                    skeleton[activeSkeleton].transform.position = originalPos;
                }

                //change foot
                if (newItem.isChangingFoot)
                {
                    targetScript.ChangeFoot();
                }
                else
                {
                    targetScript.ReturnFoot();
                }
            }

            if (slotIndex == 6)
            {
                if (newItem.twoHandWeapon)
                {
                    weaponAnims.TwoHandSword();
                    eSlots[7].AddItem(newItem);
                    if (currentEquipment[7] != null)
                    {
                        Unequip(7);
                        eSlots[7].AddItem(newItem);
                        weaponAnims.TwoHandSword();
                    }
                }
                else
                {
                    weaponAnims.OneHandSword();

                    if (currentEquipment[7] != null && !currentEquipment[7].isWeapon)
                    {
                        weaponAnims.OneHandAndShield();
                    }
                    else if (currentEquipment[7] != null && currentEquipment[7].isWeapon)
                    {
                        weaponAnims.DualWield();
                    }
                    else if (oldItem != null && oldItem.twoHandWeapon)
                    {
                        eSlots[7].ClearSlot();
                        weaponAnims.OneHandSword();
                    }
                }
            }

            if (slotIndex == 7 && !newItem.isWeapon)
            {
                weaponAnims.OneHandAndShield();
                if (currentEquipment[6] != null && currentEquipment[6].twoHandWeapon)
                {
                    Unequip(6);
                    eSlots[7].AddItem(newItem);
                    weaponAnims.OneHandAndShield();
                }
            }
            if (slotIndex == 7 && newItem.isWeapon)
            {
                weaponAnims.DualWield();
                if (currentEquipment[6] != null && currentEquipment[6].twoHandWeapon)
                {
                    Unequip(6);
                    eSlots[7].AddItem(newItem);
                    weaponAnims.DualWield();
                }
            }
        }
            
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            if (inventory.Add(oldItem, 1))
            {
                currentEquipment[slotIndex] = null;

                if (onEquipmentChanged != null)
                {
                    onEquipmentChanged.Invoke(null, oldItem);

                    for (int i = 0; i < eSlots.Length; i++)
                    {
                        if (i == slotIndex)
                        {
                            eSlots[i].ClearSlot();
                            items[i] = null;
                        }
                    }

                    //equip censored skin
                    if (slotIndex != 4)
                    {
                        var itemSkin = "default";
                        var itemType = oldItem.itemType;
                        skinsSystem[activeSkeleton].Equip(itemSkin, itemType);

                    }
                    else
                    {
                        var itemSkin = "armor/censored";
                        var itemType = oldItem.itemType;
                        skinsSystem[activeSkeleton].Equip(itemSkin, itemType);
                    }

                    //return hair and ear skin
                    if (slotIndex == 0)
                    {
                        if (oldItem.isHidingHair)
                        {
                            targetScript.UnhideHair();
                        }

                        if (oldItem.isHidingEars)
                        {
                            targetScript.UnhideEars();
                        }
                    }

                    //return hands
                    if (slotIndex == 2 && oldItem.isReplacingHand)
                    {
                        targetScript.ReturnHands();
                    }

                    //high heels off
                    if (slotIndex == 5 && oldItem.highHeels)
                    {
                        highHeelsOn = false;
                        skeleton[activeSkeleton].transform.position = originalPos;
                    }

                    //return foot
                    if (slotIndex == 5 && oldItem.isChangingFoot)
                    {
                        targetScript.ReturnFoot();
                    }

                    if (currentEquipment[6] == null && currentEquipment[7] == null)
                    {
                        weaponAnims.Idle();
                    }

                    if (oldItem.twoHandWeapon)
                    {
                        eSlots[7].ClearSlot();
                    }

                } 
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();

        //

        targetScript = skeleton[activeSkeleton].GetComponent<SkinChange>();

        activeSkeleton = raceChange.skeletonIndex;

        
    }
}



