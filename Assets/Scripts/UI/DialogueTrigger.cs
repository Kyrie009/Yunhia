using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : GameBehaviour
{
    public bool invisible = false;
    public bool triggerOn = false;
    public bool destroyOnCompletion = false;
    public Dialogue dialogue;    
    public AudioSource soundEffect;

    private void Start()
    {
        if (invisible)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    //Use trigger to proc dialogue
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOn && collision.CompareTag("Player"))
        {
            TriggerDialogue();

            if (soundEffect != null)
            {
                soundEffect.Play();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (destroyOnCompletion)
        {
            Destroy(this.gameObject);
        }
    }
    //call function for dialogue
    public void TriggerDialogue()
    {
        _DM.StartDialogue(dialogue);
    }

}
