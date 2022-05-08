using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossDoor : GameBehaviour
{
    public Animator anim;
    public TMP_Text helperText;
    public AudioClip openDoorSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_P.hasKey == true && collision.CompareTag("Player"))
        {
            helperText.text = "Press 'E' to use Key";
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
                _AM.sfx.PlayOneShot(openDoorSound);
            }
        }
        else
        {
            helperText.text = "You need a 'Key' to unseal the door";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            helperText.text = "";
        }
    }

    public void CloseDoor()
    {
        anim.SetTrigger("CloseDoor");
    }
    public void OpenDoor()
    {
        anim.SetTrigger("OpenDoor");
    }


}
