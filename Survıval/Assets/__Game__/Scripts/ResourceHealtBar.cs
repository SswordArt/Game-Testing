using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class ResourceHealtBar : MonoBehaviour
{
    
    private Slider slider;
    private float currentHealth, maxHealth;

    public GameObject globalState;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        currentHealth = globalState.GetComponent<GlobalState>().resourceHealth;
        maxHealth = globalState.GetComponent<GlobalState>().resourceMaxHealth;

        float fillValue = currentHealth / maxHealth; // 0 - 1
        slider.value = fillValue;
    }

}
