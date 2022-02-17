using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    // Variables
    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Amount of Weights and Behaviors aren't the same in: " + name, this);
            return Vector2.zero;
        }

        // Set up move
        Vector2 move = Vector2.zero;

        // Iterate through behaviors
        for(int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i]; // Multply each behavior by it's weight

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i]) // Make sure it doesn't exceed the weight
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }
}
