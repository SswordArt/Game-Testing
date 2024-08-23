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
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(); });
        

    }

    void CraftAnyItem()
    {
        //add item into inv


        //remove rss from inv
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
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            CraftingScreenUI.SetActive(false);
            ToolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }



}
