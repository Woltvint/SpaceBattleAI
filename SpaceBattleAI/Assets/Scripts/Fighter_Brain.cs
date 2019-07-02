using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Brain : MonoBehaviour
{

    private NeuralNet net;
    private Fighter_Controller controler;
    private Fighter_Weapons weapons;
    private Fighter_Health health;

    private Vector3 start;

    int maxLayers = 10;
    int maxNeurons = 20;
    int outputNeurons = 8;


    private void Start()
    {
        net = new NeuralNet();
        net.inicNet(Random.Range(1, maxLayers), Random.Range(1, maxNeurons), outputNeurons);

        controler = gameObject.GetComponent<Fighter_Controller>();
        weapons = gameObject.GetComponent<Fighter_Weapons>();
        health = gameObject.GetComponent<Fighter_Health>();

        start = transform.position;
    }


    private void FixedUpdate()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].GetInstanceID() == gameObject.GetInstanceID())
            {
                if (i != 0)
                {
                    targets[i] = targets[0];
                }
                else
                {
                    targets[i] = targets[targets.Length - 1];
                }
            }
        }

        GameObject target = GetClosestEnemy(targets);

        //inputs
        float distToEnemy = Vector3.Distance(transform.position, target.transform.position);
        float angleToEnemy = angleToVector(transform.position, controler.dirPoint.position, target.transform.position);

        float distToSpawn = Vector3.Distance(start, transform.position);
        float angleToSpawn = angleToVector(transform.position, controler.dirPoint.position, start);

        float velocity = Mathf.Abs(controler.rb.velocity.x) + Mathf.Abs(controler.rb.velocity.y);

        float power = map(weapons.power, 0, weapons.maxPower, 0, 1);
        float hp = map(health.health, 0, health.maxHealth, 0, 1);
        float shield = map(health.shield, 0, health.maxShield, 0, 1);


        float[] input = { distToEnemy, angleToEnemy, distToSpawn, angleToSpawn, velocity, power, hp, shield };


        //thinking
        float[] output = net.calcNet(input);


        //using the output to control the ship
        controler.SetSpeed(((output[0] + 1) / 2) + ((output[1] + 1) / 2) + ((output[2] + 1) / 2));
        controler.setRotation(output[3] + output[4] + output[5]);
        controler.setInert((output[6] + 1) / 2);

        //fire ?
        if (output[7] > 0)
        {
            //yes
            weapons.fire();
        }

    }


    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


    float angleToVector(Vector3 from1, Vector3 from2, Vector3 to)
    {
        Vector3 dir = to - from1;
        Vector3 mydir = from1 - from2;

        float ang = (180 - Vector3.Angle(mydir, dir));

        ang *= Mathf.Sign(Vector3.Dot(Vector3.forward, Vector3.Cross(mydir, dir)));

        return ang;
    }

    float map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}