using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManagerScript2 : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public GameObject nameText;
    public GameObject dialogueText;
    public bool firstDialogueWasPlayed;
    public bool DialogueMode;
    public Animator animator;
    private Queue<string> sentences;
    private Dialogue currentDialogue;

    [Header("Typing Speed")]
    [SerializeField]
    [Tooltip("Adjust the typing speed in seconds for each character.")]
    [Range(0f, 0.25f)]
    private float typingSpeed;

    [SerializeField]
    private GameObject[] objectToHideDuringDialogue;
    [SerializeField]
    private MonoBehaviour[] scriptToDisableDuringDialogue;
    private bool isTyping;
    private string currentSentence; // Store the current sentence

    void Awake()
    {
        animator = GetComponent<Animator>();
        sentences = new Queue<string>();
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            if (DialogueMode)
            {
                if (isTyping)
                {
                    // If typing is in progress, skip to the end of the sentence
                    SkipToSentenceEnd();
                }
                else
                {
                    // If not typing, display the next sentence
                    DisplayNextSentence();
                }
            }
        }
        if (DialogueMode)
        {

            foreach (GameObject thingsToHide in objectToHideDuringDialogue)
            {
                if (thingsToHide != null)
                {
                    thingsToHide.SetActive(false);
                }
            }

            foreach (MonoBehaviour thingsToHide in scriptToDisableDuringDialogue)
            {
                if (thingsToHide != null)
                {
                    thingsToHide.enabled = false;
                }
            }
        }
        else
        {

            foreach (GameObject thingsToHide in objectToHideDuringDialogue)
            {
                if (thingsToHide != null)
                {
                    thingsToHide.SetActive(true);
                }
            }


            foreach (MonoBehaviour thingsToHide in scriptToDisableDuringDialogue)
            {
                if (thingsToHide != null)
                {
                    thingsToHide.enabled = true;
                }
            }

        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;

        animator.SetBool("IsOpen", true);

        if (nameText != null)
        {
            nameText.GetComponent<TMP_Text>().text = dialogue.name;
        }

        sentences.Clear();


        //GameManager.Instance.IsAnyDialogueBeingShowed = true;

        foreach (string sentence in dialogue.sentences)
        {
            string text = sentence;

            /*if (dialogue.isLocalized)
            {
                text = ChickenLocalization.Instance.GetTextFromKey(sentence);
            }*/

            sentences.Enqueue(text);
        }

        DisplayNextSentence();
        DialogueMode = true;


    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue(); // Store the current sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.GetComponent<TMP_Text>().text = "";
        isTyping = true; // Set the typing flag to true


        foreach (char letter in sentence)
        {

            // If not inside asterisks, append the letter directly to the dialogue text
            dialogueText.GetComponent<TMP_Text>().text += letter;

            //AudioManager.Instance.SFX_Dialogue_WrittenText();

            yield return new WaitForSeconds(typingSpeed); // Add a delay between characters
        }

        isTyping = false; // Set the typing flag to false when typing is complete
    }


    void SkipToSentenceEnd()
    {
        StopAllCoroutines(); // Stop the typing coroutine
        dialogueText.GetComponent<TMP_Text>().text = currentSentence; // Display the complete sentence
        isTyping = false; // Set the typing flag to false
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        DialogueMode = false; // Set DialogueMode to false at the end of the dialogue
                              // Resume the game


        if (currentDialogue.thingsToActivate.Length > 0)
        {
            ActivateGameObjects();
        }

        if (currentDialogue.m_OnClear != null)
        {
            currentDialogue.m_OnClear.Invoke();
        }

        //GameManager.Instance.IsAnyDialogueBeingShowed = false;
    }

    void ActivateGameObjects()
    {
        foreach (GameObject obj in currentDialogue.thingsToActivate)
        {
            obj.SetActive(true); // Activate the GameObject
        }
    }
}