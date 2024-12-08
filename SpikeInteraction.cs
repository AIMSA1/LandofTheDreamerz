using UnityEngine;

public class SpikeInteraction : MonoBehaviour
{
    private bool isOnCooldown = false; 
    private PlayerHealth playerHealth; 

    private void OnTriggerEnter(Collider other)
    {
        // check if the player touched the spikes
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>(); 
            if (playerHealth != null && !isOnCooldown) // if there's no cooldown, start damaging
            {
                StartCoroutine(DamagePlayerContinuously());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = null; 
        }
    }

    private System.Collections.IEnumerator DamagePlayerContinuously()
    {
        // keeps hurting the player as long as they're on the spikes
        while (playerHealth != null)
        {
            if (!isOnCooldown) // only damage if there's no cooldown
            {
                playerHealth.TakeDamage(1); 
                isOnCooldown = true; 
                yield return new WaitForSeconds(2f); 
                isOnCooldown = false; 
            }
            else
            {
                yield return null; 
            }
        }
    }
}
