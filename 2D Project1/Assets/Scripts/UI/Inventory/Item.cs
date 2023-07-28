using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDesc;
    public ItemType itemType;
    public EquipmentType equipmentType;
    public Sprite itemImage;
    public GameObject itemPrefab;

    public enum ItemType
    {
        Equipment,
        Consumables,
        Ingredient,
        ETC
    }

    public enum EquipmentType
    {
        NotEquipment,
        Weapon,
        Helmet,
        Chest,
        Pants,
        Boots
    }
}
