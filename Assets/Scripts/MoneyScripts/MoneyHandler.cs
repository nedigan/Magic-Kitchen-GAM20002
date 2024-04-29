using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyHandler
{
    public static int AmountOfMoney { get; private set; } = 400;

    public static void AddMoney(int amount)
    {
        AmountOfMoney += amount;
    }

    public static void RemoveMoney(int amount)
    {
        AmountOfMoney -= amount;
    }
}
