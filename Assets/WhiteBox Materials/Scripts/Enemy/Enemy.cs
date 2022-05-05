using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : GameBehaviour
{
    public EnemyData enemyData;
    public int health;
    public int attack;
    int knockback;
    public float attackDistance = 10f;
    public AudioClip hitSound;
    Instantiator instantiator;
  
    void Start()
    {
        //initialise reference
        instantiator = GetComponent<Instantiator>();

        Setup();
    }

    //Links up enemystats from the scriptableobject
    public void Setup()
    {
        health = enemyData.health;
        attack = enemyData.attack;
        knockback = enemyData.knockBack;
    }
    //damage on contact - might want to use ontrigger enter for enemies here. - we gonna do this on hit boxes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Enemy Damage
            _P.Hit(attack);
            //Knockback
        }
    }         

    //Takes Damage
    public void Hit(int _dmg)
    {       
        health -= _dmg;
        _UI.audioSource.PlayOneShot(hitSound);
        StartCoroutine(GotHit());
        if (IsDead())
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<Renderer>().enabled = false;
            KillReward();
            //enemydies
        }
    }
    //Hit indicator - won't need this when we get our animation
    IEnumerator GotHit()
    {       
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = Color.white; 
    }
    //Check if enemy dead
    public bool IsDead()
    {
        return health <= 0;
    }

    public void KillReward()
    {
        _P.PlayerGains(0, 0, Random.Range(10,40)); // runes gain
        instantiator.InstantiateObjects(); //drop collectables
    }

}
