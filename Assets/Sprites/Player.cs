using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public HealthBar healthbar;

    // Update is called once per frame
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }



    void Update()
    {
        if(currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
}