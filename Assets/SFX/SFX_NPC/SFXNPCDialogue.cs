using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public AudioSource audioSource;   // L'AudioSource qui va jouer le son
    public AudioClip Male1;           // Son 1
    public AudioClip Male2;           // Son 2
    private bool isFirstClick = true; // Vérifie quel son jouer

    void OnMouseDown()
    {
        if (isFirstClick)
        {
            audioSource.clip = Male1; // Premier clic -> Male 1
            isFirstClick = false;      // Change l'état pour le prochain clic
        }
        else
        {
            audioSource.clip = Male2; // Clic suivant -> Male 2
            isFirstClick = true;       // Remet l'état à "true" pour alterner
        }

        audioSource.Play();  // Joue le son
        StartDialogue();     // Commence le dialogue
    }

    void StartDialogue()
    {
        // Ici tu peux appeler la logique pour afficher ton dialogue
        Debug.Log("Dialogue commencé!");
    }
}