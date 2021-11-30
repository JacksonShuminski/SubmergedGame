using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float smoothTime;
    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(0, 0, -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
