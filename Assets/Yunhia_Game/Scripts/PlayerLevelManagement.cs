using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelManagement : MonoBehaviour
{
    Vector3 spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallZone")
        {
            transform.position = spawnPoint;
        }
        else if (collision.tag == "CheckPoint")
        {
            spawnPoint = transform.position;
        }
        else if (collision.tag == "NextLevel")
        {
            spawnPoint = transform.position;
        }
        else if (collision.tag == "PreviousLevel")
        {
            spawnPoint = transform.position;
        }
    }
}
