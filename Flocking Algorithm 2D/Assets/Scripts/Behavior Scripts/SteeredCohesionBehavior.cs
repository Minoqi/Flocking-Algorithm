using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    // Variables
    private Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors return no adjustment
        if (context.Count == 0 || filter.Filter(agent, context).Count == 0)
        {
            return Vector2.zero;
        }

        // add all points togehter and average
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
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= filteredContext.Count;

        // create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        // Passed in as a ref so the original will have it's value changed
        return cohesionMove;
    }
}
