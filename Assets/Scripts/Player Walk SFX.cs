using UnityEngine;

public class PlayerWalkFX : MonoBehaviour
{
    public float walkDuration = 2.0f;   // Time to walk left
    public float speed = 2.0f;          // Walking speed

    public GameObject Player;    // Reference to the NPC Trent prefab !!!!!TO MODIFY!!!!!
    public AudioClip Footsteps1;    // First footstep sound
    public AudioClip Footsteps2;    // Second footstep sound

    private AudioSource audioSource;    // AudioSource for playing walking sounds
    private float timer = 0f;
    private bool isWalking = false;

    void Start()
    {
        // Get the AudioSource from the prefab
        if (Player != null)
        {
            audioSource = Player.GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the NPC prefab.");
        }
    }

    void Update()
    {
        // Start walking when NPC is idle and timer is 0
        if (!isWalking)
        {
            timer += Time.deltaTime;
            if (timer >= walkDuration)
            {
                isWalking = true;
                timer = 0f;
            }
        }

        // Move left and play alternating sounds if walking
        if (isWalking)
        {
            Player.transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Check if the audio is not playing, then play alternating sounds
            if (audioSource != null && !audioSource.isPlaying)
            {
                PlayRandomFootstep();
            }

            // Stop walking and sound after set duration
            timer += Time.deltaTime;
            if (timer >= walkDuration)
            {
                isWalking = false;
                if (audioSource != null) audioSource.Stop();
                timer = 0f;
            }
        }
    }

    // Function to randomly choose between two footstep sounds
    void PlayRandomFootstep()
    {
        if (audioSource != null)
        {
            // Randomly choose between the two clips
            AudioClip chosenClip = Random.value > 0.5f ? Footsteps1 : Footsteps2; //Add third Footstep!!!!!!
            audioSource.clip = chosenClip;
            audioSource.Play();

            // Log which sound is being played
            Debug.Log("Playing: " + chosenClip.name);
        }
    }
}