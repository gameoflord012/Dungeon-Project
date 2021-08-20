using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] EquipLocation equipLocation = EquipLocation.None;
    [SerializeField] Sprite nullIcon;

    Equipment equipment;

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<Equipment>();
        equipment.equipmentUpdated += Redraw;
    }

    void Start()
    {
        Redraw();
    }

    private void Redraw()
    {
        InventoryItem item = equipment.GetItemInSlot(equipLocation);
        icon.sprite = item != null ? item.GetIcon() : nullIcon;
    }
}
