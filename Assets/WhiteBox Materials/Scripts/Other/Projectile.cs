using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameBehaviour
{
    public float speed;
    public int attack;   
    public AudioSource sFX;
    public Animator anim;
    bool hasHit = false; // check to see if projectile has hit

    private void Awake()
    {
        sFX = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();       
    }

    private void Start()
    {
        StartCoroutine(DestroyIfMiss());
    }

    private void Update()
    {
        if (!hasHit) //projectile stop moving when hit target
        {
            transform.Translate(transform.right * transform.localScale.x * speed * Time.deltaTime); //projectile movement
        }
        
    }

    IEnumerator DestroyIfMiss()
    {
        yield return new WaitForSeconds(0.5f);
        HitEffect();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "CameraBounds") //ignores colission with camerabounds collider. might need a better solution than this.
        {
            if (collision.tag == "Player")
                return;

            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().Hit(attack);
            }

            HitEffect();

        }
    }

    private void HitEffect()
    {
        //When projectile collides into something
        hasHit = true; //stops object from moving
        this.GetComponent<BoxCollider2D>().enabled = false; //prevents overlapping colliders
        anim.SetTrigger("Hit"); //play hit animation
        sFX.Play(); //play hit sound
        Destroy(gameObject, 0.5f); // destroy projectile
    }
}
