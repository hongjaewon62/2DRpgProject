using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject SlotsParent;
    [SerializeField]
    private GameObject EquipmentSlotsParent;

    private Slot[] slots;

    private EquipmentSlot[] equipSlots;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    ItemEffectDataBase itemEffectDataBase;

    private void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
        equipSlots = EquipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if(playerController.scanObject == null || inventoryActivated == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventoryActivated = !inventoryActivated;

                if (inventoryActivated)
                {
                    OpenInventory();
                }
                else
                {
                    CloseInventory();
                    itemEffectDataBase.HideToolTip();
                }
            }
        }

    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public void AcquireItem(Item item, int count = 1)
    {
        if(Item.ItemType.Equipment != item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(slots[i].item != null)
                {
                    if (slots[i].item.itemName == item.itemName)
                    {
                        slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item, count);
                return;
            }
        }
    }

    public void Equiped(Item item)
    {
        if (equipSlots[0].equipPart == "Helmet" && item.equipmentType == Item.EquipmentType.Helmet)
        {
            equipSlots[0].EquipItem(item);
            //equipSlots[0].equipmentActivated = true;
        }
        else if (equipSlots[1].equipPart == "Chest" && item.equipmentType == Item.EquipmentType.Chest)
        {
            equipSlots[1].EquipItem(item);
            //equipSlots[1].equipmentActivated = true;
        }
        else if (equipSlots[2].equipPart == "Pants" && item.equipmentType == Item.EquipmentType.Pants)
        {
            equipSlots[2].EquipItem(item);
            //equipSlots[2].equipmentActivated = true;
        }
        else if (equipSlots[3].equipPart == "Boots" && item.equipmentType == Item.EquipmentType.Boots)
        {
            equipSlots[3].EquipItem(item);
            //equipSlots[3].equipmentActivated = true;
        }
        else if(equipSlots[4].equipPart == "Weapon" && item.equipmentType == Item.EquipmentType.Weapon)
        {
            equipSlots[4].EquipItem(item);
            //equipSlots[4].equipmentActivated = true;
        }
        // 아이템 장착이 트루일때 같은 아이템 장착시 교체 구현하기


        return;
    }
}
