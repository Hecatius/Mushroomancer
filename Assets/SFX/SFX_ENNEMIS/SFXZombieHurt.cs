using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXZombieHurt : MonoBehaviour
{
    public AudioSource audioSource;   // AudioSource to play the hurt sounds
    public AudioClip zombieHurt1;     // First hurt sound
    public AudioClip zombieHurt2;     // Second hurt sound
    private bool toggleHurt = false;  // Toggle between the two sounds

    // Call this method when the zombie takes damage
    public void TakeDamage(int damage)
    {
        PlayHurtSound(); // Play the hurt sound
        ApplyDamage(damage); // Handle damage logic
    }

    private void PlayHurtSound()
    {
        // Alternate between ZombieHurt1 and ZombieHurt2
        audioSource.clip = toggleHurt ? zombieHurt2 : zombieHurt1;
        audioSource.Play();
        toggleHurt = !toggleHurt; // Switch to the other sound for the next hit
    }

    private void ApplyDamage(int damage)
    {
        // Logic for applying damage (e.g., reducing health)
        Debug.Log($"Zombie took {damage} damage!");
        // You can add health reduction or death logic here if needed
    }
}
