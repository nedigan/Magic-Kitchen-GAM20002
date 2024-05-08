using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateMoneyText : MonoBehaviour, IUpdateUI
{
    private TextMeshProUGUI _moneyText;
    private MoneyHandler _moneyHandler;

    private void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
        _moneyHandler = FindFirstObjectByType<MoneyHandler>();
    }

    public void UpdateUI(object sender = null, EventArgs e = null)
    {
        _moneyText.text = $"Money: {_moneyHandler.AmountOfMoney}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }


}
