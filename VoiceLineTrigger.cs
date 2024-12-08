using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    public AudioClip voiceLine; 
    public AudioSource voiceLineAudioSource; 
    public GameObject player; 
    private PlayerController playerController; 

    private bool hasPlayed = false; // Stops the voice line from playing multiple times
    public bool disableMovementDuringVoiceLine = false; 

    void Start()
    {
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !hasPlayed)
        {
            hasPlayed = true; 

            if (voiceLineAudioSource != null && voiceLine != null)
            {
                StartCoroutine(PlayVoiceLine());
            }
        }
    }

    private System.Collections.IEnumerator PlayVoiceLine()
    {
        // Disable player movement only if specified
        if (disableMovementDuringVoiceLine && playerController != null)
        {
            playerController.SetMovementEnabled(false);
        }

        // plays the voice line
        voiceLineAudioSource.clip = voiceLine;
        voiceLineAudioSource.Play();

        // Waitsuntil the voice line finishes
        yield return new WaitForSeconds(voiceLine.length);

        if (disableMovementDuringVoiceLine && playerController != null)
        {
            playerController.SetMovementEnabled(true);
        }
    }
}
