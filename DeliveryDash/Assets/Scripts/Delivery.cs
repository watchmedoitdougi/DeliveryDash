using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPizzaColor = new Color32 (1, 1, 1, 1);
    [SerializeField] Color32 noPizzaColor = new Color32 (1, 1, 1, 1);

    [SerializeField] float destroyDelay = 0.5f;
    bool Pizza;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pizza" && !hasPizza)
        {
            Debug.Log("You picked up the Pizza!");
            hasPizza = true;
            spriteRenderer.color = hasPizzaColor;
            Destroy(other.gameObject, destroyDelay);
        }

        if (other.tag == "Customer" && hasPizza)
        {
            Debug.Log("You delivered the Pizza!");
            hasPizza = false;
            spriteRenderer.color = noPizzaColor;
        }

    }
}
