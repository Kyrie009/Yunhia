using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWalls : GameBehaviour
{
    //Makes object invisible on initialisation
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }


}
