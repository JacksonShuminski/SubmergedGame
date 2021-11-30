using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }


    public void SpawnPlayer()
    {
        GameObject p = Instantiate(player, transform.position, Quaternion.identity);
        p.transform.position -= Vector3.forward * p.transform.position.z;
    }
}
