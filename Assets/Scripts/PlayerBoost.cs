using System;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    private float FoodSpeed = 5f;
    public float SpeedDuration = 3f;
    private float timer = 0f;
    private bool isBoosted = false;

    private EntityMovement movement;

    void Awake()
    {
        movement = GetComponent<EntityMovement>();
    }

    void Update()
    {
        if (isBoosted)
        {
            timer += Time.deltaTime;

            if (timer >= SpeedDuration)
            {
                movement.speed -= FoodSpeed;
                timer = 0f;
                isBoosted = false;
                Debug.Log("Boost terminé");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (!isBoosted)
            {
                movement.speed += FoodSpeed;
                isBoosted = true;
                timer = 0f;
            }

            Debug.Log("Mange !");
            Destroy(other.gameObject);
        }
    }
}
