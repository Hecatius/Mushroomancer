using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour

{
    public float maxHealth = 10f;
    public float currentHealth;
    [SerializeField] private GameObject healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        healthBar.GetComponent<Slider>().maxValue = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        Debug.Log("oww");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.GetComponent<Slider>().value = currentHealth;
        }
    }

    private void Die()
    {
        // Handle death
        Destroy(gameObject);
    }
}


