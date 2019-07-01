using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Health : MonoBehaviour
{
    public float maxHealth;
    public float maxShield;
    public float shieldRecharge;

    public float health;
    public float shield;


    private void Start()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (shield < maxShield)
        {
            shield += shieldRecharge;
        }
    }

    public bool damage(float dmg)
    {
        if (shield - dmg >= 0)
        {
            shield -= dmg;
            return false;
        }
        else
        {
            health += shield-dmg;
            shield = 0;

            if (health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    } 

}
