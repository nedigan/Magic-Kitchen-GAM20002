using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Handles generating new OrderTickets
public class OrderManager : MonoBehaviour
{
    private Recipe[] _recipieCach;
    [SerializeField] private TMP_Dropdown _dropdown;

    private List<float> _recipeWeights = new();
    private float _tier1Probability = 50;
    private float _tier2Probability = 35;
    private float _tier3Probability = 15;

    // Start is called before the first frame update
    void Start()
    {
        FillRecipeCach();
        ResetWeights();

        foreach (Recipe recipe in _recipieCach)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData() { text = recipe.name });
        }

        int tier1Chosen = 0;
        int tier2Chosen = 0;
        int tier3Chosen = 0;

        for (int i=0; i < 100; i++) // Testing to see if distribution is correct
        {
            Recipe r = GetWeightedRandomRecipe();
            if (r.Tier == Tiers.Tier1)
                tier1Chosen++;
            else if (r.Tier == Tiers.Tier2)
                tier2Chosen++;
            else if (r.Tier == Tiers.Tier3)
                tier3Chosen++;
        }

        Debug.Log($"Tier 1: {tier1Chosen}");
        Debug.Log($"Tier 2: {tier2Chosen}");
        Debug.Log($"Tier 3: {tier3Chosen}");

    }

    private void FillRecipeCach()
    {
        _recipieCach = GetAllRecipesInProject();
    }

    private void ResetWeights()
    {
        int numOfTier1 = _recipieCach.Count(r => r.Tier == Tiers.Tier1);
        int numOfTier2 = _recipieCach.Count(r => r.Tier == Tiers.Tier2);
        int numOfTier3 = _recipieCach.Count(r => r.Tier == Tiers.Tier3);

        foreach (Recipe recipe in _recipieCach)
        {
            if (recipe.Tier == Tiers.Tier1)
            {
                _recipeWeights.Add(_tier1Probability / numOfTier1);
            }
            else if (recipe.Tier == Tiers.Tier2)
            {
                _recipeWeights.Add(_tier2Probability / numOfTier2);
            }
            else if (recipe.Tier == Tiers.Tier3)
            {
                _recipeWeights.Add(_tier3Probability / numOfTier3);
            }
        }
    }

    private Recipe[] GetAllRecipesInProject()
    {
        return Resources.LoadAll<Recipe>("Recipes");
    }

    public OrderTicket GenerateRandomOrderTicket(Animal recipient)
    {
        OrderTicket orderTicket = ScriptableObject.CreateInstance<OrderTicket>();
        
        orderTicket.Recipe = Instantiate(GetWeightedRandomRecipe());
        orderTicket.Recipient = recipient;

        return orderTicket;
    }

    public Recipe GetWeightedRandomRecipe()
    {
        if (_recipieCach.Length != _recipeWeights.Count || _recipieCach.Length == 0)
        {
            throw new ArgumentException("Items and weights must be of the same non-zero length.");
        }

        // Calculate the cumulative weights
        float totalWeight = 0;
        float[] cumulativeWeights = new float[_recipieCach.Length];
        for (int i = 0; i < _recipieCach.Length; i++)
        {
            totalWeight += _recipeWeights[i];
            cumulativeWeights[i] = totalWeight;
        }

        // Generate a random number between 0 and totalWeight
        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        // Determine which item corresponds to the random number
        for (int i = 0; i < cumulativeWeights.Length; i++)
        {
            if (randomValue < cumulativeWeights[i])
            {
                return _recipieCach[i];
            }
        }

        // This should not be reached if weights are valid
        throw new InvalidOperationException("Random selection failed.");
    }
}
