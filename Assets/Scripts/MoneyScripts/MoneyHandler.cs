using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyHandler: MonoBehaviour
{
    public int AmountOfMoney { get; private set; } = 400;
    public int StartingMoney { get; private set; }

    public int Expenses { get; private set; } = 100;

    private AudioSource _sound;

    private void Start()
    {
        // Maybe load from Playerprefs eventually???
        StartingMoney = AmountOfMoney;
        _sound = GetComponent<AudioSource>();
    }

    public void AddMoney(int amount)
    {
        AmountOfMoney += amount;
        _sound.Play();
    }

    public  void RemoveMoney(int amount)
    {
        AmountOfMoney -= amount;
    }
}
