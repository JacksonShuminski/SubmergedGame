using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public bool hooked = false;
    private Transform rope;
    private Camera main;
    private Vector2 pullpoint;
    private Rigidbody2D rb;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rope = transform.GetChild(1);
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
        if (hooked)
        {
            pull();
        }

    }

    void shoot()
    {
        Vector3 mousePos = main.ScreenPointToRay(Input.mousePosition).origin;
        Vector3 toMouse = mousePos - transform.position;
        toMouse.z = 0;
        rope.right = toMouse;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, toMouse);
        //Debug.DrawLine(transform.position, toMouse);
        if (hit.collider != null)
        {
            pullpoint = hit.point;
            hooked = true;
        }
    }

    void pull()
    {
        if (Input.GetMouseButtonUp(0))
        {
            hooked = false;
            rope.localScale = new Vector3(0, 1, 1);
        }
        else
        {
            Vector2 tohit = pullpoint - (Vector2)transform.position;
            Debug.DrawLine(transform.position, pullpoint);
            rope.right = tohit;
            rope.localScale = new Vector3(tohit.magnitude, 1, 1);
            tohit.Normalize();
            rb.velocity = rb.velocity + tohit * speed * Time.deltaTime;
        }

    }

}
