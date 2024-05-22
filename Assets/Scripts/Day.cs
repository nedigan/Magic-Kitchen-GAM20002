using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[CreateAssetMenu()]
public class Day : ScriptableObject
{
    public float DesiredProfit; // use this to generate meals ordered?
    public float DayLengthSeconds = 300f; // should this even change per day?

    public int AmountOfCustomers;
    public AnimationCurve CustomerGraph; // like the wave system
    public List<float> GetCustomerSpawnTimes() // not gonna lie, used my mate gpt to help with this
    {
        // Calculate the total area under the curve
        float totalArea = CalculateTotalArea();
        List<float> customerSpawnTimes = new List<float>();

        // Loop to place each point
        int pointsPlaced = 0;
        while (pointsPlaced < AmountOfCustomers)
        {
            // Generate a random position along the day
            float t = Random.Range(0f, 1f);

            // Evaluate the animation curve at this position
            float curveValue = CustomerGraph.Evaluate(t);

            // Calculate the probability of placing a point at this position
            float probability = curveValue / totalArea;

            // Generate a random value between 0 and 1
            float randomValue = Random.value;

            // If the random value is less than the probability, place the point
            if (randomValue < probability)
            {
                customerSpawnTimes.Add(t);
                pointsPlaced++;
            }
        }
        customerSpawnTimes.Sort();
        return customerSpawnTimes;
    }
    // pretty much integrates the line to find area underneath curve - used with probaility theory?? makes sure it always spawns the amount of customers
    private float CalculateTotalArea()
    {
        // Calculate the total area under the curve by integrating the curve
        float totalArea = 0f;
        for (float t = 0f; t <= 1f; t += 0.01f)
        {
            totalArea += CustomerGraph.Evaluate(t) * 0.01f; // Assume step size of 0.01
        }
        return totalArea;
    }
}
