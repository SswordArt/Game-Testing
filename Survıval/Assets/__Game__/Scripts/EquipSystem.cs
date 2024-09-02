using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;
    public List<GameObject> quickSlotsList = new List<GameObject>();
    public GameObject numbersHolder;
    public int selectedNumber = -1;
    private GameObject selectedItem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PopulateSlotList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectQuickSlot(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectQuickSlot(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectQuickSlot(3); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { SelectQuickSlot(4); }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { SelectQuickSlot(5); }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) { SelectQuickSlot(6); }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) { SelectQuickSlot(7); }
    }

    #region [SlotSelectionCodes]
    public void SelectQuickSlot(int number)
    {
        if (checkIfSlotIsFull(number))
        {
            if (selectedNumber != number)
            {
                selectedNumber = number;

                // Unselect Previously selected item
                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                }

                selectedItem = getSelectedItem(number);

                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = true;

                    // Color Changes
                    UpdateSlotColors(number);
                }
                else
                {
                    Debug.LogWarning("Selected item is null after selecting slot number: " + number);
                }
            }
            else // Trying to select the same slot
            {
                selectedNumber = -1; // No number selected

                // Unselect Previously selected item
                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                // Color Changes
                UpdateSlotColors(-1);
            }
        }
    }

    private void UpdateSlotColors(int activeNumber)
    {
        // Reset all slots to gray
        foreach (Transform child in numbersHolder.transform)
        {
            Text slotText = child.GetComponentInChildren<Text>();
            if (slotText != null)
            {
                slotText.color = Color.gray;
            }
        }

        // Set the selected slot to white
        if (activeNumber >= 1 && activeNumber <= 7)
        {
            Transform slotTransform = numbersHolder.transform.Find("number" + activeNumber);
            if (slotTransform != null)
            {
                Text slotText = slotTransform.GetComponentInChildren<Text>();
                if (slotText != null)
                {
                    slotText.color = Color.white;
                }
            }
        }
    }

    GameObject getSelectedItem(int slotNumber)
    {
        if (quickSlotsList[slotNumber - 1].transform.childCount > 0)
        {
            return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
        }
        return null;
    }

    public bool checkIfSlotIsFull(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.childCount > 0;
    }
    #endregion

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);

        InventorySystem.Instance.ReCalculateList();
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        return counter == 7;
    }
}
