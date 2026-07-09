using TMPro;
using UnityEngine;

public class DeliveryMenu : MonoBehaviour
{
    [Header("Visible Deliveries")]
    [SerializeField] TMP_Text[] deliveries;

    [SerializeField] DeliveryManager deliveryManager;
    [SerializeField] PhoneMenu phoneMenu;

    [Header("Cursor")]
    [SerializeField] RectTransform cursor;

    [Header("Cursor Points")]
    [SerializeField] RectTransform[] cursorPoints;

    DeliveryData[] deliveries = deliveryManager.GetDeliveries();

    int selectedEntry;
    int topVisibleEntry;
    bool ignoreInput;

    void OnEnable()
    {
        ignoreInput = true;

        topVisibleEntry = 0;
        selectedEntry = 1;

        while (selectedEntry < menuEntries.Length &&
               menuEntries[selectedEntry].isHeader)
        {
            selectedEntry++;
        }

        RefreshMenu();
    }

    void Update()
    {
        if (ignoreInput)
        {
            if (!Input.GetKey(KeyCode.Return))
                ignoreInput = false;

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
            int index = menuEntries[selectedEntry].deliveryIndex;

            if (index >= 0)
            {
                DeliveryData delivery = deliveryManager.GetDeliveries()[index];

                if (!delivery.completed)
                {
                    deliveryManager.StartDelivery(index);

                    RefreshMenu();

                    phoneMenu.ClosePhone();
                }
            }
        }
    }

    void MoveDown()
    {
        if (selectedEntry >= menuEntries.Length - 1)
            return;

        do
        {
            selectedEntry++;
        }
        while (selectedEntry < menuEntries.Length &&
               menuEntries[selectedEntry].isHeader);

        ClampScroll();
        RefreshMenu();
    }

    void MoveUp()
    {
        if (selectedEntry <= 1)
            return;

        do
        {
            selectedEntry--;
        }
        while (selectedEntry > 0 &&
               menuEntries[selectedEntry].isHeader);

        ClampScroll();
        RefreshMenu();
    }

    public void RefreshMenu()
    {
        for (int i = 0; i < deliveries.Length; i++)
        {
            int entryIndex = topVisibleEntry + i;

            if (entryIndex >= menuEntries.Length)
            {
                deliveries[i].text = "";
                deliveries[i].fontStyle = FontStyles.Normal;
                deliveries[i].color = Color.white;
                continue;
            }

            DeliveryEntry entry = menuEntries[entryIndex];

            if (entry.isHeader)
            {
                deliveries[i].text = entry.text;
                deliveries[i].fontStyle = FontStyles.Bold;
                deliveries[i].color = Color.white;
            }
            else
            {
                DeliveryData delivery =
                    deliveryManager.GetDeliveries()[entry.deliveryIndex];

                if (delivery.completed)
                {
                    deliveries[i].text = "<s>" + entry.text + "</s>";
                    deliveries[i].fontStyle = FontStyles.Normal;
                    deliveries[i].color = Color.gray;
                }
                else
                {
                    deliveries[i].text = entry.text;
                    deliveries[i].fontStyle =
                        (entryIndex == selectedEntry)
                        ? FontStyles.Bold
                        : FontStyles.Normal;
                    deliveries[i].color = Color.white;
                }
            }
        }

        MoveCursor();
    }

    void MoveCursor()
    {
        int row = selectedEntry - topVisibleEntry;

        if (row >= 0 &&
            row < cursorPoints.Length)
        {
            cursor.position = cursorPoints[row].position;
        }
    }

    void ClampScroll()
    {
        while (selectedEntry - topVisibleEntry >= deliveries.Length)
            topVisibleEntry++;

        while (selectedEntry < topVisibleEntry)
            topVisibleEntry--;

        if (topVisibleEntry < 0)
            topVisibleEntry = 0;
    }
}