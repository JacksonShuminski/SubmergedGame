using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public GameObject player;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<BoxCollider2D>().IsTouching(this.GetComponent<BoxCollider2D>()))
        {
            //use a manager to switch between scenes
        }
    }
}
