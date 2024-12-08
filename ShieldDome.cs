using System.Collections;
using UnityEngine;
using TMPro; 

public class ShieldDome : MonoBehaviour
{
    public float duration = 5f; 
    public TMP_Text shieldStatusText; 
    public AudioClip shieldVoiceLine; 
    private Renderer playerRenderer; 
    private Color originalColor; 
    private Coroutine deactivateCoroutine; 
    private PlayerAnimatorController animatorController; 
    private PlayerHealth playerHealth; 
    private AudioSource audioSource; 

    void Start()
    {
        // grab all the important components we need
        animatorController = GetComponent<PlayerAnimatorController>();
        playerRenderer = GetComponentInChildren<Renderer>();
        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>(); // makes sure the player can play sounds

        // save the player's original color and turn on glow effects
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
            playerRenderer.material.EnableKeyword("_EMISSION");
        }

        // set the UI to show that the shield is off at first
        UpdateShieldStatusUI(false);
    }

    public void Activate()
    {
        if (deactivateCoroutine == null)
        {
            SetPlayerGlow(Color.blue); 
            animatorController.SetBlocking(true); 
            playerHealth.SetShieldActive(true); // tell the health script to block damage
            UpdateShieldStatusUI(true); 

            PlayShieldVoiceLine();

            // start the timer to turn off the shield after it's done
            deactivateCoroutine = StartCoroutine(DeactivateShieldAfterDuration());
        }
    }

    public void Deactivate()
    {
        // only do stuff if the shield is actually active
        if (deactivateCoroutine != null)
        {
            SetPlayerGlow(originalColor); // reset the player's glow
            animatorController.SetBlocking(false); // stop the block animation
            playerHealth.SetShieldActive(false); // let the player take damage again
            UpdateShieldStatusUI(false); // update the UI to show the shield is off
            StopCoroutine(deactivateCoroutine); // stop the timer
            deactivateCoroutine = null;
        }
    }

    private IEnumerator DeactivateShieldAfterDuration()
    {
        // wait for the shield to run out
        yield return new WaitForSeconds(duration);
        Deactivate(); // turn off the shield
    }

    private void SetPlayerGlow(Color color)
    {
        // change the player's color and glow settings
        if (playerRenderer != null)
        {
            playerRenderer.material.color = color;
            playerRenderer.material.SetColor("_EmissionColor", color);
        }
    }

    private void UpdateShieldStatusUI(bool isActive)
    {
        // update the UI text to show if the shield is on or off
        if (shieldStatusText != null)
        {
            shieldStatusText.text = isActive ? "Shield Active: ON" : "Shield Active: OFF";
        }
    }

    private void PlayShieldVoiceLine()
    {
        // play the voice line when the shield activates
        if (audioSource != null && shieldVoiceLine != null)
        {
            audioSource.PlayOneShot(shieldVoiceLine);
        }
    }
}
