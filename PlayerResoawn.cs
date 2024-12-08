using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // where the player respawns by default when they fall
    public Transform respawnPoint; 

    // list of sounds to play when the player dies
    public AudioClip[] deathSounds; 

    // plays the sounds
    private AudioSource audioSource;

    // what level the player is on rn
    private int currentLevel = 0; 

    // this keeps track of where to respawn for each level
    [System.Serializable]
    public class LevelRespawnPoints
    {
        public int level; // what level this data is for
        public Transform fallRespawnPoint; // where to respawn if u fall
        public Transform healthRespawnPoint; // where to respawn if ur out of hearts
    }

    // all the respawn points for all levels
    public LevelRespawnPoints[] levelRespawnData; 

    void Start()
    {
        // get the AudioSource so we can play sounds
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the player hits a FallZone
        if (other.CompareTag("FallZone"))
        {
            // check if the FallZone has its own respawn point
            RespawnPoint respawnPointComponent = other.GetComponent<RespawnPoint>();
            if (respawnPointComponent != null)
            {
                RespawnAtPoint(respawnPointComponent.respawnPoint); // use its respawn point
            }
            else
            {
                Respawn(); // otherwise use the default one
            }
        }
    }

    public void RespawnOnHealthDepletion()
    {
        // get the right health respawn point for the current level
        Transform healthRespawnPoint = GetHealthRespawnPointForCurrentLevel();
        if (healthRespawnPoint != null)
        {
            RespawnAtPoint(healthRespawnPoint); // respawn there
        }
    }

    private void Respawn()
    {
        PlayRandomDeathSound(); // play a random death sound

        // put the player at the default respawn point
        transform.position = respawnPoint.position;

        // reset velocity if u have a Rigidbody
        ResetVelocity();
    }

    public void RespawnAtPoint(Transform customRespawnPoint)
    {
        PlayRandomDeathSound(); // play a sound for dying

        // move the player to the custom respawn point
        transform.position = customRespawnPoint.position;

        // reset velocity again
        ResetVelocity();
    }

    public void SetCurrentLevel(int level)
    {
        // update the current level the player is on
        currentLevel = level;
    }

    private Transform GetHealthRespawnPointForCurrentLevel()
    {
        // loop thru all levels to find the health respawn point for the current one
        foreach (LevelRespawnPoints levelRespawn in levelRespawnData)
        {
            if (levelRespawn.level == currentLevel)
            {
                return levelRespawn.healthRespawnPoint;
            }
        }
        // return nothing if there's no match
        return null; 
    }

    private void PlayRandomDeathSound()
    {
        // check if we have sounds to play
        if (audioSource != null && deathSounds.Length > 0)
        {
            // pick a random sound from the list
            int randomIndex = Random.Range(0, deathSounds.Length); 
            audioSource.PlayOneShot(deathSounds[randomIndex]); // play the sound
        }
    }

    private void ResetVelocity()
    {
        // stop the player's Rigidbody from flying around
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
