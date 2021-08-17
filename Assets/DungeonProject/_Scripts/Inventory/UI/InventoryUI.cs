using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] ItemSlotUI itemSlotPrefab;

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
            itemSlot.Setup(inventory, i);
        }
    }
}
