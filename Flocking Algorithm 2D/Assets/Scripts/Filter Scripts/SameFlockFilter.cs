using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalList)
    {
        List<Transform> filtered = new List<Transform>();

        foreach(Transform item in originalList) // Iterate through original list
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();

            if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock) // Make sure it's flock agent & in same flock
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
