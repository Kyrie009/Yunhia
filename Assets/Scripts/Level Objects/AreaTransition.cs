using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : GameBehaviour
{
    public string destinationName;
    public Transform newDestination;
    
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.ReportFreezeEvent(true);
            _UI.areaName = destinationName;
            _UI.TransitionScreen();
            StartCoroutine(TpDelay(collision));
        }
    }

    IEnumerator TpDelay(Collider2D collision)
    {
        yield return new WaitForSeconds(0.5f);
        collision.transform.position = newDestination.position;
        yield return new WaitForSeconds(0.5f);
        GameEvents.ReportFreezeEvent(false);
    }
}
