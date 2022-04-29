using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : GameBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _P.Hit(_P.maxHealth*2);
        }
    }
}
