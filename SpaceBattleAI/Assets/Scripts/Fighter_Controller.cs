using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Controller : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public float inertForce;
    public float speedMult;
    public float rotMult;
    public Transform dirPoint;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForce((-rb.velocity.normalized) * inertForce);
        
        rb.AddRelativeForce(new Vector2(speed * speedMult, 0));
        rb.AddTorque(rotSpeed * rotMult);
    }

    public void SetSpeed(float d)
    {
        speedMult = d;
    }

    public void setRotation(float d)
    {
        rotMult = d;
    }
}
