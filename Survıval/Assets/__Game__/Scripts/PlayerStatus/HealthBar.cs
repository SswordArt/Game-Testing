using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Text HealthCounter;

    public GameObject playerState;

    private float CurrentHealth, MaxHealth;

    void Awake()
    {

        slider.GetComponent<Slider>();

    }

    
    void Update()
    {
        CurrentHealth = playerState.GetComponent<PlayerStatus>().CurrentHealth;
        MaxHealth = playerState.GetComponent<PlayerStatus>().MaxHealth;

        float fillValue = CurrentHealth / MaxHealth; // 0 - 1
        slider.value = fillValue;


        HealthCounter.text = CurrentHealth + "/" + MaxHealth; // 80/100

    }
}
