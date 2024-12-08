using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // Get the SpriteRenderer and NavMeshAgent components
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Check the direction of the destination relative to the current position
        Vector3 destination = navMeshAgent.destination;
        float direction = destination.x - transform.position.x;

        // Flip the sprite based on the direction
        if (direction > 0) // Destination is to the right
        {
            spriteRenderer.flipX = true;
        }
        else if (direction < 0) // Destination is to the left
        {
            spriteRenderer.flipX = false;
        }
    }
}

