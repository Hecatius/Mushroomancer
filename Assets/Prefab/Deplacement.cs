using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deplacement : MonoBehaviour

{
    public GameObject cursorPrefab; // The cursor prefab (can be a 3D model or sprite)
    public float cursorRotation = 0f; // Rotation of the cursor
    public Vector3 cursorSize = new Vector3(1f, 1f, 1f); // Size of the cursor

    public float walkingSpeed = 3.5f; // Speed for walking
    public float runningSpeed = 6f; // Speed for running

    public float defaultStoppingDistance = 0f; // Default stop distance

    public float attackStoppingDistance = 0f; // Default stop distance

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private GameObject currentCursor;
    private GameObject currentTarget; // The currently targeted enemy
    private bool isTargeting = false; // Is the player targeting an enemy?

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Initially set the agent's speed to walking speed
        navMeshAgent.speed = walkingSpeed;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If the right mouse button is being held down (Input.GetMouseButton(1))
        if (Input.GetMouseButton(1)) // Right-click and hold (Mouse button 1 is right-click)
        {
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
                // If we clicked on the NavMesh or an area (not an enemy), move to the clicked position
                else if (hit.collider.CompareTag("Ground"))
                {
                    // Set the destination to the clicked NavMesh position
                    ResetTarget(); // Deselect the target if any
                    navMeshAgent.SetDestination(hit.point); // Move towards the clicked location

                    // Optionally, instantiate the cursor at the clicked position
                    if (currentCursor != null)
                    {
                        Destroy(currentCursor); // Destroy the previous cursor (if any)
                    }
                    currentCursor = Instantiate(cursorPrefab, hit.point + new Vector3(0,0.1f,0), Quaternion.identity);
                    currentCursor.transform.rotation = Quaternion.Euler(cursorRotation, 0, 0); // Apply rotation
                    currentCursor.transform.localScale = cursorSize; // Apply size

                    navMeshAgent.stoppingDistance = defaultStoppingDistance;

                }
                // If the clicked object is neither an enemy nor NavMesh, reset the target
                else
                {
                    ResetTarget();
                }
            }
            // Handle the player's movement speed (walking vs running)
            if (Input.GetKey(KeyCode.LeftShift)) // Running with shift key
            {
                navMeshAgent.speed = runningSpeed;
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);

            }
            else
            {
                navMeshAgent.speed = walkingSpeed;
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }

        // If we're targeting an enemy, move towards and attack it
        if (isTargeting && currentTarget != null)
        {
            MoveToTarget();

            // Check if we are close enough to attack the enemy
            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackStoppingDistance)
            {
            }
        }

        

    }

    // Sets the target for the player (enemy)
    void SetTarget(GameObject target)
    {
        currentTarget = target;
        isTargeting = true;
        navMeshAgent.isStopped = false; // Make sure the agent is not stopped


    }

    // Resets the target and stops the agent
    void ResetTarget()
    {   
        currentTarget = null;
        isTargeting = false;
        navMeshAgent.isStopped = false; // Ensure the agent isn't stopped when moving to a new location
    }

    // Moves the player towards the current target
    void MoveToTarget()
    {
        navMeshAgent.stoppingDistance = attackStoppingDistance;

        if (currentTarget != null)
        {
            navMeshAgent.SetDestination(currentTarget.transform.position); // Move towards the target



            // Optionally, instantiate the cursor at the clicked position
            if (currentCursor != null)
            {
                Destroy(currentCursor); // Destroy the previous cursor (if any)
            }
            currentCursor = Instantiate(cursorPrefab, currentTarget.transform.position - new Vector3(0,1,0), Quaternion.identity);
            currentCursor.transform.rotation = Quaternion.Euler(cursorRotation, 0, 0); // Apply rotation
            currentCursor.transform.localScale = cursorSize; // Apply size

            print(navMeshAgent.stoppingDistance);

           
        }
    }

    // Attack
    void Attack(GameObject target)
    {
        target.GetComponent<stats>().TakeDamage(1f);
    }
}

