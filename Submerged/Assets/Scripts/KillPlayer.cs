using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public Spawn spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            spawner.SpawnPlayer();
            AkSoundEngine.PostEvent("Death", gameObject);
        }
    }
}
