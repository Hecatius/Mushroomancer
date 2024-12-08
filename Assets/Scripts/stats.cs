using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour

{
    public float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private GameObject healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

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


