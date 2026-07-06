using TMPro;
using UnityEngine;

public class DeliveryMenu : MonoBehaviour
{
    [Header("Delivery List")]
    [SerializeField] TMP_Text[] deliveries;

    [Header("Cursor")]
    [SerializeField] RectTransform cursor;

    int currentDelivery = 0;

    void OnEnable()
    {
        // Reset to the first delivery every time the menu opens.
        currentDelivery = 0;
        MoveCursor();
    }

    void Update()
    {
        // Ignore input if this menu isn't visible.
        if (!gameObject.activeInHierarchy)
            return;

        // Move Down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentDelivery < deliveries.Length - 1)
            {
                currentDelivery++;
                MoveCursor();
            }
        }

        // Move Up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentDelivery > 0)
            {
                currentDelivery--;
                MoveCursor();
            }
        }

        // Select a delivery
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Selected: " + deliveries[currentDelivery].text);
        }
    }

    void MoveCursor()
    {
        Vector3 offset = new Vector3(-40f, 0f, 0f);
        cursor.position = deliveries[currentDelivery].transform.position + offset;
    }
}