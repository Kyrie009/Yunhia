using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionZone : GameBehaviour
{
    //reduces the player corruption gauge while in the zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.ReportCorruptionStart();
        }
        
    }
    //stops reducing the corruption gaige when player leaves the zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.ReportCorruptionStop();
        }
    }
}
