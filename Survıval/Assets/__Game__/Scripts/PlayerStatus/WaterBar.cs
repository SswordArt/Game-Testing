using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    public Slider slider;
    public Text HydrationCounter;

    public GameObject playerState;

    private float CurrentHydration, MaxHydration;

    void Awake()
    {

        slider.GetComponent<Slider>();

    }


    void Update()
    {
        CurrentHydration = playerState.GetComponent<PlayerStatus>().CurrentHydration;
        MaxHydration = playerState.GetComponent<PlayerStatus>().MaxHydration;

        float fillValue = CurrentHydration / MaxHydration; // 0 - 1
        slider.value = fillValue;


        HydrationCounter.text = CurrentHydration + "/" + MaxHydration; // 80/100

    }
}
