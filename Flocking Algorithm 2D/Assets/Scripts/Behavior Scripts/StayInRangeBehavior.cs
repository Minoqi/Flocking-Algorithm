using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Range")]
public class StayInRangeBehavior : FlockBehavior
{
    // Variables
    public Vector2 center;
    public float radius = 75f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Variables
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float distanceCheckFromCenter = centerOffset.magnitude / radius; // Checks distance to radius (closer to 1 farther from radius, closer to 0 closer to center)

        // Check if far from radius or not
        if (distanceCheckFromCenter < 0.9f) // Don't do anything if close enough to radius
        {
            return Vector2.zero;
        }

        return centerOffset * Mathf.Pow(distanceCheckFromCenter, 2); // Adjust movement to go towards the center
    }
}
