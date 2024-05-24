using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyHandler: MonoBehaviour
{
    public int AmountOfMoney { get; private set; } = 400;
    public int StartingMoney { get; private set; }

    public int Expenses { get; private set; } = 100;

    private void Start()
    {
        // Maybe load from Playerprefs eventually???
        StartingMoney = AmountOfMoney;
    }
    public void AddMoney(int amount)
    {
        AmountOfMoney += amount;
        AudioManager.GetInstance().PlayClipIndex(0); // money sfx
    }

    public  void RemoveMoney(int amount)
    {
        AmountOfMoney -= amount;
    }
}
