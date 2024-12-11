using UnityEngine;

public class DwarfDialogue : MonoBehaviour
{
    public AudioSource audioSource;  // L'AudioSource qui va jouer le son
    public AudioClip dwarf1;         // Son 1
    public AudioClip dwarf2;         // Son 2
    public AudioClip dwarf3;         // Son 3
    private int clickCount = 0;      // Compteur pour alterner les sons

    void OnMouseDown()
    {
        // Alterne les sons à chaque clic
        if (clickCount == 0)
        {
            audioSource.clip = dwarf1; // Premier clic -> dwarf1
        }
        else if (clickCount == 1)
        {
            audioSource.clip = dwarf2; // Deuxième clic -> dwarf2
        }
        else
        {
            audioSource.clip = dwarf3; // Troisième clic -> dwarf3
        }

        // Joue le son
        audioSource.Play();

        // Incrémente le compteur et le remet à 0 après le troisième clic
        clickCount = (clickCount + 1) % 3;

        StartDialogue(); // Commence le dialogue
    }

    void StartDialogue()
    {
        // Logique pour afficher ton dialogue ici
        Debug.Log("Dialogue du nain commencé!");
    }
}