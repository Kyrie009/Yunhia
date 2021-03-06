using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    //Dialogue sentences stored in queue
    private Queue<string> sentences;
    //Animation
    public Animator animator;
    //references
    public TMP_Text nameText;
    public TMP_Text sentenceText;
    //checks
    bool isDialogueActive = false;
    [HideInInspector]
    public bool isDialogueEnded = false;

    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        if (isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DisplayNextSentence();
            }
        }       
    }
    //Starts the Dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueEnded = false;
        isDialogueActive = true;
        animator.SetBool("active", true);
        nameText.text = dialogue.name;
        //Clears dialogue from the previous conversation
        sentences.Clear();
        //goes through the passed dialogue and adds them to the queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        //Display First sentence
        DisplayNextSentence();
    }
    //display next sentence in queue
    public void DisplayNextSentence()
    {
        animator.SetTrigger("press");
        //Ends dialogue when no sentences left in queue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return; //ends the function here instead of reading the rest
        }      
        string sentence = sentences.Dequeue();      //Removes the sentence from the queue 
        StopAllCoroutines();                        //Stops all animations before displaying the next sentence(prevents spam)
        StartCoroutine(TypeSentence(sentence));     //Displays the Sentences
    }
    //Types out the sentence instead of the words being instantly displayed
    IEnumerator TypeSentence(string sentence)
    {
        //Dialogue is first set to an empty string before displaying the letters
        sentenceText.text = "";
        //loops through each character in the text(special character array function
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;    //displays a letter
            yield return null;              //This delays a single frame
        }
    }
    //Closes dialogue
    void EndDialogue()
    {
        isDialogueActive = false;
        isDialogueEnded = true;
        animator.SetBool("active", false);
        GameEvents.ReportGamePlaying();
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    public bool IsDialogueEnded()
    {
        return isDialogueEnded;
    }

}
