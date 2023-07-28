using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            ItemPickUp pickUp = collision.gameObject.GetComponent<ItemPickUp>();

            Debug.Log(pickUp.transform.GetComponent<ItemPickUp>().item.itemName + "∏¶(¿ª) »πµÊ«ﬂΩ¿¥œ¥Ÿ");
            theInventory.AcquireItem(pickUp.transform.GetComponent<ItemPickUp>().item);
            Destroy(pickUp.transform.gameObject);
        }
    }
}
