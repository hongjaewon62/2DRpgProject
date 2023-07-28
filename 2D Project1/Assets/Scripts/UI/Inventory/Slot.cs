using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    private ItemEffectDataBase itemEffectDataBase;

    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private TextMeshProUGUI text_Count;

    [SerializeField]
    private TextMeshProUGUI text_equipped;

    private bool equipmentActivated = false;

    private void Start()
    {
        itemEffectDataBase = FindObjectOfType<ItemEffectDataBase>();
    }

    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
    public void AddItem(Item addItem, int count = 1)
    {
        item = addItem;
        itemCount = count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            text_Count.gameObject.SetActive(true);
            text_Count.text = itemCount.ToString();
            //equipmentActivated = false;
            //EquipTextActive();
        }
        else
        {
            text_Count.text = "0";
            text_Count.gameObject.SetActive(false);
            if (equipmentActivated)
            {
                Debug.Log("add");
                EquipTextActive();
            }
        }

        SetColor(1);
    }

    public void SetSlotCount(int count)
    {
        itemCount += count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        text_Count.gameObject.SetActive(false);
        equipmentActivated = false;
        EquipTextActive();
    }

    private void EquipTextActive()
    {
        if(equipmentActivated)
        {
            text_equipped.gameObject.SetActive(equipmentActivated);
        }
        else
        {
            text_equipped.gameObject.SetActive(equipmentActivated);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                // ¼Ò¸ð
                if(item.itemType == Item.ItemType.Equipment)
                {
                    itemEffectDataBase.UseItem(item);
                    equipmentActivated = !equipmentActivated;
                    EquipTextActive();
                }
                else
                {
                    itemEffectDataBase.UseItem(item);
                }
                if(item.itemType == Item.ItemType.Consumables)
                {
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item tempItem = item;
        int tempItemCount = itemCount;
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(tempItem, tempItemCount);
            if (DragSlot.instance.dragSlot.equipmentActivated == false && this.equipmentActivated == true)
            {
                DragSlot.instance.dragSlot.equipmentActivated = true;
                this.equipmentActivated = false;
                DragSlot.instance.dragSlot.EquipTextActive();
                this.EquipTextActive();
            }
            else if (DragSlot.instance.dragSlot.equipmentActivated == true && this.equipmentActivated == false)
            {
                DragSlot.instance.dragSlot.equipmentActivated = false;
                this.equipmentActivated = true;
                DragSlot.instance.dragSlot.EquipTextActive();
                this.EquipTextActive();
            }
        }
        else
        {
            if (DragSlot.instance.dragSlot.equipmentActivated == true && this.equipmentActivated == false)
            {
                this.equipmentActivated = true;
                this.EquipTextActive();
            }
            DragSlot.instance.dragSlot.ClearSlot();
        }
        itemEffectDataBase.StatTxt(item);
        itemEffectDataBase.ShowToolTip(item, transform.position);
    }

    // ToolTip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            itemEffectDataBase.StatTxt(item);
            itemEffectDataBase.ShowToolTip(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemEffectDataBase.HideToolTip();
    }
}
