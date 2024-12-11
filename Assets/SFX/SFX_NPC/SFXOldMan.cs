using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManSFX : MonoBehaviour
{
    public AudioSource audioSource;  // L'AudioSource qui va jouer le son
    public AudioClip oldman;         // Son de l'homme �g�
    private bool hasPlayed = false;  // V�rifie si le son a d�j� �t� jou�

    void Start()
    {
        // S'assurer que le son ne soit pas en boucle
        audioSource.loop = false;  // D�sactive la boucle du son
    }

    void OnMouseDown()
    {
        // Si le son n'a pas d�j� �t� jou� et qu'il n'est pas en train de jouer
        if (!hasPlayed && !audioSource.isPlaying)
        {
            audioSource.clip = oldman;  // Assigne le clip audio
            audioSource.Play();         // Joue le son
            hasPlayed = true;           // Marque que le son a �t� jou�

            StartDialogue();            // Commence le dialogue
        }
    }

    void StartDialogue()
    {
        // Logique pour afficher ton dialogue ici
        Debug.Log("Dialogue de l'homme �g� commenc�!");
    }
}
