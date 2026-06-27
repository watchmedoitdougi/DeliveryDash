using UnityEngine;

public class PhoneMenu : MonoBehaviour
{
    [Header("Phone")]
    [SerializeField] GameObject phoneMenu;

    [Header("Cursor")]
    [SerializeField] RectTransform cursor;
    [SerializeField] RectTransform[] buttons;

    bool isPaused;

    int currentSelection = 0;

    void Start()
    {
        phoneMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePhone();
        }

        // Don't let the cursor move unless the phone is open.
        if (!isPaused) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentSelection == 0) currentSelection = 1;
            else if (currentSelection == 2) currentSelection = 3;

            MoveCursor();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentSelection == 1) currentSelection = 0;
            else if (currentSelection == 3) currentSelection = 2;

            MoveCursor();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentSelection == 0) currentSelection = 2;
            else if (currentSelection == 1) currentSelection = 3;

            MoveCursor();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentSelection == 2) currentSelection = 0;
            else if (currentSelection == 3) currentSelection = 1;

            MoveCursor();
        }
    }

    void TogglePhone()
    {
        isPaused = !isPaused;

        phoneMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            MoveCursor();
        }
    }

    void MoveCursor()
    {
        cursor.position = buttons[currentSelection].position;
    }
}