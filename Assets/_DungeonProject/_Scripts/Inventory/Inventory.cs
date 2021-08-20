using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int size = 16;
    [SerializeField] ItemSlot[] startingItems;
    [SerializeField] ItemTooltip itemTooltip;

    ItemSlot[] itemSlots;

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
}
