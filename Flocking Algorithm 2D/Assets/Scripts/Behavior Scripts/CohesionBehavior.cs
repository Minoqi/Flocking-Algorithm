using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbors, return 0
        if (context.Count == 0 || filter.Filter(agent, context).Count == 0)
        {
            return Vector2.zero;
        }

        // Add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = null;

        if (filter == null) // If not filtering, use original context list
        {
            filteredContext = context;
        }
        else
        {
            filteredContext = filter.Filter(agent, context);
        }

        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position; // Add all points
        }
        cohesionMove /= filteredContext.Count; // Average point

        // Create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        return cohesionMove;
    }
}
