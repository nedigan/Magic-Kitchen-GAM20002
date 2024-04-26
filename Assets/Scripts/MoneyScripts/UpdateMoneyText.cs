using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateMoneyText : MonoBehaviour
{
    private TextMeshProUGUI _moneyText;

    private void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = $"Money: {MoneyHandler.AmountOfMoney}";
    }
}
