using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] DeliveryData[] deliveries;

    int currentDelivery = -1;

    void Start()
    {
        if (deliveries.Length > 0)
        {
            deliveries[0].unlocked = true;
        }
    }

    public DeliveryData[] GetDeliveries()
    {
        return deliveries;
    }

    public bool HasActiveDelivery()
    {
        return currentDelivery != -1;
    }

    public DeliveryData GetCurrentDelivery()
    {
        if (currentDelivery < 0)
            return null;

        return deliveries[currentDelivery];
    }

    public bool StartDelivery(int index)
    {
        // Invalid index
        if (index < 0 || index >= deliveries.Length)
            return false;

        // Already delivering something
        if (currentDelivery != -1)
            return false;

        DeliveryData delivery = deliveries[index];

        // Can't start locked or completed deliveries
        if (!delivery.unlocked || delivery.completed)
            return false;

        currentDelivery = index;

        if (delivery.pizzaPickup != null)
            delivery.pizzaPickup.SetActive(true);

        if (delivery.customer != null)
            delivery.customer.SetActive(true);

        Debug.Log("Started delivery: " + delivery.deliveryName);

        return true;
    }

    public void CompleteCurrentDelivery()
    {
        if (currentDelivery == -1)
            return;

        DeliveryData delivery = deliveries[currentDelivery];

        delivery.completed = true;

        if (delivery.pizzaPickup != null)
            delivery.pizzaPickup.SetActive(false);

        if (delivery.customer != null)
            delivery.customer.SetActive(false);

        UnlockNextDelivery();

        Debug.Log("Completed delivery: " + delivery.deliveryName);

        currentDelivery = -1;
    }

    void UnlockNextDelivery()
    {
        for (int i = 0; i < deliveries.Length; i++)
        {
            if (!deliveries[i].unlocked)
            {
                deliveries[i].unlocked = true;
                break;
            }
        }
    }
}