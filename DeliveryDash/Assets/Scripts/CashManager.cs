using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    [SerializeField] TMP_Text cashText;

    int cash = 0;

    void Start()
    {
        UpdateCashUI();
    }

    public void AddCash(int amount)
    {
        cash += amount;
        UpdateCashUI();
    }

    void UpdateCashUI()
    {
        cashText.text = "$" + cash;
    }
}