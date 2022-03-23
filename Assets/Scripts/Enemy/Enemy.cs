using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : GameBehaviour
{
    public EnemyData enemyData;
    int health;
    int attack;
    int knockback;
    public float attackDistance = 10f;

  
    void Start()
    {
        Setup();
    }
    private void Update()
    {
        
    }
    //Links up enemystats from the scriptableobject
    public void Setup()
    {
        health = enemyData.health;
        attack = enemyData.attack;
        knockback = enemyData.knockBack;
    }
    //damage on contact - might want to use ontrigger enter for enemies here. - we gonna do this on hit boxes
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
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
        StartCoroutine(GotHit());
        if (IsDead())
        {
            Destroy(this.gameObject);
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

}
