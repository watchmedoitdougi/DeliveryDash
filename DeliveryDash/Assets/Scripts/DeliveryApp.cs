using TMPro;
using UnityEngine;

public class DeliveryApp : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text[] rows;
    [SerializeField] RectTransform cursor;
    [SerializeField] RectTransform[] cursorPoints;

    [Header("Managers")]
    [SerializeField] DeliveryManager deliveryManager;
    [SerializeField] PhoneMenu phoneMenu;

    // Data
    DeliveryData[] deliveries;

    // State
    int selectedIndex;
    int topIndex;

    const int visibleRows = 7;

    bool ignoreInput;
    void Start()
    {
        deliveries = deliveryManager.GetDeliveries();
    }
    void OnEnable()
    {
        deliveries = deliveryManager.GetDeliveries();

        topIndex = 0;

        selectedIndex = 0;

        while (selectedIndex < deliveries.Length &&
               deliveries[selectedIndex].isHeader)
        {
            selectedIndex++;
        }

        ignoreInput = true;

        Refresh();
    }
    void Update()
    {
        if (ignoreInput)
        {
            // Wait until Enter has been released.
            if (Input.GetKeyUp(KeyCode.Return))
            {
                ignoreInput = false;
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectDelivery();
        }
    }

    void MoveUp()
    {
        int next = selectedIndex - 1;

        while (next >= 0)
        {
            if (!deliveries[next].isHeader)
            {
                selectedIndex = next;
                break;
            }

            next--;
        }

        ClampScroll();
        Refresh();
    }

    void MoveDown()
    {
        int next = selectedIndex + 1;

        while (next < deliveries.Length)
        {
            if (!deliveries[next].isHeader)
            {
                selectedIndex = next;
                break;
            }

            next++;
        }

        ClampScroll();
        Refresh();
    }

    void SelectDelivery()
    {
        DeliveryData delivery = deliveries[selectedIndex];

        if (delivery.completed)
            return;

        if (!delivery.unlocked)
            return;

        if (deliveryManager.HasActiveDelivery())
            return;

        if (deliveryManager.StartDelivery(selectedIndex))
        {
            phoneMenu.ClosePhone();
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < visibleRows; i++)
        {
            int index = topIndex + i;

            if (index >= deliveries.Length)
            {
                rows[i].text = "";
                continue;
            }

            DeliveryData delivery = deliveries[index];

            if (delivery.isHeader)
            {
                rows[i].text = delivery.displayName;
                rows[i].fontStyle = FontStyles.Bold;
                rows[i].color = Color.yellow;
            }
            else if (delivery.completed)
            {
                rows[i].text = "<s>" + delivery.displayName + "</s>";
                rows[i].fontStyle = FontStyles.Normal;
                rows[i].color = Color.gray;
            }
            else if (!delivery.unlocked)
            {
                rows[i].text = "???";
                rows[i].fontStyle = FontStyles.Normal;
                rows[i].color = Color.gray;
            }
            else
            {
                rows[i].text = delivery.displayName;
                rows[i].fontStyle =
                    (index == selectedIndex)
                    ? FontStyles.Bold
                    : FontStyles.Normal;
                rows[i].color = Color.white;
            }
        }

        MoveCursor();
    }

    void MoveCursor()
    {
        int row = selectedIndex - topIndex;

        if (row >= 0 && row < cursorPoints.Length)
        {
            cursor.position = cursorPoints[row].position;
        }
    }

    void ClampScroll()
    {
        // Scroll down
        while (selectedIndex >= topIndex + visibleRows)
        {
            topIndex++;
        }

        // Scroll up
        while (selectedIndex < topIndex + 1)
        {
            topIndex--;
        }

        if (topIndex < 0)
            topIndex = 0;

        // If the row above the top is a header,
        // include it on screen.
        if (topIndex > 0 && deliveries[topIndex - 1].isHeader)
        {
            topIndex--;
        }
    }

}