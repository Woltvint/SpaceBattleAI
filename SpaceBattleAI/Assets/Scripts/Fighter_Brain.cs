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
    int outputNeurons = 7;


    private void Start()
    {
        net = new NeuralNet();
        net.inicNet(Random.Range(1, maxLayers),Random.Range(1, maxNeurons), outputNeurons);

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
                    targets[i] = targets[targets.Length];
                }
            }
        }

        GameObject target = GetClosestEnemy(targets);


        Vector3 dir = target.transform.position - transform.position;

        float ang = Vector3.Angle(Vector3.zero,target.transform.position) - Vector3.Angle(Vector3.zero, transform.position);

        Debug.Log("me:" + transform.position + " " +  (180 - Vector3.Angle(transform.position - controler.dirPoint.position,dir)));

        /*

        float[] input = {0,1,0};

        float[] output = net.calcNet(input);

        controler.SetSpeed(output[0]+ output[1]+ output[2]);
        controler.setRotation(output[3] + output[4] + output[5]);

        //fire ?
        if (output[6] > 0.5f)
        {
            //yes
            weapons.fire();
        }
        */
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

    float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
    {
        // angle in [0,180]
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));

        // angle in [-179,180]
        float signed_angle = angle * sign;

        // angle in [0,360] (not used but included here for completeness)
        //float angle360 =  (signed_angle + 180) % 360;

        return signed_angle;
    }


}
