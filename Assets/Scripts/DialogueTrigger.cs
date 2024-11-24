using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private bool playFirstDialogueOnGameStart;
    public Dialogue[] dialogues;
    
    void Start()
    {   if (playFirstDialogueOnGameStart)
        {
            TriggerDialogue(0);
        }
    }

    public void TriggerDialogue (int index)
    
    {
        //Exemple de call de l;a fonction dans votre code
        //FindObjectOfType<DialogueTrigger>().TriggerDialogue(0) //affiche le premier dialogue changer le 0 si vous voulez faire apparaitre un autre dialogue.
        if (index >= 0 && index < dialogues.Length)
        {
            GetComponent<DialogueManagerScript2>().StartDialogue(dialogues[index]);
        }
        else
        {
            Debug.LogWarning("Index out of range for dialogues array.");
        }
    }
    private void OnValidate()
    {
        foreach (var dialogue in dialogues)
        {
            if (dialogue != null)
            {
                EnforceCharacterLimit(dialogue);
            }
        }
    }

    private void EnforceCharacterLimit(Dialogue dialogue)
    {
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            string sentence = dialogue.sentences[i];
            if (sentence.Length > 240)
            {
                dialogue.sentences[i] = sentence.Substring(0, 240);
            }
        }
    }
}