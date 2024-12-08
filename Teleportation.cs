using System.Collections;
using UnityEngine;
using TMPro;

public class Teleportation : MonoBehaviour
{
    public float teleportDistance = 10f; 
    public int maxTeleports = 3; 
    public float rechargeTime = 10f; 
    public TMP_Text teleportationUI; 
    public AudioClip teleportVoiceLine; 

    private int availableTeleports; 
    private bool[] orbRechargeStatus; 
    private AudioSource audioSource; 
    private PlayerAnimatorController animatorController; 

    void Start()
    {
        availableTeleports = maxTeleports; // start with all orbs
        orbRechargeStatus = new bool[maxTeleports]; 
        audioSource = GetComponent<AudioSource>(); 
        animatorController = GetComponent<PlayerAnimatorController>(); 
        UpdateTeleportationUI(); 
    }

    public void Activate()
    {
        if (availableTeleports > 0) // only teleport if you got orbs
        {
            PlayTeleportVoiceLine(); // play the sound

            // do the teleport
            StartCoroutine(PerformTeleport());
        }
        else
        {
            Debug.Log("No teleport orbs available!"); // just in case you try without orbs
        }
    }

    private IEnumerator PerformTeleport()
    {
        animatorController?.SetTeleporting(true); // start animation
        yield return new WaitForSeconds(0.5f); // wait for animation

        // figure out where you're going
        Vector3 teleportDirection = transform.forward;
        Vector3 teleportTarget = CalculateTeleportTarget(teleportDirection);
        transform.position = teleportTarget; 

        animatorController?.SetTeleporting(false); 

        availableTeleports--; 
        UpdateTeleportationUI(); 
        for (int i = 0; i < maxTeleports; i++) 
        {
            if (!orbRechargeStatus[i])
            {
                orbRechargeStatus[i] = true; // start recharge
                StartCoroutine(RechargeOrb(i)); 
                break;
            }
        }
    }

    private Vector3 CalculateTeleportTarget(Vector3 direction)
    {
        RaycastHit hit;
        Vector3 startPosition = transform.position;

        if (Physics.Raycast(startPosition, direction, out hit, teleportDistance))
        {
            return hit.point - (direction.normalized * 0.1f); // stop short of the wall
        }
        else
        {
            return startPosition + direction * teleportDistance; // full distance if no wall
        }
    }

    private IEnumerator RechargeOrb(int orbIndex)
    {
        yield return new WaitForSeconds(rechargeTime); 
        orbRechargeStatus[orbIndex] = false; // orb is recharged
        availableTeleports++; // get an orb back
        UpdateTeleportationUI(); 
    }

    private void UpdateTeleportationUI()
    {
        if (teleportationUI != null) 
        {
            teleportationUI.text = $"Teleports: {availableTeleports}\nRecharging: {(maxTeleports - availableTeleports)}";
        }
    }

    private void PlayTeleportVoiceLine()
    {
        if (audioSource != null && teleportVoiceLine != null) 
        {
            audioSource.PlayOneShot(teleportVoiceLine); 
        }
    }
}
