using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item", order = 0)]
public class InventoryItem : ScriptableObject
{
    [SerializeField] Sprite icon = null;
    [SerializeField] bool stackable = false;
    [SerializeField] float basePrice;
    [SerializeField] EquipLocation equipLocation = EquipLocation.None;
    [SerializeField] string description;

    public Sprite GetIcon()
    {
        return icon;
    }

    public bool IsStackable()
    {
        return stackable;
    }

    public float GetPrice()
    {
        return basePrice;
    }

    public string GetDescription()
    {
        return description;
    }

    public EquipLocation GetEquipLocation()
    {
        return equipLocation;
    }
}
