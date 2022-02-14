using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors return current heading
        if (context.Count == 0)
        {
            return agent.transform.up;
        }

        // Add all points together and average
        Vector2 alignmentMove = Vector2.zero;

        foreach (Transform item in context)
        {
            alignmentMove += (Vector2)item.transform.up; // Get direction facing
        }

        alignmentMove /= context.Count; // Average direction

        return alignmentMove;
    }
}
