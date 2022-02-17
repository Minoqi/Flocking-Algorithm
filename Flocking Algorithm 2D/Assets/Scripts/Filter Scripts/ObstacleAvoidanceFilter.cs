using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Obstacle Avoidance")]
public class ObstacleAvoidanceFilter : ContextFilter
{
    // Variables
    public LayerMask layerToAvoid;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalList)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in originalList) // Iterate through original list
        {
            if (layerToAvoid == (layerToAvoid | (1 << item.gameObject.layer))) // If it's on a laye to avoid add to list
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
