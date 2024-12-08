using System.Collections;
using UnityEngine;
using TMPro;

public class Superspeed : MonoBehaviour
{
    public float speedMultiplier = 2f; 
    public float duration = 15f; 
    public TMP_Text superspeedTimerText; 
    public AudioClip superspeedVoiceLine; 

    private PlayerController playerController; 
    private PlayerAnimatorController animatorController; 
    private AudioSource audioSource; 
    private bool isActive = false; 
    private float remainingTime; 

    void Start()
    {
        // grabs needed components
        playerController = GetComponent<PlayerController>();
        animatorController = GetComponent<PlayerAnimatorController>();
        audioSource = GetComponent<AudioSource>();

        UpdateTimerUI(0); 
    }

    public void Activate()
    {
        if (!isActive) 
        {
            isActive = true; 
            remainingTime = duration; 

            PlaySuperspeedVoiceLine(); 

            animatorController.SetSprinting(true); 
            playerController.moveSpeed *= speedMultiplier; 
            StartCoroutine(SpeedCountdown()); 
        }
    }

    public void Deactivate()
    {
        if (isActive) 
        {
            isActive = false; 
            animatorController.SetSprinting(false); 
            playerController.moveSpeed /= speedMultiplier; 
            UpdateTimerUI(0); 
        }
    }

    private IEnumerator SpeedCountdown()
    {
        while (remainingTime > 0) 
        {
            UpdateTimerUI(remainingTime); 
            remainingTime -= Time.deltaTime; 
            yield return null; 
        }

        Deactivate(); // turn off superspeed when time is up
    }

    private void UpdateTimerUI(float time)
    {
        if (superspeedTimerText != null) 
        {
            superspeedTimerText.text = $"Superspeed: {Mathf.CeilToInt(time)}s"; 
        }
    }

    private void PlaySuperspeedVoiceLine()
    {
        if (audioSource != null && superspeedVoiceLine != null) 
        {
            audioSource.PlayOneShot(superspeedVoiceLine); 
        }
    }
}
