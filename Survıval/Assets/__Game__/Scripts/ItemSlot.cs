using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemSlot : MonoBehaviour, IDropHandler
{

    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }






    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        //if there is not item already then set our item.
        if (!Item)
        {

            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);

            if (transform.CompareTag("QuickSlot") == false) 
            {
                DragDrop.itemBeingDragged.GetComponent<InventoryItem>().isInsideQuickSlot = false;
                InventorySystem.Instance.ReCalculateList();
            }

            if (transform.CompareTag("QuickSlot"))
            {
                var inventoryItem = DragDrop.itemBeingDragged.GetComponent<InventoryItem>();
                inventoryItem.isInsideQuickSlot = true;
                inventoryItem.isSelected = true;  // Burada isSelected'ı da güncelleyebilirsin

                Debug.Log("Item QuickSlot'a eklendi: " + inventoryItem.name + ", isInsideQuickSlot: " + inventoryItem.isInsideQuickSlot + ", isSelected: " + inventoryItem.isSelected);

                InventorySystem.Instance.ReCalculateList();
            }

        }


    }




}