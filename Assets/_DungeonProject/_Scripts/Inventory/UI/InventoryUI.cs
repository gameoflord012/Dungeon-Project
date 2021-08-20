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
            itemSlot.SetItemTooltip(itemTooltip);
            itemSlot.Setup(inventory, i);
        }
    }
}
