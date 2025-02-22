using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int enemyDamage;

    public void Initialize(int damage)
    {
        enemyDamage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthController health = other.GetComponent<HealthController>();
            if (health != null)
            {
                health.TakeDamage(enemyDamage);
                Debug.Log("DamageZone: Player received damage");
            }
        }
    }

}
