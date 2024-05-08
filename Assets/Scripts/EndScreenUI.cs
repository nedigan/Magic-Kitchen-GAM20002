using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenUI : MonoBehaviour, IUpdateUI
{
    private DayManager _dayManager;
    private MoneyHandler _moneyHandler;
    private Canvas _canvas;

    [SerializeField] private TextMeshProUGUI _moneyMade;
    [SerializeField] private TextMeshProUGUI _expenses;
    [SerializeField] private TextMeshProUGUI _profit;
    // Start is called before the first frame update
    void Start()
    {
        _dayManager = FindFirstObjectByType<DayManager>();
        _moneyHandler = FindFirstObjectByType<MoneyHandler>();

        if (_dayManager != null)
            _dayManager.OnDayEnd += UpdateUI;
        else
            Debug.LogError("There is no day manager in the scene");

        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void UpdateUI(object sender = null, EventArgs e = null)
    {
        _canvas.enabled = true;

        int moneyMade = _moneyHandler.AmountOfMoney - _moneyHandler.StartingMoney;
        _moneyMade.text = $"Money Made: {moneyMade}";
        _expenses.text = $"Expenses: -{_moneyHandler.Expenses}";
        _profit.text = $"Profit: {moneyMade - _moneyHandler.Expenses}";
    }
}
