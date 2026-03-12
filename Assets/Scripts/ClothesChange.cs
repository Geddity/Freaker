using Spine;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesChange : MonoBehaviour
{
    public EquipmentManager equipmentManager;
    public int activeSkeleton;
    public int savedSkeleton;
    public SkinChange targetScript;
    public Equipment equipment;
    public GameObject itemIcon;

    [SerializeField] private GameObject ears;
    [SerializeField] private GameObject hairs;

    public bool highHeelsOn = false;
    private Vector3 originalPos;

    public EquipedSkins equipedSkins;

    public void Start()
    {
        activeSkeleton = equipmentManager.activeSkeleton;
        targetScript = equipmentManager.targetScript;

        savedSkeleton = activeSkeleton;

        GameObject buttonIcon = itemIcon;
        buttonIcon.GetComponent<Image>().enabled = true;
        buttonIcon.GetComponent<Image>().sprite = equipment.icon;
    }

    public void ButtonPressed()
    {
        if (GetComponent<Toggle>().isOn)
        {
            Equip();
        }
        else
        {
            UnEquip();
        }
    }

    public void ReEquip()
    {
        targetScript.StartSkin();

        equipmentManager.skinsSystem[activeSkeleton].clothesSkin = "default";
        equipmentManager.skinsSystem[activeSkeleton].pantsSkin = "armor/censored";
        equipmentManager.skinsSystem[activeSkeleton].beltSkin = "default";
        equipmentManager.skinsSystem[activeSkeleton].bracersSkin = "default";
        equipmentManager.skinsSystem[activeSkeleton].bootsSkin = "default";
        equipmentManager.skinsSystem[activeSkeleton].helmSkin = "default";

        targetScript.ReturnFoot();
        equipmentManager.skeleton[activeSkeleton].transform.position = originalPos;

        for (int i = 0; i < equipedSkins.equipedSkins.Length; i++)
        {
            if(equipedSkins.equipedSkins[i] != null)
            {
                var itemSkin = equipedSkins.equipedSkins[i].itemSkin;
                var itemType = equipedSkins.equipedSkins[i].itemType;

                equipmentManager.skinsSystem[activeSkeleton].Equip(itemSkin, itemType);

                if (itemType == SkinChange.ItemType.Helm)
                {
                    if (equipedSkins.equipedSkins[i].isHidingHair)
                    {
                        targetScript.HideHair();
                    }

                    if (equipedSkins.equipedSkins[i].isHidingEars)
                    {
                        targetScript.HideEars();
                    }
                }

                if (itemType == SkinChange.ItemType.Bracers)
                {
                    if (equipedSkins.equipedSkins[i].isReplacingHand)
                    {
                        targetScript.activeHandColorIndex = equipedSkins.equipedSkins[i].replacingSkin;
                        targetScript.ChangeHands();
                    }
                    else
                    {
                        targetScript.ReturnHands();
                    }
                }

                if (itemType == SkinChange.ItemType.Boots)
                {
                    if (equipedSkins.equipedSkins[i].highHeels && equipmentManager.raceChange.isFemale)
                    {
                        highHeelsOn = true;
                        equipmentManager.skeleton[activeSkeleton].transform.position += new Vector3(0, 0.7f, 0);
                    }
                    else if (!equipedSkins.equipedSkins[i].highHeels)
                    {
                        highHeelsOn = false;
                        equipmentManager.skeleton[activeSkeleton].transform.position = originalPos;
                    }

                    if (equipedSkins.equipedSkins[i].isChangingFoot)
                    {
                        targetScript.ChangeFoot();
                    }
                    else
                    {
                        targetScript.ReturnFoot();
                    }
                }
            } 
        }
    }

    public void Equip()
    {
        var itemSkin = equipment.itemSkin;
        var itemType = equipment.itemType;

        int slotIndex = (int)equipment.itemType;

        for (int i = 0; i < equipedSkins.equipedSkins.Length; i++)
        {
            if (i == slotIndex)
            {
                equipedSkins.equipedSkins[i] = equipment;
            }
        }

        equipmentManager.skinsSystem[activeSkeleton].Equip(itemSkin, itemType);
        if (itemType == SkinChange.ItemType.Helm)
        {
            if (equipment.isHidingHair)
            {
                targetScript.HideHair();

                foreach (Transform child in hairs.transform)
                {
                    if (child.TryGetComponent(out Button button))
                    {
                        button.interactable = false;
                    }
                }
            }

            if (equipment.isHidingEars)
            {
                targetScript.HideEars();

                foreach (Transform child in ears.transform)
                {
                    if (child.TryGetComponent(out Button button))
                    {
                        button.interactable = false;
                    }
                }
            }
        }

        if (itemType == SkinChange.ItemType.Bracers)
        {
            if (equipment.isReplacingHand)
            {
                targetScript.activeHandColorIndex = equipment.replacingSkin;
                targetScript.ChangeHands();
            }
            else
            {
                targetScript.ReturnHands();
            }
        }

        if (itemType == SkinChange.ItemType.Boots)
        {
            if (equipment.highHeels && highHeelsOn == false && equipmentManager.raceChange.isFemale)
            {
                highHeelsOn = true;
                equipmentManager.skeleton[activeSkeleton].transform.position += new Vector3(0, 0.7f, 0);
            }
            else if (!equipment.highHeels)
            {
                highHeelsOn = false;
                equipmentManager.skeleton[activeSkeleton].transform.position = originalPos;
            }

            if (equipment.isChangingFoot)
            {
                targetScript.ChangeFoot();
            }
            else
            {
                targetScript.ReturnFoot();
            }
        }
    }

    public void UnEquip()
    {
        int slotIndex = (int)equipment.itemType;

        for (int i = 0; i < equipedSkins.equipedSkins.Length; i++)
        {
            if (i == slotIndex)
            {
                equipedSkins.equipedSkins[i] = null;
            }
        }

        var itemType = equipment.itemType;
        if (itemType == SkinChange.ItemType.Pants)
        {
            var itemSkin = "armor/censored";
            equipmentManager.skinsSystem[activeSkeleton].Equip(itemSkin, itemType);
        }
        else
        {
            var itemSkin = "default";
            equipmentManager.skinsSystem[activeSkeleton].Equip(itemSkin, itemType);
        }

        if (itemType == SkinChange.ItemType.Helm)
        {
            if (equipment.isHidingHair)
            {
                targetScript.UnhideHair();

                foreach (Transform child in hairs.transform)
                {
                    if (child.TryGetComponent(out Button button))
                    {
                        button.interactable = true;
                    }
                }
            }

            if (equipment.isHidingEars)
            {
                targetScript.UnhideEars();

                foreach (Transform child in ears.transform)
                {
                    if (child.TryGetComponent(out Button button))
                    {
                        button.interactable = true;
                    }
                }
            }
        }

        if (itemType == SkinChange.ItemType.Bracers)
        {
            if (equipment.isReplacingHand)
            {
                targetScript.ReturnHands();
            }
        }

        if (itemType == SkinChange.ItemType.Boots)
        {
            if (equipment.highHeels)
            {
                highHeelsOn = false;
                equipmentManager.skeleton[activeSkeleton].transform.position = originalPos;
            }

            if (equipment.isChangingFoot)
            {
                targetScript.ReturnFoot();
            }
        }    
    }

    private void Update()
    {
        activeSkeleton = equipmentManager.activeSkeleton;
        targetScript = equipmentManager.targetScript;

        if (savedSkeleton != activeSkeleton)
        {
            StartCoroutine(ReEquipDelay());
            savedSkeleton = activeSkeleton;
        }
    }

    IEnumerator ReEquipDelay()
    {
        yield return new WaitForSeconds(0.01f);
        ReEquip();
    }
}
