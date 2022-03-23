using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameBehaviour
{
    public WeaponData weapon;
    public string weaponName;
    public Sprite sprite;
    public int attack;

    void Start()
    {
        Setup();
    }
    //Used to update current weapon
    public void Setup()
    {
        weaponName = weapon.weaponName;
        sprite = weapon.sprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
        //stats
        attack = weapon.attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Hit(attack);
        }
    }
}
