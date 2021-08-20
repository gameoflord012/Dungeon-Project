using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] ItemSlotUI itemSlotPrefab;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Button removeButton;
    [SerializeField] Button equipButton;

    InventoryItem currentItem = null;

    void Start()
    {
        inventory.inventoryUpdated += RefreshUI;

        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (Transform sampleChild in transform)
            Destroy(sampleChild.gameObject);

        for (int i = 0; i < inventory.GetSize(); ++i)
        {
            ItemSlotUI itemSlot = Instantiate(itemSlotPrefab, transform);
            itemSlot.SetInventoryUI(this);
            itemSlot.Setup(inventory, i);
        }
    }

    public void SetInventoryItem(InventoryItem inventoryItem)
    {
        currentItem = inventoryItem;
    }

    private void RefreshMenuUI()
    {
        itemTooltip.Setup(currentItem);

    }
}
