using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    //--PLAYER HEALTH-- // 
    public float CurrentHealth;
    public float MaxHealth;

    //--PLAYER HUNGER--//
    public float CurrentFood;
    public float MaxFood;

    private float distanceTravelled = 0;
    private Vector3 lastPosition;

    public GameObject playerBody;

    //--PLAYER WATER--//
    public float CurrentHydration;
    public float MaxHydration;

    public bool isHydrationActive;

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

    private void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentFood = MaxFood;
        CurrentHydration = MaxHydration;
        lastPosition = playerBody.transform.position;

        StartCoroutine(DeacreaseWater());
    }

    IEnumerator DeacreaseWater()
    {
        while (true) 
        {

            CurrentHydration -= 1;
            yield return new WaitForSeconds(10);

            
            
        }

    }


    void Update()
    {
        Vector3 currentPosition = playerBody.transform.position;

        // Only consider horizontal movement (X and Z axes)
        Vector2 lastPosition2D = new Vector2(lastPosition.x, lastPosition.z);
        Vector2 currentPosition2D = new Vector2(currentPosition.x, currentPosition.z);

        // Calculate distance in the horizontal plane
        float distance = Vector2.Distance(currentPosition2D, lastPosition2D);

        // Update distanceTravelled if there is a significant movement
        if (distance > 0.01f)
        {
            distanceTravelled += distance;
            lastPosition = currentPosition;
        }

        // Decrease food based on distance travelled
        if (distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            CurrentFood -= 1;
        }
    }
}
