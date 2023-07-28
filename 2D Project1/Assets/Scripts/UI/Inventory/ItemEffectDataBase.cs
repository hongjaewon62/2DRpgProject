using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName;
    public string[] part;
    public int[] num;
}

[System.Serializable]
public class EquipEffect
{
    public string itemName;
    //public string part;
    public string[] stat;
    public int[] num;
}
public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    public EquipEffect[] equipEffects;

    [SerializeField]
    private Health thePlayerHealth;

    [SerializeField]
    private SlotToolTip slotToolTip;

    //[SerializeField]
    //private EquipmentSlot equipmentSlot;

    [SerializeField]
    private Inventory inventory;

    private const string HP = "HP", DEF = "DEF", DAMAGE = "DAMAGE";
    //private const string Helmet = "Helmet", Chest = "Chest", Pants = "Pants", Boots = "Boots", Weapon = "Weapon";

    public void UseItem(Item item)
    {
        if (item.itemType == Item.ItemType.Equipment)
        {
            if(item.equipmentType != Item.EquipmentType.NotEquipment)
            {
                inventory.Equiped(item);
                
                Debug.Log(item.itemName + " 을 장착했습니다.");
                //for (int i = 0; i < equipEffects.Length; i++)
                //{
                //    if (equipEffects[i].itemName == item.itemName)
                //    {
                //        for (int j = 0; j < equipEffects[i].stat.Length; j++)
                //        {
                //            inventory.Equiped(item);
                //            Debug.Log(item.itemName + " 을 장착했습니다.");
                //        }
                //        return;
                //    }
                //}
                //Debug.Log("ItemEffectDataBase에 일치하는 itemName 없습니다");
            }
        }
        else if (item.itemType == Item.ItemType.Consumables)
        {
            for(int i = 0; i < itemEffects.Length; i++)
            {
                if(itemEffects[i].itemName == item.itemName)
                {
                    for(int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch(itemEffects[i].part[j])
                        {
                            case HP:
                                thePlayerHealth.PlayerRecoveryHp(itemEffects[i].num[j]);
                                break;
                            case DEF:
                                break;
                            case DAMAGE:
                                break;
                            default:
                                Debug.Log("잘못된 Status");
                                break;
                        }
                        Debug.Log(item.itemName + " 을 사용했습니다.");
                    }
                    return;
                }
            }
            Debug.Log("ItemEffectDataBase에 일치하는 itemName 없습니다");
        }
    }

    public void StatTxt(Item item)
    {
        for (int i = 0; i < equipEffects.Length; i++)
        {
            if (equipEffects[i].itemName == item.itemName)
            {
                for (int j = 0; j < equipEffects[i].stat.Length; j++)
                {
                    switch (equipEffects[i].stat[j])
                    {
                        case HP:
                            slotToolTip.StatHpTxt(equipEffects[i].num[j]);
                            break;
                        case DEF:
                            slotToolTip.StatDefTxt(equipEffects[i].num[j]);
                            break;
                        case DAMAGE:
                            slotToolTip.StatDemageTxt(equipEffects[i].num[j]);
                            break;
                        default:
                            Debug.Log("잘못된 Status");
                            break;
                    }
                }
                return;
            }
        }
    }

    public void ShowToolTip(Item item, Vector2 pos)
    {
        slotToolTip.ShowToolTip(item, pos);
    }

    public void HideToolTip()
    {
        slotToolTip.HideToolTip();
    }
}
