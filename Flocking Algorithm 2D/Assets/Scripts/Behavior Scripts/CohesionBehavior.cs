using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbors, return 0
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // Add all points together and average
        Vector2 cohesionMove = Vector2.zero;

        foreach(Transform item in context)
        {
            cohesionMove += (Vector2)item.position; // Add all points
        }
        cohesionMove /= context.Count; // Average point

        // Create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        return cohesionMove;
    }
}
