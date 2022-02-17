using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Variables
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 20;
    [Range(0f, 10f)]
    public float AgentDensity = 0.08f; // Size of spawn circle

    // Flock Behavior
    [Range(1f, 100f)]
    public float driveFactor = 10f; // Makes agents move faster (1 is default rate)
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f; // Radius to look for neighbors/things nearby
    [Range(0f, 10f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    // Utility (Saves on math)
    float squareMaxSpeed; 
    float squareNeighborRadius; 
    float squareAvoidanceRadius; 
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * startingCount * AgentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform); // Instantiate inside random point inside circle
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behavior.CalculateMove(agent, context, this); // Run scriptable object to calculate move
            move *= driveFactor; // For speedier movement

            if (move.sqrMagnitude > squareMaxSpeed) // Don't pass max speed
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }

    // Run physics overlap check to see what other agents get hit
    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

        foreach(Collider2D col in contextColliders)
        {
            if (col != agent.AgentCollider) // Not own collider
            {
                context.Add(col.transform);
            }
        }

        return context;
    }
}
