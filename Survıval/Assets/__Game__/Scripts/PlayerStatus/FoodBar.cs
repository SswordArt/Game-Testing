using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    public Slider slider;
    public Text FoodCounter;

    public GameObject playerState;

    private float CurrentFood, MaxFood;

    void Awake()
    {

        slider.GetComponent<Slider>();

    }


    void Update()
    {
        CurrentFood = playerState.GetComponent<PlayerStatus>().CurrentFood;
        MaxFood = playerState.GetComponent<PlayerStatus>().MaxFood;

        float fillValue = CurrentFood / MaxFood; // 0 - 1
        slider.value = fillValue;


        FoodCounter.text = CurrentFood + "/" + MaxFood; // 80/100

    }
}
