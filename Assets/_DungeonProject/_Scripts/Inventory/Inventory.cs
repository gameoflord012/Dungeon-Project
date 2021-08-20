using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int size = 16;
    [SerializeField] ItemSlot[] startingItems;
    [SerializeField] Equipment equipment;

    ItemSlot[] itemSlots;
    public event Action inventoryUpdated;

    [Serializable]
    public class ItemSlot
    {
        public InventoryItem item;
        public int number;
    }

    void Awake()
    {
        itemSlots = new ItemSlot[size];

        for (int i = 0; i < startingItems.Length; ++i)
            itemSlots[i] = startingItems[i];
    }

    public ItemSlot[] GetItemSlots()
    {
        return itemSlots;
    }

    public int GetSize()
    {
        return itemSlots.Length;
    }

    public bool AddToFirstEmptySlot(InventoryItem item, int number)
    {
        int i = FindSlot(item);

        if (i < 0)
            return false;

        itemSlots[i].item = item;
        itemSlots[i].number += number;

        if (inventoryUpdated != null)
            inventoryUpdated();

        return true;
    }

    public void RemoveFromSlot(int slot, int number)
    {
        itemSlots[slot].number -= number;

        if (itemSlots[slot].number <= 0)
        {
            itemSlots[slot].number = 0;
            itemSlots[slot].item = null;
        }

        if (inventoryUpdated != null)
            inventoryUpdated();
    }

    private int FindSlot(InventoryItem item)
    {
        int i = FindStack(item);
        if (i < 0)
            return FindEmptySlot();
        return i;
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < itemSlots.Length; i++)
            if (itemSlots[i].item == null)
                return i;
        return -1;
    }

    private int FindStack(InventoryItem item)
    {
        if (item.IsStackable())
            for (int i = 0; i < itemSlots.Length; i++)
                if (object.ReferenceEquals(itemSlots[i].item, item))
                    return i;
        return -1;
    }
}
