using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HealthBar>() != null)
        {
            HealthBar health = collider.GetComponent<HealthBar>();
            health.Damage(damage);
        }
    }
}
