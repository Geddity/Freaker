using System.Collections;
using UnityEngine;

[System.Serializable]
public class Slot 
{
    [field: SerializeField] public Item item { get; private set; } = null;
    [field: SerializeField] public int amount { get; private set; } = 0;

    public Slot()
    {
        item = null;
        amount = 0;
    }

    public Slot (Item newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
    }

    public Slot(Slot slot)
    {
        this.item = slot.item;
        this.amount = slot.amount;
    }

    public void Clear() 
    { 
        this.item = null; 
        this.amount = 0;
    }

    //public Item GetItem() { return item; }
    //public int GetAmount() { return amount; }
    public void PlusAmount(int newAmount) { amount += newAmount; }
    public void MinusAmount(int newAmount) 
    { 
        amount -= newAmount;
        //if (amount <= 0)
        //{
        //    Clear();
        //}
    }
    public void AddItem(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;  
    }
}
