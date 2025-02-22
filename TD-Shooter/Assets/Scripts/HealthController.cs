using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthController : MonoBehaviour
{
    public event Action OnDeath;

    public int maxHealth = 100;
    public int currentHealth;

    //private int damage = 10;
    //private int healPoints = 10;
    private void Awake()
    {
        currentHealth = maxHealth;
        // if not first level
        if (PlayerInfoKeeper.isGameStarted)
        {
            // chache HP value
            currentHealth = PlayerInfoKeeper.HP;
        }
        else
            PlayerInfoKeeper.HP = currentHealth;

    }

    public void FixedUpdate()
    {  
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void TakeDamage(int damage)
    {    
        currentHealth -= damage;

        if (CompareTag("Player"))
        {
            UIManager.Instance.UpdateHealthValue(currentHealth);
            Debug.Log($"Player received damage: {damage}");
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();

        Destroy(gameObject);
    }

    public void Heal(int hpToAdd)
    {
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

        currentHealth += hpToAdd;
    }
}
