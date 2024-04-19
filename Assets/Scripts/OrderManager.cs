using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Handles generating new OrderTickets
public class OrderManager : MonoBehaviour
{
    private Recipe[] _recipieCach;

    // Start is called before the first frame update
    void Start()
    {
        FillRecipeCach();
    }

    private void FillRecipeCach()
    {
        _recipieCach = GetAllRecipesInProject();
    }

    private Recipe[] GetAllRecipesInProject()
    {
        return Resources.LoadAll<Recipe>("Recipes");
    }

    public OrderTicket GenerateRandomOrderTicket(Animal recipient)
    {
        OrderTicket orderTicket = ScriptableObject.CreateInstance<OrderTicket>();
        
        orderTicket.Recipe = Instantiate(_recipieCach[Random.Range(0, _recipieCach.Length)]);
        orderTicket.Recipient = recipient;

        return orderTicket;
    }
}
