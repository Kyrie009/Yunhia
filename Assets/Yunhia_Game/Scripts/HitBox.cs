using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : GameBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            return;

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(_P.attack);
            AttackEffect();
        }
    }

    private void AttackEffect()
    {
        //anim.SetTrigger("Hit"); //play hit animation
        //sFX.Play(); //play hit sound
    }
}
