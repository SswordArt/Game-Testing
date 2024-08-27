using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> SlotList = new List<GameObject>();

    public List<string> ItemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    // public bool isFull;


    //PickUpAlert

    public GameObject pickupAlert;

    public Text pickupName;
    public Image pickupImage;

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


    void Start()
    {
        isOpen = false;
        

        PopulateSlotList();

    }

    private void PopulateSlotList()
    {

        foreach (Transform child in inventoryScreenUI.transform) 
        {
            if (child.CompareTag("Slot"))
            {
                SlotList.Add(child.gameObject);
            }  
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);

            if (!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {

            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            ItemList.Add(itemName);

            Sprite sprite = itemToAdd.GetComponent<Image>().sprite;


            TriggerPickUpAlert(itemName, sprite);

            ReCalculateList();
            CraftingSystem.Instance.RefreshNeededItems();

    }


    void TriggerPickUpAlert(string itemName, Sprite itemSprite)
    {

        pickupAlert.SetActive(true);

        pickupName.text = itemName;

        pickupImage.sprite = itemSprite;

        StartCoroutine(HidePickupAlertAfterDelay(2f));
    }

    private IEnumerator HidePickupAlertAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);


        pickupAlert.SetActive(false);

    }


    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = SlotList.Count - 1; i >= 0; i--)
        {

            if (SlotList[i].transform.childCount > 0)
            {

                if (SlotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {

                    Destroy(SlotList[i].transform.GetChild(0).gameObject);
                    counter -= 1;

                }
            }

        }


        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in SlotList)
        {

            if (slot.transform.childCount == 0)
            {

                return slot;

            } 
        }

        return new GameObject();

    }



    public bool checkIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }

        }
        if (counter == 21)
        {

            return true;

        }
        else
        {

            return false;

        }
    }

    public void ReCalculateList()
    {

        ItemList.Clear();

        foreach (GameObject slot in SlotList)
        {

            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";

                string result = name.Replace(str2, "");


                ItemList.Add(result);
            }
        }
    }



}