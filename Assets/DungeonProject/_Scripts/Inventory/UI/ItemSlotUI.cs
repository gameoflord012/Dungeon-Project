using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] Text number;

    public void Setup(Inventory inventory, int index)
    {
        Inventory.ItemSlot itemSlot = inventory.GetItemSlots()[index];
        if (itemSlot == null)
        {
            print("null");
            itemIcon.enabled = false;
            number.enabled = false;
            return;
        }

        itemIcon.sprite = itemSlot.item.GetIcon();
        number.text = itemSlot.number.ToString();
    }
}
