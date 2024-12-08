using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    public PlayerRespawn playerRespawn; 
    public Transform respawnPoint; 

    private bool isPaused = false;

    void Update()
    {
        // Toggle pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void RespawnPlayer()
    {
        if (playerRespawn != null && respawnPoint != null)
        {
            playerRespawn.RespawnAtPoint(respawnPoint);
            Resume(); // Automatically resume the game after respawning
        }
        
    }

    public void SaveAndMainMenu()
    {
        if (playerRespawn != null)
        {
            // Save player's position
            PlayerPrefs.SetFloat("PlayerX", playerRespawn.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", playerRespawn.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", playerRespawn.transform.position.z);

            // Save current scene
            PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);

            PlayerPrefs.Save();
        }

        // Load Main Menu scene
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
