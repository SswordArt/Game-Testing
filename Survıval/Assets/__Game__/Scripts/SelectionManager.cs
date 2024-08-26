using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance {  get; set; }

    public bool onTarget;

    public GameObject selectedObject;

    public GameObject interaction_Info_UI;
    Text interaction_text;


    public Image centerDotIcon;
    public Image HandIcon;
 
    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    void Awake()
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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();


            if (interactable && interactable.PlayerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);

                if (interactable.CompareTag("Pickable"))
                {

                    centerDotIcon.gameObject.SetActive(false);
                    HandIcon.gameObject.SetActive(true);


                }
                else
                {
                    centerDotIcon.gameObject.SetActive(true);
                    HandIcon.gameObject.SetActive(false);


                }


            }
            else 
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
                centerDotIcon.gameObject.SetActive(true);
                HandIcon.gameObject.SetActive(false);
            }

        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
            centerDotIcon.gameObject.SetActive(true);
            HandIcon.gameObject.SetActive(false);
        }
    }
}