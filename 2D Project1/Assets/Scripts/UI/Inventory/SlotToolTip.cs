using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject toolTipBase;

    [SerializeField]
    private TextMeshProUGUI itemNameTxt;
    [SerializeField]
    private TextMeshProUGUI itemDescTxt;
    [SerializeField]
    private TextMeshProUGUI itemStatTxt;
    [SerializeField]
    private TextMeshProUGUI itemUseTxt;

    private int damage = 0;
    private int health = 0;
    private int defense = 0;

    public void ShowToolTip(Item item, Vector2 pos)
    {
        toolTipBase.SetActive(true);

        if (pos.x >= 1305)
        {
            if(pos.y > 390)
            {
                pos += new Vector2(-toolTipBase.GetComponent<RectTransform>().rect.width * 0.5f, -toolTipBase.GetComponent<RectTransform>().rect.height * 0.7f);
            }
            else
            {
                pos += new Vector2(-toolTipBase.GetComponent<RectTransform>().rect.width * 0.5f, toolTipBase.GetComponent<RectTransform>().rect.height * 0.7f);
            }
        }
        else if (pos.y <= 339)
        {
            pos += new Vector2(toolTipBase.GetComponent<RectTransform>().rect.width * 0.5f, toolTipBase.GetComponent<RectTransform>().rect.height * 0.7f);
        }
        else
        {
            pos += new Vector2(toolTipBase.GetComponent<RectTransform>().rect.width * 0.5f, -toolTipBase.GetComponent<RectTransform>().rect.height * 0.7f);
        }

        toolTipBase.transform.position = pos;

        itemNameTxt.text = item.itemName;
        itemDescTxt.text = item.itemDesc;

        if(item.equipmentType != Item.EquipmentType.NotEquipment)
        {
            if(item.equipmentType == Item.EquipmentType.Weapon)
            {
                itemStatTxt.text = "DAMAGE : " + damage;
            }
            else
            {
                itemStatTxt.text = "HP : " + health + "\n" + "DEF : " + defense;
            }

        }
        else
        {
            itemStatTxt.text = "";
        }

        if(item.itemType == Item.ItemType.Equipment)
        {
            itemUseTxt.text = "Right Click - Equip";
        }
        else if(item.itemType == Item.ItemType.Consumables)
        {
            itemUseTxt.text = "Right Click - Use";
        }
        else
        {
            itemUseTxt.text = "";
        }
    }

    public void HideToolTip()
    {
        toolTipBase.SetActive(false);
    }

    public void StatHpTxt(int healthTxt = 0)
    {
        health = healthTxt;
    }

    public void StatDemageTxt(int damageTxt = 0)
    {
        damage = damageTxt;
    }
    public void StatDefTxt(int defenseTxt = 0)
    {
        defense = defenseTxt;
    }

}
