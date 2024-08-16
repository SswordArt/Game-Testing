using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName;

    public void Interact()
    {
        Debug.Log("Interacting with " + itemName);
        // Buraya nesneyle ne yapmak istediðinizi ekleyebilirsiniz
    }
}