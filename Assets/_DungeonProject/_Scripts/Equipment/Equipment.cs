using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    Dictionary<EquipLocation, InventoryItem> equippedItems = new Dictionary<EquipLocation, InventoryItem>();

    public event Action equipmentUpdated;

    public InventoryItem GetItemInSlot(EquipLocation equipLocation)
    {
        if (!equippedItems.ContainsKey(equipLocation))
            return null;

        return equippedItems[equipLocation];
    }

    public void AddItem(EquipLocation slot, InventoryItem item)
    {
        if (equippedItems.ContainsKey(slot))
            inventory.AddToFirstEmptySlot(equippedItems[slot], 1);

        equippedItems[slot] = item;

        if (equipmentUpdated != null)
            equipmentUpdated();
    }

    public void RemoveItem(EquipLocation slot)
    {
        if (equippedItems.ContainsKey(slot))
            inventory.AddToFirstEmptySlot(equippedItems[slot], 1);

        equippedItems.Remove(slot);

        if (equipmentUpdated != null)
            equipmentUpdated();
    }
}
