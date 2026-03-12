using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    //
    [SerializeField] private GameObject itemsParent;
    public GameObject inventoryUI;

    public Transform equipedItems;

    Inventory inventory;

    InventorySlot[] iSlots;
    private GameObject[] slots;

    [SerializeField] private GameObject itemCursor;

    //

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int baseSpace = 24;
    public int space;

    public CharacterObject characterObject;

    [SerializeField] private Slot[] startingItems;

    [SerializeField] public Slot[] items;

    private Slot dragSlot;
    private Slot tempSlot;
    private Slot originalSlot;
    bool isDragingItem;
    

    private void Start()
    {
        inventory = instance;
        //inventory.onItemChangedCallback += UpdateUI;

        iSlots = itemsParent.GetComponentsInChildren<InventorySlot>();

        //
        space = baseSpace + characterObject.characterData.GetIntValue(CharacterStatsEnum.ExtraSlots);

        slots = new GameObject[itemsParent.transform.childCount];
        items = new Slot[space];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new Slot();
        }


        for (int i = 0; i < itemsParent.transform.childCount; i++)
            slots[i] = itemsParent.transform.GetChild(i).gameObject;


        for (int i = 0; i < startingItems.Length; i++)
        {
            Add(startingItems[i].item, startingItems[i].amount);
        }

        //StartCoroutine(LoadDelay());



        UpdateUI();
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(0.1f);

        if (inventory != null)
        {
            for (int i = 0; i < characterObject.characterData.inventory.Length; i++)
            {
                if (items[i].item != null)
                {
                    Add(items[i].item, items[i].amount);
                }

            }
        }
    }

    private void Update()
    {
        itemCursor.SetActive(isDragingItem);
        itemCursor.transform.position = Input.mousePosition;
        if (isDragingItem)
            itemCursor.GetComponent<Image>().sprite = dragSlot.item.icon;

        if (Input.GetKeyDown(KeyCode.I) && SceneManager.GetActiveScene().name != "Creation_Arena")
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Debug.Log("inv on/off");
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isDragingItem)
            {
                ItemDrop();
            }
            else
                ItemDrag();
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(1))
        {
            if (isDragingItem)
            {
                ItemDrop_Single();
            }
            else
                ItemDrag_Half();
        }

        space = baseSpace + characterObject.characterData.GetIntValue(CharacterStatsEnum.ExtraSlots);
    }

    private bool ItemDrag()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.item == null)
            return false; // there is no item to move

        dragSlot = new Slot(originalSlot);
        originalSlot.Clear();
        isDragingItem = true;

        UpdateUI();
        return true;
    }

    private bool ItemDrag_Half()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.item == null)
            return false; // there is no item to move

        dragSlot = new Slot(originalSlot.item, Mathf.CeilToInt(originalSlot.amount / 2f));
        originalSlot.MinusAmount(Mathf.CeilToInt(originalSlot.amount / 2f));
        if (originalSlot.amount == 0)
            originalSlot.Clear();
        isDragingItem = true;

        UpdateUI();
        return true;
    }

    private bool ItemDrop()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            Add(dragSlot.item, dragSlot.amount);
            dragSlot.Clear();
        }
        else
        {
            if (originalSlot.item != null)
            {
                if (originalSlot.item == dragSlot.item && originalSlot.item.Stackable && originalSlot.amount < originalSlot.item.stackSize)   //same items should stack
                {    
                    var amountCanAdd = originalSlot.item.stackSize - originalSlot.amount;
                    var amountToAdd = Mathf.Clamp(dragSlot.amount, 0, amountCanAdd);
                    var remainder = dragSlot.amount - amountToAdd;

                    originalSlot.PlusAmount(amountToAdd);
                    if (remainder == 0)
                        dragSlot.Clear();
                    else
                    {
                        dragSlot.MinusAmount(amountCanAdd);
                        UpdateUI();
                        return false;
                    }
                }
                else
                {
                    tempSlot = new Slot(originalSlot); //a = b
                    originalSlot.AddItem(dragSlot.item, dragSlot.amount); //b = c
                    dragSlot.AddItem(tempSlot.item, tempSlot.amount); //a = c


                    UpdateUI();
                    return true;
                }
            }
            else //place item as usual
            {
                originalSlot.AddItem(dragSlot.item, dragSlot.amount);
                dragSlot.Clear();
            }
        }

        isDragingItem = false;

        UpdateUI();
        return true;
    }

    private bool ItemDrop_Single()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
            return false; // there is no item to move

        if (originalSlot.item != null && (originalSlot.item != dragSlot.item || originalSlot.amount >= originalSlot.item.stackSize))
            return false;

        dragSlot.MinusAmount(1);
        if(originalSlot.item != null && originalSlot.item == dragSlot.item)
            if(originalSlot.item.Stackable)   // prevent stacking unstackable by drag n drop
                originalSlot.PlusAmount(1);
            else
                return false;                     //
        else
            originalSlot.AddItem(dragSlot.item, 1);
        
        if (dragSlot.amount < 1)
        {
            isDragingItem = false;
            dragSlot.Clear();
        }
        else
            isDragingItem = true;

        UpdateUI();
        return true;

    }


    private Slot GetClosestSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
                return items[i];
        }

        return null;
    }

    public bool Add(Item item, int amount) 
    {
            //
        Slot slot = Contains(item);

        if (slot != null && slot.item.Stackable && slot.amount < item.stackSize)
        {

            var amountCanAdd = slot.item.stackSize - slot.amount;
            var amountToAdd = Mathf.Clamp(amount, 0, amountCanAdd);
            var remainder = amount - amountCanAdd;

            slot.PlusAmount(amountToAdd);

            if (remainder > 0)
                Add(item, remainder);
        }

        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].item == null) //this is empty slot
                {
                    var amountCanAdd = item.stackSize - items[i].amount;
                    var amountToAdd = Mathf.Clamp(amount, 0, amountCanAdd);
                    var remainder = amount - amountCanAdd;

                    items[i].AddItem(item, amountToAdd);

                    if (remainder > 0)
                    {
                        Add(item, remainder);
                    }
                        


                    //items[i].AddItem(item, amount);
                    break;
                }
            }
        }
        
        UpdateUI();;
        return true;
    }

    public bool Remove (Item item)
    {
        //items.Remove(item);

        //if (onItemChangedCallback != null)
        //    onItemChangedCallback.Invoke();


        //
        Slot temp = Contains(item);
        if (temp != null)
        {
            if (temp.amount > 1)
                temp.MinusAmount(1);
            else
            {
                int slotRemoveIndex = 0;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].item == item)
                    {
                        slotRemoveIndex = i;
                        break;
                    }
                }

                items[slotRemoveIndex].Clear();
            }
        }
            
        else 
        {
            return false;
        }

        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //if (i < inventory.items.Count)
            try
            {
                iSlots[i].AddItem(inventory.items[i].item);

                //slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().icon;

                if (items[i].item.Stackable)
                    slots[i].GetComponentInChildren<Text>().text = items[i].amount + "";
                else
                    
                    slots[i].GetComponentInChildren<Text>().text = "";
            }
            //else
            catch
            {
                iSlots[i].ClearSlot();

                //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                //slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].GetComponentInChildren<Text>().text = "";
            }
        }
    }

    public Slot Contains(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item == item/* && items[i].item.Stackable &&items[i].amount < items[i].item.stackSize*/)
                return items[i];
        }

        return null;
    }
}
