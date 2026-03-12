using Spine.Unity.Examples;
using UnityEngine;
using UnityEngine.UI;

public class EquipedSlot : MonoBehaviour
{
    public Image icon;

    Item item;

    public SkinChange.ItemType itemType;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
 
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void Useitem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
