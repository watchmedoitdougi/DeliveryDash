using UnityEngine;

[System.Serializable]
public class DeliveryData
{
    [Header("Display")]
    public bool isHeader;

    public string displayName;

    [Header("Delivery")]
    public GameObject pizzaPickup;
    public GameObject customer;

    public float timeLimit = 45f;

    [HideInInspector]
    public bool unlocked;

    [HideInInspector]
    public bool completed;
}