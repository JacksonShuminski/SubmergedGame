using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public Spawn spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("HIT!");
            Destroy(collision.gameObject);
            spawner.SpawnPlayer();
        }
    }
}
