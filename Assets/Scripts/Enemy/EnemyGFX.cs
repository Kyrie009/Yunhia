using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        //flips enemy to the direction it is moving towards
        if (aiPath.desiredVelocity.x >= 0.01)
        {
            transform.localScale = new Vector3(-0.15f, 0.15f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01)
        {
            transform.localScale = new Vector3(0.15f, 0.15f, 1f);
        }
    }
}
