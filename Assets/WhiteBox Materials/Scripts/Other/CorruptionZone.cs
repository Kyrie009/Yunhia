using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionZone : GameBehaviour
{
    //reduces the player corruption gauge while in the zone
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //do DOT damage
        }
    }

}
