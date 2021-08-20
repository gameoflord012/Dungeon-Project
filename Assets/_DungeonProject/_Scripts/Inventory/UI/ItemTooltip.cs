using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text itemName;
    [SerializeField] Text itemDescription;

    public void Setup(InventoryItem inventoryItem)
    {
        if (inventoryItem == null)
        {
            itemName.enabled = false;
            itemDescription.enabled = false;
            return;
        }

        itemName.enabled = true;
        itemName.text = inventoryItem.name;

        itemDescription.enabled = true;
        itemDescription.text = inventoryItem.GetDescription();
    }
}
