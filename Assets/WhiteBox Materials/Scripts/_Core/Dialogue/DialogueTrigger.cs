using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : GameBehaviour
{
    [Header("Trigger Options")]
    public bool invisible = false;
    public bool triggerOn = false;
    public bool interactableObject = false;
    public bool destroyOnCompletion = false;
    public bool hasCutScene;
    [Header("CutScene")]
    public Sprite cutScene;
    [Header("References")]
    public Dialogue dialogue;
    [Header("Sounds")]
    public AudioSource[] soundEffects;
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
                soundEffects[0].Play();
                canInteract = false;
                TriggerDialogue();
            }
        }
    }
    //Use trigger to proc dialogue
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOn && collision.CompareTag("Player"))
        {
            TriggerDialogue();          
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {  
        //Checks if player can interact with someone.
        if (!_DM.IsDialogueActive() && interactableObject && collision.CompareTag("Player"))
        {
            canInteract = true;
            //enable helper text here
        }
        else
        {
            canInteract = false;
            //disable helper text here
        }

        if (hasCutScene)
        {
            if (_DM.IsDialogueEnded())
            {
                _UI.cutsceneAnim.SetTrigger("CloseCutScene");
            }
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
        if (hasCutScene) //play cutscene if it has it
        {
            _UI.PlayCutscene(cutScene);
        }
        GameEvents.ReportInteracting();
    }

}
