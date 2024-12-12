using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSwordSlash : MonoBehaviour
{
    public AudioSource audioSource;  // AudioSource to play the sounds
    public AudioClip swordSlash1;    // First sword slash sound
    public AudioClip swordSlash2;    // Second sword slash sound
    private bool toggleSlash = false; // Toggle between the two sounds

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            PlaySwordSound(); // Play the sword slash sound
            PerformAttack();  // Trigger attack logic (if needed)
        }
    }

    void PlaySwordSound()
    {
        // Alternate between SwordSlash1 and SwordSlash2
        audioSource.clip = toggleSlash ? swordSlash2 : swordSlash1;
        audioSource.Play();
        toggleSlash = !toggleSlash; // Switch to the other sound for the next attack
    }

    void PerformAttack()
    {
        // Logic for the attack (e.g., damage calculation, animation trigger)
        Debug.Log("Sword attack performed!");
    }
}