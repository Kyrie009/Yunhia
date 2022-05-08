using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : GameBehaviour
{
    public AudioClip KeySound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _P.ManageKey(true);
            _AM.sfx.PlayOneShot(KeySound);
            Destroy(gameObject);
        }
    }
}
