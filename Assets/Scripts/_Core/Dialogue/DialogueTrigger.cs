using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : GameBehaviour
{
    [Header("Check Options")]
    public bool invisible = false;
    public bool triggerOn = false;
    public bool isNPC = false;
    public bool destroyOnCompletion = false;
    [Header("References")]
    public Dialogue dialogue;    
    public AudioSource soundEffect;
    //chacks
    bool canInteract = false;


    private void Start()
    {
        if (invisible) //trigger object invisble when game starts
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (canInteract) //This is confined in update so that the keypress registers properly. may need to improve code later.
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }
    }
    //Use trigger to proc dialogue
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOn && collision.CompareTag("Player"))
        {
            GetSound();
            TriggerDialogue();          
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {  
        //Checks if player can interact with someone.
        if (!_DM.IsDialogueActive() && isNPC && collision.CompareTag("Player"))
        {
            canInteract = true;
            //enable helper text here
        }
        else
        {
            canInteract = false;
            //disable helper text here
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        if (destroyOnCompletion)
        {
            Destroy(this.gameObject);
        }
    }
    //call function for dialogue
    public void TriggerDialogue()
    {
        //disable helpertext here
        _DM.StartDialogue(dialogue);
    }
    //play sound
    private void GetSound()
    {
        if (soundEffect != null)
        {
            soundEffect.Play();
        }
    }

}
