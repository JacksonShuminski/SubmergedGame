using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObject : MonoBehaviour
{
    public Transform[] nodes;
    public float speed;
    public int current;


    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        if (nodes.Length > 0)
        {
            transform.position = nodes[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toNode = nodes[(current + 1) % nodes.Length].position - transform.position;
        toNode.z = 0;
        if (toNode.magnitude > speed*Time.deltaTime)
        {
            transform.position += toNode.normalized * speed * Time.deltaTime;
        }
        else
        {
            current++;
        }
    }
}
