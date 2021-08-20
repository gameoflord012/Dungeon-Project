using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] Text number;

    InventoryItem inventoryItem;
    ItemTooltip itemTooltip;

    public void SetItemTooltip(ItemTooltip itemTooltip)
    {
        this.itemTooltip = itemTooltip;
    }

    public void Setup(Inventory inventory, int index)
    {
        Inventory.ItemSlot itemSlot = inventory.GetItemSlots()[index];
        if (itemSlot == null)
        {
            inventoryItem = null;
            itemIcon.enabled = false;
            number.enabled = false;
            return;
        }

        inventoryItem = itemSlot.item;
        itemIcon.sprite = itemSlot.item.GetIcon();

        if (itemSlot.number <= 1)
        {
            number.enabled = false;
            return;
        }

        number.text = itemSlot.number.ToString();
    }

    public void ShowTooltip()
    {
        itemTooltip.Setup(inventoryItem);
    }
}
