using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool isShieldActive = false; // sheilds
    private HealthUI healthUI; 
    public Transform healthRespawnPoint; 
    private PlayerRespawn playerRespawn; // Reference to PlayerRespawn script

    void Start()
    {
        currentHealth = maxHealth;
        healthUI = FindObjectOfType<HealthUI>();
        playerRespawn = GetComponent<PlayerRespawn>(); 
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isShieldActive) return; // Prevent damage if shield is active

        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthUI();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        healthUI.UpdateHealth(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player has died due to health depletion!");

        if (playerRespawn != null && healthRespawnPoint != null)
        {
            playerRespawn.RespawnAtPoint(healthRespawnPoint); // Respawn the player at the health respawn point
        }
        else
        {
            Debug.LogWarning("HealthRespawnPoint or PlayerRespawn is not set!");
        }

        // Reset health after respawning
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void SetShieldActive(bool isActive)
    {
        isShieldActive = isActive; // Update shield status
    }
}
