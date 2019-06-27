using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Weapons : MonoBehaviour
{
    public float power;
    public float laserPowerCost;
    public GameObject shot;
    public float time;
    public float delay;

    public Transform firePoint1;
    public Transform firePoint2;

    private void FixedUpdate()
    {
        if (power < 100)
        {
            power += 0.5f;
        }
        if (time > 0)
        {
            time--;
        }

        fire();
    }


    public void fire()
    {
        if (power >= laserPowerCost && time == 0)
        {
            Instantiate(shot, firePoint1.position, transform.rotation);
            Instantiate(shot, firePoint2.position, transform.rotation);
            power -= laserPowerCost;
            time = delay;
        }
    }
}
