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
        // Alterne les sons � chaque clic
        if (clickCount == 0)
        {
            audioSource.clip = dwarf1; // Premier clic -> dwarf1
        }
        else if (clickCount == 1)
        {
            audioSource.clip = dwarf2; // Deuxi�me clic -> dwarf2
        }
        else
        {
            audioSource.clip = dwarf3; // Troisi�me clic -> dwarf3
        }

        // Joue le son
        audioSource.Play();

        // Incr�mente le compteur et le remet � 0 apr�s le troisi�me clic
        clickCount = (clickCount + 1) % 3;

        StartDialogue(); // Commence le dialogue
    }

    void StartDialogue()
    {
        // Logique pour afficher ton dialogue ici
        Debug.Log("Dialogue du nain commenc�!");
    }
}