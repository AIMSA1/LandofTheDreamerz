using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    void Start()
    {
        
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            // save player postion in game
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            Debug.Log("No saved position found. Starting at default position.");
        }
    }

    public void SaveGame()
    {
        
        Vector3 playerPosition = transform.position;

        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        // save player data in game
        PlayerPrefs.Save();

        Debug.Log("Game Saved! Player position saved at: " + playerPosition);
    }
}
