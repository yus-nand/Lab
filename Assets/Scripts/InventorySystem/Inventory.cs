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
            slots.Add(new InventorySlot(null, 0));
        }
        // slots.Add(new InventorySlot(null, 0));
    }
    public bool AddItem(ItemObject item, int quantity)
    {
        Debug.Log($"AddItem called. Inventory size: {size}, Slots count: {slots.Count}");
        if(item.isStackable)
        {
            for(int i = 0; i < size; i++ )
            {
                if(!slots[i].IsEmpty && slots[i].item != null)
                {
                    Debug.Log($"{slots[i]} filled with {slots[i].item.itemName}");
                    if(slots[i].item == item)
                    {
                        if(slots[i].quantity + quantity > item.maxStackSize)
                        {
                            Debug.Log($"Stack overflow! Current quantity: {slots[i].quantity}, Attempting to add: {quantity}, Max stack size: {item.maxStackSize}");
                            int overflow = slots[i].quantity + quantity - item.maxStackSize;
                            int quantityToAdd = quantity - overflow;
                            slots[i].quantity += quantityToAdd;
                            Debug.Log($"Stacking {quantityToAdd} of {item.name} in slot {i}");
                            if(i + 1 < size)
                            {
                                // i++;                        // we increment i to got to next slot
                                // AddItem(item, overflow);    // but because of this i becomes 0 again
                                SplitAndAddItem(item, overflow, i + 1);
                            }
                            else
                            {
                                Debug.Log($"Ran out of inventory space");
                            }
                        }
                        else
                        {
                            slots[i].quantity += quantity;
                        }
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
                    if(slots[i].quantity + quantity > item.maxStackSize)
                    {
                        Debug.Log("Stack exceeds max stack size, splitting into multiple slots");
                        int overflow = slots[i].quantity + quantity - item.maxStackSize;
                        int quantityToAdd = quantity - overflow;
                        slots[i] = new InventorySlot(item, quantityToAdd);
                        Debug.Log($"Stacking {quantityToAdd} of {item.name} in slot {i}");
                        if(i + 1 < size)
                        {   
                            SplitAndAddItem(item, overflow, i + 1);
                        }
                        else
                        {
                            Debug.Log($"Ran out of inventory space");
                        }
                    }
                    else
                    {
                        slots[i] = new InventorySlot(item, quantity);
                    }
                    Debug.Log($"New item {item.name} added successfully");
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }
        for(int i = 0; i < size; i++ )
        {
            if(slots[i].IsEmpty)
            {
                slots[i]= new InventorySlot(item, quantity);
                Debug.Log($"{item.itemName} added successfully");
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        int emptyCount = 0;
        foreach(var slot in slots) if(slot.IsEmpty) emptyCount++;
        Debug.Log($"Inventory full! Empty slots: {emptyCount}/{slots.Count}");
        return false;   
    }
    void SplitAndAddItem(ItemObject item, int quantity, int index)
    {
        if(quantity > item.maxStackSize)
        {
            int overflow = quantity - item.maxStackSize;
            int quantityToAdd = quantity - overflow;
            slots[index] = new InventorySlot(item, quantityToAdd);
            Debug.Log($"Stacking {quantityToAdd} of {item.name} in slot {index}");

            if(index + 1 < size)
                SplitAndAddItem(item, overflow, index + 1);
            else
                Debug.Log($"Ran out of inventory space");

        }
        else
        {
            Debug.Log($"Stacking {quantity} of {item.name} in slot {index}");
            slots[index] = new InventorySlot(item, quantity);
        }
    }
    public void RemoveItem()
    {
        // will do later
    }
    public void ClearInventory()
    {
        foreach(var slot in slots)
        {
            slots.Clear();
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
