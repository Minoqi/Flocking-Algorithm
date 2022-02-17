using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors return current heading
        if (context.Count == 0 || filter.Filter(agent, context).Count == 0)
        {
            return agent.transform.up;
        }

        // Add all points together and average
        Vector2 alignmentMove = Vector2.zero;
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
            alignmentMove += (Vector2)item.transform.up; // Get direction facing
        }

        alignmentMove /= filteredContext.Count; // Average direction

        return alignmentMove;
    }
}
