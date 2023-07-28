using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public Image EquipmentImage;

    public string equipPart;

    public bool equipmentActivated = false;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private Health health;

    [SerializeField]
    private ItemEffectDataBase itemDatabase;
    //public bool equipChest = false;
    //public bool equipPants = false;
    //public bool equipBoots = false;
    //public bool equipWeapont = false;

    private void SetColor(float alpha)
    {
        Color color = EquipmentImage.color;
        color.a = alpha;
        EquipmentImage.color = color;
    }

    public void EquipItem(Item equipItem)
    {
        Debug.Log("트루");
        equipmentActivated = !equipmentActivated;
        if(equipmentActivated)
        {
            item = equipItem;
            EquipmentImage.sprite = item.itemImage;
            SetColor(1);
            if(item.equipmentType != Item.EquipmentType.Weapon)
            {
                for (int i = 0; i < itemDatabase.equipEffects.Length; i++)
                {
                    if (itemDatabase.equipEffects[i].itemName == item.itemName)
                    {
                        health.PlayerIncreaseHp(itemDatabase.equipEffects[i].num[0]);
                        player.PlayerIncreaseDef(itemDatabase.equipEffects[i].num[1]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < itemDatabase.equipEffects.Length; i++)
                {
                    if (itemDatabase.equipEffects[i].itemName == item.itemName)
                    {
                        player.PlayerIncreaseAttackDamage(itemDatabase.equipEffects[i].num[0]);
                    }
                }
            }
        }
        else
        {
            if (item.equipmentType != Item.EquipmentType.Weapon)
            {
                for (int i = 0; i < itemDatabase.equipEffects.Length; i++)
                {
                    if (itemDatabase.equipEffects[i].itemName == item.itemName)
                    {
                        health.PlayerDecreaseHp(itemDatabase.equipEffects[i].num[0]);
                        player.PlayerDecreaseDef(itemDatabase.equipEffects[i].num[1]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < itemDatabase.equipEffects.Length; i++)
                {
                    if (itemDatabase.equipEffects[i].itemName == item.itemName)
                    {
                        player.PlayerDecreaseAttackDamage(itemDatabase.equipEffects[i].num[0]);
                    }
                }
            }
            ClearSlot();
            Debug.Log("클리어 슬롯");
        }
    }

    private void ClearSlot()
    {
        item = null;
        EquipmentImage.sprite = null;
        SetColor(0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (eventData.button == PointerEventData.InputButton.Right)
        //{
        //    ClearSlot();
        //    Debug.Log(equipmentActivated);
        //}
    }
}
