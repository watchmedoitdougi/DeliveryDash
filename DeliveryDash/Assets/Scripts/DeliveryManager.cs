using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private DeliveryData[] deliveries;

    private int currentDelivery = -1;

    void Awake()
    {
        for (int i = 0; i < deliveries.Length; i++)
        {
            if (!deliveries[i].isHeader)
            {
                deliveries[i].unlocked = true;
                break;
            }
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
        if (currentDelivery == -1)
            return null;

        return deliveries[currentDelivery];
    }

    public bool StartDelivery(int index)
    {
        if (HasActiveDelivery())
        { 
            Debug.Log("Already on a delivery.");
            return false;
        }

        if (index < 0 || index >= deliveries.Length)
            return false;

        DeliveryData delivery = deliveries[index];

        if (delivery.isHeader)
            return false;

        if (!delivery.unlocked)
            return false;

        if (delivery.completed)
            return false;

        currentDelivery = index;

        if (delivery.pizzaPickup != null)
            delivery.pizzaPickup.SetActive(true);

        if (delivery.customer != null)
            delivery.customer.SetActive(true);

        Debug.Log("Started " + delivery.displayName);

        return true;
    }

    public void CompleteCurrentDelivery()
    {
        if (!HasActiveDelivery())
            return;

        DeliveryData delivery = deliveries[currentDelivery];

        delivery.completed = true;

        if (delivery.pizzaPickup != null)
            delivery.pizzaPickup.SetActive(false);

        if (delivery.customer != null)
            delivery.customer.SetActive(false);

        UnlockNextDelivery();

        Debug.Log("Completed " + delivery.displayName);

        currentDelivery = -1;

        DeliveryApp app = FindFirstObjectByType<DeliveryApp>();

        if (app != null)
        {
            app.Refresh();
        }
    }

    void UnlockNextDelivery()
    {
        for (int i = 0; i < deliveries.Length; i++)
        {
            DeliveryData delivery = deliveries[i];

            if (!delivery.isHeader &&
                !delivery.unlocked &&
                !delivery.completed)
            {
                delivery.unlocked = true;
                break;
            }
        }
    }
}