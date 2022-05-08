using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeInstantiator : GameBehaviour
{
    public GameObject targetPrefab;

    private void Awake()
    {
        if(_GM.isFirstRun == true) // Run this if this only if it is the first playthrough
        {
            Instantiate(targetPrefab, this.transform.position, this.transform.rotation);
        }
        
    }
}
