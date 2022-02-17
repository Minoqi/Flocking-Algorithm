using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    // Variables
    public Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    
    public Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        transform.up = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
