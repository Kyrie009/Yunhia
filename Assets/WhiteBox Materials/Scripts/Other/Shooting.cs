using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : GameBehaviour
{
    float attackspeed = 0.2f; //firing rate independant of character stats. but can be used as a stat parameter.
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
        if (isFiring == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isFiring = true;
                anim.SetTrigger("MagicAttack1");
                Shoot();
                Invoke(nameof(ResetAttack), attackspeed);
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
}
