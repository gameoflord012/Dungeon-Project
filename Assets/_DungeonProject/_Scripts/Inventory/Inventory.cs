using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemSlot[] itemSlots;

#pragma warning disable 414
    [SerializeField] int size = 16;
#pragma warning restore 414

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
