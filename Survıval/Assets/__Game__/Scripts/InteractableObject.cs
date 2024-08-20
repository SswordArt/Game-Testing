using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public bool CanTake;

    public bool PlayerInRange;

    public string ItemName;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PlayerInRange && SelectionManager.Instance.onTarget && CanTake == true)
        {
            Debug.Log("Item Added to ýnv");
            Destroy(gameObject);

        }

    }



    public string GetItemName()
    {
        return ItemName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
