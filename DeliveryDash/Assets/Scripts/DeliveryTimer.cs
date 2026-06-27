using TMPro;
using UnityEngine;

public class DeliveryTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float deliveryTime = 30f;
    [SerializeField] CashManager cashManager;

    float currentTime;

    bool timerRunning;

    void Start()
    {
        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!timerRunning) return;

        currentTime -= Time.deltaTime;

        float displayTime = Mathf.Max(currentTime, 0);

        int minutes = Mathf.FloorToInt(displayTime / 60);
        int seconds = Mathf.CeilToInt(displayTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime > 15)
        {
            timerText.color = Color.green;
        }
        else if (currentTime > 10)
        {
            timerText.color = Color.yellow;
        }
        else if (currentTime > 5)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.Lerp(
                Color.red,
                Color.clear,
                Mathf.PingPong(Time.time * 4f, 1f)
            );
        }

        if (currentTime <= 0)
        {
            DeliveryFailed();
        }
    }

    public void StartDelivery()
    {
        currentTime = deliveryTime;
        timerRunning = true;

        timerText.gameObject.SetActive(true);
    }

    public void CompleteDelivery()
    {
        timerRunning = false;
        timerText.gameObject.SetActive(false);

        int reward = 10 + Mathf.RoundToInt(currentTime);

        cashManager.AddCash(reward);

        Debug.Log("Earned $" + reward);
    }

    void DeliveryFailed()
    {
        timerRunning = false;
        timerText.gameObject.SetActive(false);

        Debug.Log("Delivery Failed!");

        // Pizza will respawn at pickup spot
    }
}