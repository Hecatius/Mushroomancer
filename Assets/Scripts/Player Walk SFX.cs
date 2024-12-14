using UnityEngine;

public class PlayerWalkFX : MonoBehaviour
{
    public AudioSource audioSource; // Source audio pour les sons de pas
    public AudioClip footsteps1;    // Premier son de pas
    public AudioClip footsteps2;    // Deuxi�me son de pas
    public float stepInterval = 0.5f; // Intervalle de temps entre chaque pas

    private bool isWalking = false; // Indique si le joueur marche
    private bool toggleStep = false; // Alterne entre les deux sons
    private float stepCooldown = 0f; // Timer pour jouer les sons de pas

    void Update()
    {
        // V�rifie si le joueur est en train de marcher (via Animator)
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            isWalking = animator.GetBool("isWalking") || animator.GetBool("isRunning");
        }

        // Si le joueur marche, joue des sons de pas
        if (isWalking)
        {
            HandleFootsteps();
        }
        else
        {
            // Arr�te le son si le joueur est immobile
            audioSource.Stop();
            stepCooldown = 0f; // R�initialise le timer
        }
    }

    private void HandleFootsteps()
    {
        if (stepCooldown <= 0f) // Si le cooldown est �coul�
        {
            // Alterne entre Footsteps1 et Footsteps2
            audioSource.clip = toggleStep ? footsteps1 : footsteps2;
            audioSource.Play();
            toggleStep = !toggleStep;

            // R�initialise le timer
            stepCooldown = stepInterval;
        }

        // R�duit le cooldown au fil du temps
        stepCooldown -= Time.deltaTime;
    }
}