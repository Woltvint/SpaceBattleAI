using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float velocity;
    public float range;
    public Transform p1;
    public Transform p2;

    private Vector3 start;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        start = transform.position;
    }

    void FixedUpdate()
    {
        rb.velocity = (p2.position - p1.position) * velocity;

        if (Vector3.Distance(start, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
}
