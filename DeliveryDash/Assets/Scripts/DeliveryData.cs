using UnityEngine;

[System.Serializable]
public class DeliveryData
{
    [Header("Info")]
    public string deliveryName;
    public string pizzaShop;

    [Header("Scene Objects")]
    public GameObject pizzaPickup;
    public GameObject customer;

    [Header("Settings")]
    public float timeLimit = 45f;

    [HideInInspector]
    public bool unlocked;

    [HideInInspector]
    public bool completed;
}