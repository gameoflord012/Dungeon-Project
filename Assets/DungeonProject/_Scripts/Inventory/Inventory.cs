using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] int size = 16;

    [Serializable]
    public class ItemSlot
    {
        public InventoryItem item;
        public int number;
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
