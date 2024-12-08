using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts; 
    private int currentHealth;

    void Start()
    {
        //set up for 3 hearts in the beginning
        currentHealth = hearts.Length;
        UpdateHealthUI();
    }

    public void UpdateHealth(int health)
    {
        currentHealth = health;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            //hearts appear or not 
            hearts[i].enabled = i < currentHealth;
        }
    }
}
