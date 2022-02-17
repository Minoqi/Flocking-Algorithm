using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors return no adjustment
        if (context.Count == 0 || filter.Filter(agent, context).Count == 0)
        {
            return Vector2.zero;
        }

        // Add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int numAvoid = 0; // Num of boids to avoid
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
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                Debug.Log(Vector2.SqrMagnitude(item.position - agent.transform.position));
                numAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }

        if (numAvoid > 0)
        {
            avoidanceMove /= numAvoid;
        }

        return avoidanceMove;
    }
}
