using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManSFX : MonoBehaviour
{
    public AudioSource audioSource;  // L'AudioSource qui va jouer le son
    public AudioClip oldman;         // Son de l'homme âgé
    private bool hasPlayed = false;  // Vérifie si le son a déjà été joué

    void Start()
    {
        // S'assurer que le son ne soit pas en boucle
        audioSource.loop = false;  // Désactive la boucle du son
    }

    void OnMouseDown()
    {
        // Si le son n'a pas déjà été joué et qu'il n'est pas en train de jouer
        if (!hasPlayed && !audioSource.isPlaying)
        {
            audioSource.clip = oldman;  // Assigne le clip audio
            audioSource.Play();         // Joue le son
            hasPlayed = true;           // Marque que le son a été joué

            StartDialogue();            // Commence le dialogue
        }
    }

    void StartDialogue()
    {
        // Logique pour afficher ton dialogue ici
        Debug.Log("Dialogue de l'homme âgé commencé!");
    }
}
