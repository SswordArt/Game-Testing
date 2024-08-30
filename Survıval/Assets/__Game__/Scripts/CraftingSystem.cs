using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public GameObject CraftingScreenUI;
    public GameObject ToolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    //Category Buttons
    Button toolsBTN;

    //Craft Buttons
    Button craftAxeBTN;

    //Req TEXT
    Text AxeReq1, AxeReq2;

     public bool isOpen;

    //All Blueprint
    public Blueprint AxeBLP = new Blueprint("Axe", 2, "Stone", 3, "Stick", 3);





    public static CraftingSystem Instance { get; set; }

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

        toolsBTN = CraftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        //AXE

        AxeReq1 = ToolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = ToolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBTN = ToolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });
        

    }

    void CraftAnyItem(Blueprint bluePrintToCraft)
    {
        //add item into inv
        InventorySystem.Instance.AddToInventory(bluePrintToCraft.itemName);

        //remove rss from inv

        if (bluePrintToCraft.NumOfReq == 1)
        {
            InventorySystem.Instance.RemoveItem(bluePrintToCraft.Req1, bluePrintToCraft.Req1amount);
        }   
        else if (bluePrintToCraft.NumOfReq == 2)
        {   
            InventorySystem.Instance.RemoveItem(bluePrintToCraft.Req1, bluePrintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(bluePrintToCraft.Req2, bluePrintToCraft.Req2amount);
        }



        //refresh list
        StartCoroutine(calculate());


    }

    public IEnumerator calculate()
    {
        yield return 0; 
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();

    }

    void OpenToolsCategory()
    {
        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(true);


    }
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {

            Debug.Log("C is pressed");
            CraftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;


            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            CraftingScreenUI.SetActive(false);
            ToolsScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;

            }

            isOpen = false;
        }
    }


    public void RefreshNeededItems()
    {

        int stone_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.Instance.ItemList;

        foreach (string itemName in inventoryItemList)
        {

            switch (itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;

                case "Stick":
                    stick_count += 1;
                    break;

            }

        }

        // FOR AXE //

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
            AxeReq1.color = Color.white;
            AxeReq2.color = Color.white;
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);

            // Koþullara göre renk ayarý yapýlýr
            AxeReq1.color = stone_count >= 3 ? Color.white : Color.red;
            AxeReq2.color = stick_count >= 3 ? Color.white : Color.red;
        }





    }


}
