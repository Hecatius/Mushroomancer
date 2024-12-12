using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXZombieGroan : MonoBehaviour
{
    public AudioSource audioSource;  // AudioSource for the zombie growl
    public AudioClip zombieGrowl1;   // First growl sound
    public AudioClip zombieGrowl2;   // Second growl sound
    public Transform player;         // Reference to the player's Transform
    public float maxVolume = 0.5f;   // Maximum volume when the player is close
    public float minVolume = 0.1f;   // Minimum volume when the player is far
    public float detectionRange = 10f; // Range within which the growl is audible
    private bool isAlive = true;     // Tracks if the zombie is alive
    private bool toggleGrowl = false; // Toggles between growl sounds

    void Start()
    {
        PlayNextGrowl(); // Start growling
    }

    void Update()
    {
        if (isAlive)
        {
            AdjustGrowlVolume();
        }
    }

    private void AdjustGrowlVolume()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Adjust the volume based on the player's distance
        if (distance <= detectionRange)
        {
            float volume = Mathf.Lerp(maxVolume, minVolume, distance / detectionRange);
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0; // Mute if the player is out of range
        }
    }

    private void PlayNextGrowl()
    {
        if (isAlive)
        {
            // Alternate between ZombieGrowl1 and ZombieGrowl2
            audioSource.clip = toggleGrowl ? zombieGrowl2 : zombieGrowl1;
            audioSource.Play();
            toggleGrowl = !toggleGrowl;

            // Schedule the next growl after the current one finishes
            Invoke(nameof(PlayNextGrowl), audioSource.clip.length + Random.Range(0.5f, 2f)); // Add a random delay for variety
        }
    }

    public void TakeDamage()
    {
        if (isAlive)
        {
            StopGrowling(); // Stop growling when hurt
        }
    }

    public void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            StopGrowling(); // Stop growling permanently on death
        }
    }

    private void StopGrowling()
    {
        audioSource.Stop();
        CancelInvoke(nameof(PlayNextGrowl)); // Stop scheduled growls
    }
}