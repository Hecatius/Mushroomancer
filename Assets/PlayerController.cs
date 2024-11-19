using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float cursorRotation = 0f; // Rotation of the cursor
    public float attackRange;
    public Vector3 cursorSize = new Vector3(1f, 1f, 1f); // Size of the cursor

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    public GameObject cursorPrefab;
    private GameObject currentCursor;

    private GameObject currentTarget; // The currently targeted enemy
    private bool isTargeting = false; // Is the player targeting an enemy?

    public float walkingSpeed = 3.5f; // Speed for walking
    public float runningSpeed = 6f; // Speed for running

    void Start()
    {
        // Get the Animator and NavMeshAgent components
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Initially set the agent's speed to walking speed
        navMeshAgent.speed = walkingSpeed;
    }

    void Update()
    {
        // Check for a right-click input (Mouse button 1)
        if (Input.GetMouseButton(1)) // Right-click (Mouse button 1 is right-click)
        {
            // Perform a raycast from the camera to where the user clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // If we clicked on an enemy, target the enemy
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Set the current target to the hit enemy
                    SetTarget(hit.collider.gameObject);
                }
                // Check if the raycast hit a valid NavMesh
                else if (hit.collider.CompareTag("Ground")) // Make sure the object has the "NavMesh" tag
                {
                    // Set the destination of the NavMeshAgent to the hit point
                    navMeshAgent.SetDestination(hit.point);

                    UpdateCursor(hit.point);

                    isTargeting = false;
                }
            }

           
        }
        // Check if the player is holding down the shift key to run
        if (Input.GetKey(KeyCode.LeftShift)) // Running with shift key
        {
            navMeshAgent.speed = runningSpeed;
        }
        else
        {
            navMeshAgent.speed = walkingSpeed;
        }

        // Check if the Left Shift key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("attack", true);
        }

        // Reset the "attack" bool when the key is released
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetBool("attack", false);
        }

        // Calculate the distance to the destination
        float distanceToDestination = Vector3.Distance(transform.position, navMeshAgent.destination);

        // Set the "isWalking" bool based on the distance to the destination
        animator.SetBool("isWalking", distanceToDestination > 1f);

        // If we're targeting an enemy, move towards and attack it
        if (isTargeting && currentTarget != null)
        {
            MoveToTarget();
        }
    }

    void SetTarget(GameObject target)
    {
        currentTarget = target;
        isTargeting = true;
        navMeshAgent.isStopped = false; // Make sure the agent is not stopped*/
    }

    // Moves the player towards the current target
    void MoveToTarget()
    {
        if (currentTarget != null)
        {
            navMeshAgent.SetDestination(currentTarget.transform.position); // Move towards the target

            UpdateCursor(currentTarget.transform.position);

            // Check if we are close enough to attack the enemy
            if (Vector3.Distance(transform.position, currentTarget.transform.position) < attackRange)
            {
                navMeshAgent.SetDestination(gameObject.transform.position);
            }

        }
    }

    private void UpdateCursor(Vector3 position)
    {
        // Raise the hit point by 1 unit on the Y-axis
        Vector3 raisedHitPoint = position + new Vector3(0, 0.2f, 0);

        // Place the cursor at the hit point
        if (currentCursor != null)
        {
            Destroy(currentCursor); // Destroy the previous cursor (if any)
        }
        currentCursor = Instantiate(cursorPrefab, raisedHitPoint, Quaternion.identity);

        // Set the cursor's rotation and size
        currentCursor.transform.rotation = Quaternion.Euler(cursorRotation, 0, 0); // Apply rotation
        currentCursor.transform.localScale = cursorSize; // Apply size
    }
}
