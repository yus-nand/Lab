using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int size = 20;
    public List<InventorySlot> slots = new List<InventorySlot>();

    public event Action OnInventoryChanged;

    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            slots.Add(new InventorySlot());
        }
    }
    public bool AddItem(ItemObject item, int quantity)
    {
        Debug.Log($"AddItem called. Inventory size: {size}, Slots count: {slots.Count}");
        if(item.isStackable)
        {
            foreach(var slot in slots)
            {
                if(!slot.IsEmpty && slot.item.itemObject != null)
                {
                    Debug.Log($"slot filled with {slot.item.itemObject.itemName}");
                    if(slot.item.itemObject == item)
                    {
                        slot.item.quantity += quantity;
                        Debug.Log($"{item.name} stacked successfully");
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    slot.item = new ItemInstance(item, quantity);
                    Debug.Log($"New item {item.name} added successfully");
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }
        foreach(var slot in slots)
        {
            if(slot.IsEmpty)
            {
                slot.item = new ItemInstance(item, quantity);
                Debug.Log($"{item.itemName} added successfully");
                OnInventoryChanged?.Invoke();
                return true;
            }
            else if(slot.item?.itemObject != null)
            {
                Debug.Log($"slot filled with {slot.item.itemObject.itemName}");
            }
        }
        int emptyCount = 0;
        foreach(var slot in slots) if(slot.IsEmpty) emptyCount++;
        Debug.Log($"Inventory full! Empty slots: {emptyCount}/{slots.Count}");
        return false;   
    }
    public void RemoveItem()
    {
        // will do later
    }
    public void ClearInventory()
    {
        foreach(var slot in slots)
        {
            slot.Clear();
        }
        OnInventoryChanged?.Invoke();
    }
    public void Swap(int a, int b)
    {
        var temp = slots[a].item;
        slots[a].item = slots[b].item;
        slots[b].item = temp;

        OnInventoryChanged?.Invoke();
    }
}
