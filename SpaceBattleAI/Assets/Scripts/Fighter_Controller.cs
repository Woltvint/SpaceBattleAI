using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Controller : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public int speedMult;
    public int rotMult;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector2(speed * speedMult, 0));
        rb.AddTorque(rotSpeed * rotMult);
    }

    public void SetSpeed(int d)
    {
        speedMult = d;
    }

    public void setRotation(int d)
    {
        rotMult = d;
    }
}
