using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public Slot dragSlot;

    [SerializeField]
    private Image imageItem;

    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image DragitemImage)
    {
        imageItem.sprite = DragitemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = imageItem.color;
        color.a = alpha;
        imageItem.color = color;
    }
}
