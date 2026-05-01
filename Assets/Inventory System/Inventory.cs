using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour      // add this script to the player or a gameobject that stays in the scene
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
        {                               // fins a stack that can accommodate the new quantity
            InventorySlot slot = slots.Find(slots => !slots.IsEmpty && slots.item == item && slots.quantity < item.maxStackSize);
            if(slot != null)
            {
                if(slot.quantity + quantity > item.maxStackSize)
                {
                    int overflow = slot.quantity + quantity - item.maxStackSize;
                    int quantityToAdd = quantity - overflow;
                    slot.quantity += quantityToAdd;
                    SplitAndAdd(item, overflow);
                }
                else
                {
                    slot.quantity += quantity;
                }
                OnInventoryChanged?.Invoke();
                return true;
            }                           // if no stack found, find an empty slot
            else if(slots.Exists(slots => slots.IsEmpty && slots.item == null))
            {
                InventorySlot emptySlot = slots.Find(slots => slots.IsEmpty && slots.item == null);
                
                if(quantity > item.maxStackSize)
                {
                    int overflow = quantity - item.maxStackSize;
                    int quantityToAdd = quantity - overflow;
                    emptySlot.item = item;
                    emptySlot.quantity = quantityToAdd;
                    SplitAndAdd(item, overflow);
                }
                else
                {
                    emptySlot.item = item;
                    emptySlot.quantity = quantity;
                }
                OnInventoryChanged?.Invoke();
                return true;
            }
            else
            {
                Debug.Log($"No existing stack found for {item.name}, looking for empty slot");
                return false;
            }
        }

        // logic for non stackable items

        InventorySlot _slot = slots.Find(slots => slots.IsEmpty & slots.item == null);
        _slot.item = item;
        _slot.quantity = quantity;
        OnInventoryChanged?.Invoke();
        return true;  
    }
    public void SplitAndAdd(ItemObject item, int quantity)
    {
        InventorySlot slot = slots.Find(slots => slots.IsEmpty && slots.item == null /*&& slots.quantity < item.maxStackSize*/);
        if(slot != null)
        {
            if(quantity > item.maxStackSize)
            {
                int overflow = quantity - item.maxStackSize;
                int quantityToAdd = quantity - overflow;
                slot.item = item;
                slot.quantity = quantityToAdd;
                if(overflow > 0)
                {
                    SplitAndAdd(item, overflow);
                }
            }
            else
            {
                slot.item = item;
                slot.quantity = quantity;
            }
        }
        else
        {
            Debug.Log($"Ran out of inventory space");
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
