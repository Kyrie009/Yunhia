using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 2)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite sprite;
    //Stat boosts
    public int attack;
}
