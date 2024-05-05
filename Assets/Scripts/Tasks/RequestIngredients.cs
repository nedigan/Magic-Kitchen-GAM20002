using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum RecipeTypes
//{
//    ScrambledEggs,
//    FriedEgg,
//    Pavlova,
//    Bread,
//    Omelette,
//    BirthdayCake
//}

public class RequestIngredients : Task
{
    //private int _ingredientsRemaining;

    //private bool _mealCooked = false;

    //private Dictionary<RecipeTypes, int> _recipes = new Dictionary<RecipeTypes, int>() // assuming only one type of ingredient
    //{
    //    {RecipeTypes.ScrambledEggs , 2 },
    //    {RecipeTypes.FriedEgg, 1 },
    //    {RecipeTypes.Pavlova , 3 },
    //    {RecipeTypes.Bread , 2 },
    //    {RecipeTypes.Omelette , 2 },
    //    {RecipeTypes.BirthdayCake, 4 }
    //};

    private Station _stove;
    private OrderTicket _ticket;
    private Thought _recipeThought;

    public Station Stove => _stove;

    public void SetUp(OrderTicket ticket)
    {
        _ticket = ticket;
    }

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

    public void DeliverIngredient(Item ingredient)
    {
        _ticket.Recipe.Ingredients.Remove(ingredient.Type);

        Destroy(ingredient.gameObject);

        if (_ticket.Recipe.Ingredients.Count <= 0)
        {
            FinishTask();
        }
    }

    public override void FinishTask()
    {
        Debug.Log("Meal is cooked");
        _stove.TaskHolder.RemoveCurrentTask();
        _stove.Occupied = false;

        // Create Item for the Meal
        _ticket.Meal = Instantiate(_ticket.Recipe.Result);
        _ticket.Meal.transform.position = _stove.ItemPlaceLocation.transform.position;
        //_ticket.Meal.transform.rotation = _stove.ItemPlaceLocation.transform.rotation;

        _ticket.Meal.SetCurrentRoom(_stove.CurrentRoom);

        // Create TurtleGrabMeal Task
        TurtleGrabMeal grabMealTask = ScriptableObject.CreateInstance<TurtleGrabMeal>();
        grabMealTask.SetUp(_ticket);
        Manager.ManageTask(grabMealTask);

        _stove.ThoughtManager.StopThinking();
    }

    public override void PerformTask()
    {
        //if (_ingredientsRemaining <= 0 && !_mealCooked)
        //{
        //    FinishTask();
        //    _mealCooked = true;
        //}
    }

    public override void StartTask()
    {
        Debug.Log("Asking for ingredients");
        _stove.Occupied = true;

        _stove.ThoughtManager.ThinkAbout(Thought.FromThinkable(_ticket.Recipe).SetEmotion(ThoughtEmotion.Info).SetScale(1));

        // Create a GatherIngredient task for each ingredient in the recipe
        foreach (ItemType ingredient in _ticket.Recipe.Ingredients)
        {
            GatherIngredient gather = ScriptableObject.CreateInstance<GatherIngredient>();
            gather.SetUp(this, ingredient);
            Manager.ManageTask(gather);
        }
    }
}
