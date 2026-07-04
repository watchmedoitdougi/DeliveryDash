using UnityEngine;

public class PhoneMenu : MonoBehaviour
{
    public enum PhoneScreen
    {
        Home,
        Chat,
        Delivery,
        Police,
        Settings
    }

    [Header("Phone")]
    [SerializeField] GameObject phoneMenu;
    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject chatMenu;
    [SerializeField] GameObject deliveryMenu;
    [SerializeField] GameObject policeMenu;
    [SerializeField] GameObject settingsMenu;

    [Header("Cursor")]
    [SerializeField] RectTransform cursor;
    [SerializeField] RectTransform[] buttons;

    bool isPaused;
    int currentSelection = 0;

    PhoneScreen currentScreen = PhoneScreen.Home;

    void Start()
    {
        phoneMenu.SetActive(false);

        ShowHomeScreen();
    }

    void Update()
    {
        // Open / Close Phone
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePhone();
        }

        if (!isPaused)
            return;

        // Return to Home Screen from any app
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ShowHomeScreen();
            MoveCursor();
            return;
        }

        // Only allow home screen navigation while on the home screen
        if (currentScreen != PhoneScreen.Home)
            return;

        // Cursor Movement
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

        // Open an app
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentSelection)
            {
                case 0:
                    OpenChatMenu();
                    break;

                case 1:
                    OpenDeliveryMenu();
                    break;

                case 2:
                    OpenPoliceMenu();
                    break;

                case 3:
                    OpenSettingsMenu();
                    break;
            }
        }
    }

    void TogglePhone()
    {
        isPaused = !isPaused;

        phoneMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            ShowHomeScreen();
            MoveCursor();
        }
    }

    void ShowHomeScreen()
    {
        homeScreen.SetActive(true);
        chatMenu.SetActive(false);
        deliveryMenu.SetActive(false);
        policeMenu.SetActive(false);
        settingsMenu.SetActive(false);

        currentScreen = PhoneScreen.Home;
    }

    void OpenChatMenu()
    {
        homeScreen.SetActive(false);
        chatMenu.SetActive(true);
        deliveryMenu.SetActive(false);
        policeMenu.SetActive(false);
        settingsMenu.SetActive(false);

        currentScreen = PhoneScreen.Chat;
    }

    void OpenDeliveryMenu()
    {
        homeScreen.SetActive(false);
        chatMenu.SetActive(false);
        deliveryMenu.SetActive(true);
        policeMenu.SetActive(false);
        settingsMenu.SetActive(false);

        currentScreen = PhoneScreen.Delivery;
    }

    void OpenPoliceMenu()
    {
        homeScreen.SetActive(false);
        chatMenu.SetActive(false);
        deliveryMenu.SetActive(false);
        policeMenu.SetActive(true);
        settingsMenu.SetActive(false);

        currentScreen = PhoneScreen.Police;
    }

    void OpenSettingsMenu()
    {
        homeScreen.SetActive(false);
        chatMenu.SetActive(false);
        deliveryMenu.SetActive(false);
        policeMenu.SetActive(false);
        settingsMenu.SetActive(true);

        currentScreen = PhoneScreen.Settings;
    }

    void MoveCursor()
    {
        cursor.position = buttons[currentSelection].position;
    }
}