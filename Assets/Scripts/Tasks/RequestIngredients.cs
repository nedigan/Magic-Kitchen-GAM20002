using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecipeTypes
{
    ScrambledEggs,
    FriedEgg,
    Pavlova,
    Bread,
    Omelette,
    BirthdayCake
}

public class RequestIngredients : Task
{
    private int _ingredientsRemaining;

    private bool _mealCooked = false;

    private Dictionary<RecipeTypes, int> _recipes = new Dictionary<RecipeTypes, int>() // assuming only one type of ingredient
    {
        {RecipeTypes.ScrambledEggs , 2 },
        {RecipeTypes.FriedEgg, 1 },
        {RecipeTypes.Pavlova , 3 },
        {RecipeTypes.Bread , 2 },
        {RecipeTypes.Omelette , 2 },
        {RecipeTypes.BirthdayCake, 4 }
    };

    private Station _stove;

    public override TaskHolder FindTaskHolder()
    {
        Station stove = FindEmptyStationOfType(StationType.Stove);

        if (stove != null)
        {
            _stove = stove;
            return _stove.TaskHolder;
        }
        return null;
    }

    public void DeliverIngredient()
    {
        _ingredientsRemaining--;
    }

    public override void FinishTask()
    {
        Debug.Log("Meal is cooked");
        _stove.TaskHolder.RemoveCurrentTask();
    }

    public override void PerformTask()
    {
        if (_ingredientsRemaining <= 0 && !_mealCooked)
        {
            FinishTask();
            _mealCooked = true;
        }
    }

    public override void StartTask()
    {
        Debug.Log("Ask for ingredients");
        // Convert dictionary to list of key-value pairs
        List<KeyValuePair<RecipeTypes, int>> list = new List<KeyValuePair<RecipeTypes, int>>(_recipes);

        // Get a random index within the range of the list
        int randomIndex = Random.Range(0, list.Count);

        // Retrieve the random item
        KeyValuePair<RecipeTypes, int> randomRecipe = list[randomIndex];

        //_ingredientsRemaining = randomRecipe.Value;
        _ingredientsRemaining = 1;

        for (int i = 0; i < _ingredientsRemaining; i++)
        {
            GatherIngredient gather = ScriptableObject.CreateInstance<GatherIngredient>();
            gather.SetUp(_stove);
            Manager.ManageTask(gather);
        }
    }
}
