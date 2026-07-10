using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 topperOnColor = new Color32(255, 255, 0, 255);
    [SerializeField] Color32 topperOffColor = new Color32(255, 255, 255, 255);

  

    bool hasPizza;

    [SerializeField] SpriteRenderer topperRenderer;
    [SerializeField] GameObject pizzaPickupParticles;
    [SerializeField] GameObject cashPickupParticles;
    [SerializeField] Timer deliveryTimer;
    [SerializeField] DeliveryManager deliveryManager;

    void Start()
    {
        topperRenderer.color = topperOffColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pizza") && !hasPizza)
        {
            Debug.Log("You picked up the Pizza!");

            hasPizza = true;

            deliveryTimer.StartDelivery(45f);

            topperRenderer.color = topperOnColor;

            Instantiate(
                pizzaPickupParticles,
                other.transform.position,
                Quaternion.identity
            );

            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Customer") && hasPizza)
        {
            Debug.Log("You delivered the Pizza!");

            hasPizza = false;

            deliveryTimer.CompleteDelivery();
            deliveryManager.CompleteCurrentDelivery();

            Instantiate(
                cashPickupParticles,
                other.transform.position,
                Quaternion.identity
            );

            other.gameObject.SetActive(false);

            topperRenderer.color = topperOffColor;
        }
    }
}