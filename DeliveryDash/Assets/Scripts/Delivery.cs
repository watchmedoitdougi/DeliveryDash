using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 topperOnColor = new Color32(255, 255, 0, 255);
    [SerializeField] Color32 topperOffColor = new Color32(255, 255, 255, 255);

    [SerializeField] float destroyDelay = 0.5f;

    bool hasPizza;

    [SerializeField] SpriteRenderer topperRenderer;
    [SerializeField] GameObject pizzaPickupParticles;
    [SerializeField] GameObject cashPickupParticles;
    [SerializeField] DeliveryTimer deliveryManager;

    void Start()
    {
        topperRenderer.color = topperOffColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pizza" && !hasPizza)
        {
            Debug.Log("You picked up the Pizza!");
            hasPizza = true;
            deliveryManager.StartDelivery();

            topperRenderer.color = topperOnColor;

            Instantiate(
                pizzaPickupParticles,
                other.transform.position,
                Quaternion.identity
            );

            Destroy(other.gameObject, destroyDelay);
        }

        if (other.tag == "Customer" && hasPizza)
        {
            Debug.Log("You delivered the Pizza!");
            hasPizza = false;

            deliveryManager.CompleteDelivery();

            Instantiate(
               cashPickupParticles,
               other.transform.position,
               Quaternion.identity
           );

            Destroy(other.gameObject, destroyDelay);

            topperRenderer.color = topperOffColor;
        }
    }
}