using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this really needs to be fixed and abstracted properly
public class Shooting : GameBehaviour
{
    float castSpeed = 0.2f; //firing rate independant of character stats. but can be used as a stat parameter.
    int manaCost = 20;
    //cache
    public GameObject projectileprefab;
    public Transform shootingPoint;
    public Animator anim;
    bool isFiring = false;

    private void Awake()
    {
        anim = _P.anim;
    }
    void Update()
    {
        if (isFiring == false && _P.currentMana > 0)
        {
            if (Input.GetMouseButtonDown(1)) // fire spell input
            {
                if (_P.currentMana - manaCost >= 0) //checks if can pay the spell cost to fire spell if not enought do nothing and keep your remaining mana
                {
                    _P.currentMana -= manaCost; // pay mana cost 
                    isFiring = true;
                    anim.SetTrigger("MagicAttack1");
                    Shoot();
                    Invoke(nameof(ResetAttack), castSpeed);
                    if (IsOutOfMana()) // check if you have no mana left after firing spell
                    {
                        _P.currentMana = 0;                        
                    }
                    _UI.UpdateStatus();
                }
            }
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectileprefab, shootingPoint);
        projectile.transform.parent = null;
    }

    void ResetAttack()
    {
        isFiring = false;
    }

    bool IsOutOfMana()
    {
        return _P.currentMana <= 0;
    }
}
