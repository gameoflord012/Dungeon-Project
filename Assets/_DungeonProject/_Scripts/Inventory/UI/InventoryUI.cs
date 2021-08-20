using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] ItemSlotUI itemSlotPrefab;
    [SerializeField] ItemTooltip itemTooltip;

    void Start()
    {
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
