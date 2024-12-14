using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;  // L'AudioSource qui va jouer la musique
    public AudioClip Minstrel1;      // Musique de fond (Minstrel2)

    void Start()
    {
        // Assure-toi que la musique est en boucle
        audioSource.loop = true;

        // Charge et joue la musique de fond Minstrel2
        audioSource.clip = Minstrel1;
        audioSource.volume = 0.2f; // Volume faible pour être subtile
        audioSource.Play();
    }
}