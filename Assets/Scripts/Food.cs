using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    // public float FoodAttack = f;
    // public float FoodFlee= 0.5f;
    public float FoodSpeed= 1f;
    public GameObject foodcube;
    private void OnTriggerEnter(Collider foodcube)
    {
            GetComponent<EntityMovement>().speed += FoodSpeed;
            Debug.Log("Mange");
    }
}
