using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public LayerMask interactableLayer;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        Interact();
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange, interactableLayer))
            {
                InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

                if (interactableObject != null)
                {
                    interactableObject.Interact();
                }
            }
        }
    }
}
