using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.PlayerLoop.PostLateUpdate;

public class Deplacement : MonoBehaviour

{
    public GameObject cursorPrefab; // The cursor prefab (can be a 3D model or sprite)
    public float cursorRotation = 0f; // Rotation of the cursor
    public Vector3 cursorSize = new Vector3(1f, 1f, 1f); // Size of the cursor

    public float walkingSpeed = 3.5f; // Speed for walking
    public float runningSpeed = 6f; // Speed for running
    private float attackCooldown = 0f;
    public float attackSpeed = 1.0f;
    public float baseDamage = 1f;

    public float defaultStoppingDistance = 0f; // Default stop distance

    public float attackStoppingDistance = 0f; // Default stop distance

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameObject currentCursor;
    private GameObject currentTarget; // The currently targeted enemy
    public GameObject dialogueManager;
    private bool isTargeting = false; // Is the player targeting an enemy?

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Initially set the agent's speed to walking speed
        navMeshAgent.speed = walkingSpeed;

        animator = GetComponent<Animator>();

        if (dialogueManager == null ) 
        {
            Debug.Log("Le dialogue manager n'est pas attacher");
        }
    }

    void Update()
    {
        // If the right mouse button is being held down (Input.GetMouseButton(1))
        if (Input.GetMouseButton(1) && dialogueManager.GetComponent<DialogueManagerScript2>().DialogueMode != true) // Right-click and hold (Mouse button 1 is right-click)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // If we clicked on an enemy, target the enemy
                if (hit.collider.CompareTag("Ennemy") || hit.collider.CompareTag("NPC"))
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
                    currentCursor = Instantiate(cursorPrefab, hit.point + new Vector3(0, 0.1f, 0), Quaternion.identity);
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
        }

        // Handle the player's movement speed (walking vs running)
        if (navMeshAgent.velocity.magnitude > 0.1f) // Vérifie si le personnage se déplace
        {
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
        if (isTargeting && currentTarget != null && dialogueManager.GetComponent<DialogueManagerScript2>().DialogueMode != true)
        {
            MoveToTarget();

            // Check if we are close enough to attack the enemy
            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackStoppingDistance)
            {   
                if (currentTarget.tag == "Ennemy") 
                {
                    if (attackCooldown > 0)
                    {
                        attackCooldown -= Time.deltaTime;
                    }

                    if (attackCooldown <= 0)
                    {

                        Debug.Log("je suis proche");
                        Attack(currentTarget);
                        attackCooldown = 1f / attackSpeed;
                    }
                } else if(currentTarget.tag == "NPC")
                {
                    
                    currentTarget.GetComponent<DialogueTrigger>().TriggerDialogue(0);
                    ResetTarget();
                }
            }
        }

        UpdateSpriteDirection(); // Updates the sprite orientation

    }

    private void UpdateSpriteDirection()
    {
        Vector3 referencePosition;

        // Determine the reference position: either the current target or the cursor
        if (currentTarget != null)
        {
            referencePosition = currentTarget.transform.position;
        }
        else if (currentCursor != null)
        {
            referencePosition = currentCursor.transform.position;
        }
        else
        {
            return; // No target or cursor to reference
        }

        // Convert world positions to camera-local positions
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 referenceScreenPos = Camera.main.WorldToScreenPoint(referencePosition);

        // Determine the horizontal direction in screen space
        float directionX = referenceScreenPos.x - playerScreenPos.x;

        // Flip the sprite based on screen-space direction
        spriteRenderer.flipX = directionX < 0;
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
        animator.SetTrigger("Attack");

        target.GetComponent<stats>().TakeDamage(baseDamage);
    }
}

