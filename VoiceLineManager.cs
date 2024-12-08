using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    public AudioSource voiceLineAudioSource; // Reference to the AudioSource
    public AudioSource backgroundMusicAudioSource; // Reference to the background music AudioSource
    public GameObject player; // Reference to the player GameObject
    private PlayerController playerController; // Reference to PlayerController for disabling movement

    private float originalMusicVolume; // Store the original background music volume
    public bool isIntro = true; // Determines if this is the intro voice line

    void Start()
    {
        // Get the PlayerController component
        playerController = player.GetComponent<PlayerController>();

        // Disable player movement if this is the intro
        if (isIntro && playerController != null)
        {
            playerController.SetMovementEnabled(false);
        }

        // Save the original volume of the background music
        if (backgroundMusicAudioSource != null)
        {
            originalMusicVolume = backgroundMusicAudioSource.volume;
        }

        // Play the voice line
        if (voiceLineAudioSource != null)
        {
            LowerMusicVolume(); // Lower background music volume
            voiceLineAudioSource.Play();
            StartCoroutine(EnableMovementAfterVoiceLine());
        }
    }

    private System.Collections.IEnumerator EnableMovementAfterVoiceLine()
    {
        // Wait until the voice line finishes
        yield return new WaitForSeconds(voiceLineAudioSource.clip.length);

        // Restore background music volume
        RestoreMusicVolume();

        // Enable player movement only if this is the intro
        if (isIntro && playerController != null)
        {
            playerController.SetMovementEnabled(true);
        }
    }

    private void LowerMusicVolume()
    {
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.volume = originalMusicVolume * 0.2f; // Reduce volume to 20%
        }
    }

    private void RestoreMusicVolume()
    {
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.volume = originalMusicVolume; // Restore original volume
        }
    }
}
