using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // the options menu panel
    public GameObject optionsMenu;
    // the main menu panel
    public GameObject mainMenu;

    public void PlayGame()
    {
        // check if the game has a save
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            // load the saved scene if it exists
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            // otherwise start a new game
            SceneManager.LoadScene("MainGame"); // replace "MainGame" with ur scene name
        }
    }

    public void OpenOptions()
    {
        // show the options menu and hide the main menu
        if (optionsMenu != null && mainMenu != null)
        {
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void CloseOptions()
    {
        // show the main menu and hide the options menu
        if (optionsMenu != null && mainMenu != null)
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        // quits the game when built
        Application.Quit();
    }
}
