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
    public float inertMult;
    public Transform dirPoint;
    public Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForce((-rb.velocity.normalized) * inertForce * inertMult);
        
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

    public void setInert(float d)
    {
        inertMult = d;
    }
}
